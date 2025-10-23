using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TerraTechModMenu
{
    /// <summary>
    /// カスタムウェイポイントクラス
    /// </summary>
    [System.Serializable]
    public class CustomWaypoint
    {
        public string name;
        public float x;
        public float y;
        public float z;

        public CustomWaypoint(string name, Vector3 position)
        {
            this.name = name;
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }

        public Vector3 GetPosition()
        {
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// カスタムウェイポイントリスト（JSON保存用）
    /// </summary>
    [System.Serializable]
    public class CustomWaypointList
    {
        public List<CustomWaypoint> waypoints = new List<CustomWaypoint>();
    }

    /// <summary>
    /// UIタブの種類
    /// </summary>
    public enum TabType
    {
        Cheats,     // チート機能
        Tech,       // Tech関連
        Teleport,   // テレポート
        ESP,        // CAB可視化
        Settings,   // 設定
        Info        // 情報
    }

    /// <summary>
    /// ModMenuの管理クラス
    /// Unity IMGUIを使用してメニューを描画
    /// </summary>
    public class ModMenuManager : MonoBehaviour
    {
        /// <summary>
        /// 敵CAB情報（キャッシュ用）
        /// </summary>
        private class EnemyCabInfo
        {
            public Tank tank;
            public TankBlock cabBlock;
            public Vector3 position;
            public float distance;
            public string corpName;
            public cakeslice.Outline outline;
        }
        // メニューの表示状態
        private bool showMenu = false;

        // メニューのウィンドウRect
        private Rect windowRect = new Rect(20, 20, 500, 700);

        // メニューを開く前のカーソル状態を保存（復元用）
        private bool previousCursorVisible = false;
        private CursorLockMode previousCursorLockState = CursorLockMode.None;

        // スクロールビューの位置
        private Vector2 scrollPosition = Vector2.zero;

        // 現在のタブ
        private TabType currentTab = TabType.Cheats;

        // ウェイポイント一覧の表示状態
        private bool showWaypoints = false;

        // カスタムウェイポイント管理
        private CustomWaypointList customWaypoints = new CustomWaypointList();
        private string waypointSavePath = "C:\\Programming\\TerraTechModMenu\\CustomWaypoints.json";
        private string newWaypointName = "New Waypoint";
        private bool showCustomWaypointCreation = false;

        // 座標入力テレポート
        private string coordInputX = "0";
        private string coordInputY = "0";
        private string coordInputZ = "0";
        private bool showCoordInput = false;

        // チート機能のON/OFF状態
        private bool godMode = false;
        private bool infiniteResources = false;
        private bool unlockAllBlocks = false;
        private bool noClip = false;
        private bool oneHitKill = false;
        private bool destroyCabOnly = false;

        // ESP（CAB可視化）設定
        private bool showEnemyCABs = false;
        private float espRange = 500f;
        private bool espShowDistance = true;
        private bool espShowCorpName = false;
        private List<EnemyCabInfo> cachedEnemyCABs = new List<EnemyCabInfo>();
        private float lastCabScanTime = 0f;
        private const float CAB_SCAN_INTERVAL = 0.1f; // 0.1秒ごとに更新

        // 自動照準設定
        private bool autoAimToCAB = false;
        private float autoAimRange = 300f;

        // 敵AI無効化設定
        private bool disableEnemyAI = false;
        private float disableAIRange = 0f; // 0 = 全範囲
        private System.Collections.Generic.HashSet<TechAI> disabledAITechs = new System.Collections.Generic.HashSet<TechAI>();

        // No Clip設定
        private bool noClipApplied = false;
        private List<Collider> playerColliders = new List<Collider>();

        // Fire Rate Multiplier設定
        private float fireRateMultiplier = 1.0f;
        private Dictionary<ModuleWeapon, float> originalFireRates = new Dictionary<ModuleWeapon, float>();

        // Time of Day設定
        private float timeOfDay = 12f; // 0-24時間

        // Auto Harvest設定
        private bool autoHarvest = false;
        private float autoHarvestRange = 50f;

        // Enemy Tech Block Editor設定
        private bool showEnemyTechEditor = false;
        private Tank selectedEnemyTech = null;
        private float enemyTechScanRange = 100f;
        private Vector2 enemyTechEditorScrollPos = Vector2.zero;

        // Enemy Tech Edit Mode設定
        private bool isEditMode = false;
        private Tank originalPlayerTech = null;
        private int originalEnemyTeam = -1;

        // Infinite Collector設定
        private bool infiniteCollector = false;
        private float collectorRangeMultiplier = 1.0f;

        // リフレクションキャッシュ（武器制御用）
        private System.Reflection.MethodInfo controlInputTargetedMethod = null;
        private bool weaponReflectionInitialized = false;

        // タイムスケール
        private float timeScale = 1.0f;

        // Tech Spawner設定
        private int spawnCount = 1;
        private int spawnTeam = 1; // 1=敵, 0=味方
        private float spawnDistance = 30f;
        private int minBlockCount = 1;
        private int maxBlockCount = 100;

        // マップ生成範囲拡張
        private bool expandMapRange = false;
        private float mapRangeMultiplier = 2.0f;

        // ストレージ収集強化
        private bool enhanceStorage = false;
        private bool enhanceStorageApplied = false; // 適用済みフラグ
        private bool reflectionCacheInitialized = false; // Reflectionキャッシュの初期化済みフラグ
        private float pickupRangeMultiplier = 3.0f;
        private float pickupSpeedMultiplier = 3.0f; // デフォルト値を下げた
        private float storageCapacityMultiplier = 1.0f; // スタック容量倍率（NEW）
        private float lastPickupRangeMultiplier = 3.0f; // 前回の値（変更検出用）
        private float lastPickupSpeedMultiplier = 3.0f; // 前回の値（変更検出用）
        private float lastStorageCapacityMultiplier = 1.0f; // 前回の値（変更検出用）

        // 元の値を保存（復元用）
        private System.Collections.Generic.Dictionary<object, float> originalRanges = new System.Collections.Generic.Dictionary<object, float>();
        private System.Collections.Generic.Dictionary<object, float> originalStrengths = new System.Collections.Generic.Dictionary<object, float>();
        private System.Collections.Generic.Dictionary<ModuleItemHolder, int> originalCapacities = new System.Collections.Generic.Dictionary<ModuleItemHolder, int>();

        // Reflectionフィールドキャッシュ（遅延初期化：初めてStorage EnhancementをONにした時に取得）
        private System.Reflection.FieldInfo rangeField;
        private System.Reflection.FieldInfo intervalField;
        private System.Reflection.FieldInfo timerField;
        private System.Reflection.FieldInfo pickupIntervalField;
        private System.Reflection.FieldInfo handoverIntervalField;
        private System.Reflection.FieldInfo prePickupField;
        private System.Reflection.PropertyInfo contentionProp;
        private System.Reflection.FieldInfo strengthField;
        private System.Reflection.FieldInfo dropAfterField;
        private System.Reflection.FieldInfo glueField;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Debug.Log("[TerraTech ModMenu] ModMenuManager started!");
            // InitializeReflectionCache(); // 一時的に無効化
            LoadCustomWaypoints(); // カスタムウェイポイントを読み込み
        }

        /// <summary>
        /// 終了処理（MonoBehaviour破棄時のクリーンアップ）
        /// </summary>
        private void OnDestroy()
        {
            try
            {
                Debug.Log("[TerraTech ModMenu] ModMenuManager destroying, cleaning up outlines...");
                // 全てのOutlineを無効化
                DisableAllCachedOutlines();
                cachedEnemyCABs.Clear();

                // 無効化した敵AIを復元
                if (disabledAITechs.Count > 0)
                {
                    RestoreAllDisabledAI();
                }

                // No Clipのコライダーを復元
                if (noClipApplied)
                {
                    foreach (var collider in playerColliders)
                    {
                        if (collider != null)
                        {
                            collider.enabled = true;
                        }
                    }
                    playerColliders.Clear();
                    noClipApplied = false;
                }

                // ストレージ強化の設定を復元
                if (enhanceStorageApplied)
                {
                    // 元の値に復元
                    foreach (var kvp in originalRanges)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            if (rangeField != null)
                                rangeField.SetValue(kvp.Key, kvp.Value);
                        }
                        catch { }
                    }

                    foreach (var kvp in originalStrengths)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            if (strengthField != null)
                                strengthField.SetValue(kvp.Key, kvp.Value);
                        }
                        catch { }
                    }

                    foreach (var kvp in originalCapacities)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            int numStacks = kvp.Key.NumStacks;
                            if (numStacks > 0)
                            {
                                int originalCapacityPerStack = kvp.Value / numStacks;
                                kvp.Key.OverrideStackCapacity(originalCapacityPerStack);
                            }
                        }
                        catch { }
                    }
                }

                Debug.Log("[TerraTech ModMenu] ModMenuManager cleanup completed");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] OnDestroy failed: {ex}");
            }
        }

        /// <summary>
        /// Reflectionフィールドをキャッシュ（パフォーマンス向上）
        /// </summary>
        private void InitializeReflectionCache()
        {
            try
            {
                // ModuleItemPickup用フィールド
                rangeField = typeof(ModuleItemPickup).GetField("m_PickupRange",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                intervalField = typeof(ModuleItemPickup).GetField("m_VisionRefreshInterval",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                timerField = typeof(ModuleItemPickup).GetField("m_VisionRefreshTimer",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                pickupIntervalField = typeof(ModuleItemPickup).GetField("m_PickupAfterMinInterval",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                handoverIntervalField = typeof(ModuleItemPickup).GetField("m_HandoverAfterMinInterval",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prePickupField = typeof(ModuleItemPickup).GetField("m_PrePickupPeriod",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                // ModuleItemHolder用プロパティ
                contentionProp = typeof(ModuleItemHolder).GetProperty("PickupContentionPeriod");

                // ModuleItemHolderMagnet用フィールド
                strengthField = typeof(ModuleItemHolderMagnet).GetField("m_Strength",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                dropAfterField = typeof(ModuleItemHolderMagnet).GetField("m_DropAfterMinTime",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                glueField = typeof(ModuleItemHolderMagnet).GetField("m_GluePeriod",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                Debug.Log("[ModMenu] Reflection cache initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] Failed to initialize reflection cache: {ex}");
            }
        }

        /// <summary>
        /// 毎フレーム実行される処理
        /// キー入力をチェック
        /// </summary>
        private void Update()
        {
            // INSERT キーでメニューの表示/非表示を切り替え
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                if (!showMenu)
                {
                    // メニューを開く前に現在のカーソル状態を保存
                    previousCursorVisible = Cursor.visible;
                    previousCursorLockState = Cursor.lockState;

                    // メニューを開く
                    showMenu = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    // メニューを閉じる時は元のカーソル状態に復元
                    showMenu = false;
                    Cursor.visible = previousCursorVisible;
                    Cursor.lockState = previousCursorLockState;
                }
            }

            // ESC キーでメニューを閉じる
            if (showMenu && Input.GetKeyDown(KeyCode.Escape))
            {
                // メニューを閉じる時は元のカーソル状態に復元
                showMenu = false;
                Cursor.visible = previousCursorVisible;
                Cursor.lockState = previousCursorLockState;
            }

            // チート機能の適用
            ApplyCheatFeatures();

            // ESP（CAB可視化）または自動照準のスキャン
            if (showEnemyCABs || autoAimToCAB)
            {
                ScanEnemyCABs();
            }
            else
            {
                // ESP/自動照準が両方OFFの場合、Outlineを無効化してキャッシュをクリア
                if (cachedEnemyCABs.Count > 0)
                {
                    DisableAllCachedOutlines();
                    cachedEnemyCABs.Clear();
                }
            }

            // 自動照準を適用
            if (autoAimToCAB)
            {
                ApplyAutoAimToCAB();
            }

            // 敵AI無効化を適用
            ApplyDisableEnemyAI();

            // Storage Enhancementは一時的に無効化
            // ApplyEnhanceStorage();
        }

        /// <summary>
        /// チート機能を毎フレーム適用
        /// </summary>
        private void ApplyCheatFeatures()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // God Mode (無敵)
                // forever=false で毎フレーム適用（トグル可能）
                if (godMode)
                {
                    playerTank.SetInvulnerable(true, false);
                }
                else
                {
                    playerTank.SetInvulnerable(false, false);
                }

                // Infinite Resources (無限リソース)
                if (infiniteResources)
                {
                    ApplyInfiniteResources();
                }

                // Expand Map Range (マップ生成範囲拡張)
                ApplyExpandMapRange();

                // Enhance Storage (ストレージ収集強化) - 遅延初期化方式で有効化
                ApplyEnhanceStorage();

                // One Hit Kill (ワンヒットキル)
                if (oneHitKill)
                {
                    ApplyOneHitKill();
                }

                // Destroy CAB Only (CABのみ破壊)
                if (destroyCabOnly)
                {
                    ApplyDestroyCabOnly();
                }

                // No Clip (壁抜け)
                ApplyNoClip();

                // Fire Rate Multiplier (連射速度倍率)
                ApplyFireRateMultiplier();

                // Auto Harvest (自動採掘)
                if (autoHarvest)
                {
                    ApplyAutoHarvest();
                }

                // Time of Day (時間帯制御)
                ApplyTimeOfDay();

                // Infinite Collector (無限コレクター)
                ApplyInfiniteCollector();

                // Edit Mode Safety Check (編集モードの安全チェック)
                CheckEditModeSafety();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyCheatFeatures failed: {ex}");
            }
        }

        /// <summary>
        /// ワンヒットキルを適用（近くの敵を即座に破壊）
        /// </summary>
        private void ApplyOneHitKill()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // プレイヤーの周囲50m以内の敵Techを検索
                ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
                if (manVisible == null) return;

                Vector3 playerPos = playerTank.trans.position;

                // Tankタイプのオブジェクトを検索
                var searchIterator = manVisible.VisiblesTouchingRadius(
                    playerPos,
                    50f,  // 検索範囲: 50m
                    new Bitfield<ObjectTypes>(new ObjectTypes[] { ObjectTypes.Vehicle }),
                    false,
                    0
                );

                // 見つかった全ての敵Techに大ダメージを与える
                foreach (var visible in searchIterator)
                {
                    if (visible != null && visible.tank != null)
                    {
                        Tank enemyTank = visible.tank;

                        // プレイヤー自身はスキップ
                        if (enemyTank == playerTank) continue;

                        // 敵かどうかチェック
                        if (enemyTank.IsEnemy(playerTank.Team))
                        {
                            // 敵を即座に破壊（大ダメージを全ブロックに与える）
                            try
                            {
                                // ブロック全てに致命的ダメージ
                                var blockman = enemyTank.blockman;
                                if (blockman != null)
                                {
                                    // 全ブロックを反復処理して破壊
                                    var blocks = new System.Collections.Generic.List<TankBlock>();
                                    foreach (var block in blockman.IterateBlocks())
                                    {
                                        blocks.Add(block);
                                    }

                                    // 一気に破壊
                                    foreach (var block in blocks)
                                    {
                                        if (block != null && block.IsAttached)
                                        {
                                            block.damage.SelfDestruct(0.01f); // 0.01秒後に自爆
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"[ModMenu] Failed to destroy enemy tank: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyOneHitKill failed: {ex}");
            }
        }

        /// <summary>
        /// CABのみを破壊（敵を即座に無力化）
        /// </summary>
        private void ApplyDestroyCabOnly()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // プレイヤーの周囲50m以内の敵Techを検索
                ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
                if (manVisible == null) return;

                Vector3 playerPos = playerTank.trans.position;

                // Tankタイプのオブジェクトを検索
                var searchIterator = manVisible.VisiblesTouchingRadius(
                    playerPos,
                    50f,  // 検索範囲: 50m
                    new Bitfield<ObjectTypes>(new ObjectTypes[] { ObjectTypes.Vehicle }),
                    false,
                    0
                );

                // 見つかった全ての敵TechのCABのみを破壊
                foreach (var visible in searchIterator)
                {
                    if (visible != null && visible.tank != null)
                    {
                        Tank enemyTank = visible.tank;

                        // プレイヤー自身はスキップ
                        if (enemyTank == playerTank) continue;

                        // 敵かどうかチェック
                        if (enemyTank.IsEnemy(playerTank.Team))
                        {
                            // CABブロックのみを静かに破壊
                            try
                            {
                                var blockman = enemyTank.blockman;
                                if (blockman != null)
                                {
                                    // GetRootBlock()でCABブロックを取得
                                    TankBlock cabBlock = blockman.GetRootBlock();

                                    if (cabBlock != null && cabBlock.IsAttached)
                                    {
                                        // CABを静かに切り離す（爆発なし）
                                        // blockman.Detach()を使って静かに分離
                                        blockman.Detach(cabBlock, true, false, false);

                                        // 切り離したCABブロックを直接削除
                                        if (cabBlock != null)
                                        {
                                            UnityEngine.Object.Destroy(cabBlock.gameObject);
                                        }

                                        Debug.Log($"[ModMenu] Quietly destroyed CAB of enemy tank at {enemyTank.trans.position}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"[ModMenu] Failed to destroy enemy CAB: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyDestroyCabOnly failed: {ex}");
            }
        }

        /// <summary>
        /// 敵のCAB情報をスキャンしてキャッシュ（0.1秒ごとに更新）
        /// </summary>
        private void ScanEnemyCABs()
        {
            try
            {
                // スキャン間隔チェック（0.1秒ごと）
                if (Time.time - lastCabScanTime < CAB_SCAN_INTERVAL)
                {
                    return;
                }
                lastCabScanTime = Time.time;

                Tank playerTank = Singleton.playerTank;
                if (playerTank == null)
                {
                    cachedEnemyCABs.Clear();
                    return;
                }

                ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
                if (manVisible == null)
                {
                    cachedEnemyCABs.Clear();
                    return;
                }

                Vector3 playerPos = playerTank.trans.position;

                // 古いOutlineを無効化してからキャッシュをクリア
                DisableAllCachedOutlines();
                cachedEnemyCABs.Clear();

                // ESP、自動照準の最大範囲を計算
                float maxRange = Mathf.Max(espRange, autoAimRange);

                // 指定範囲内の敵Techを検索
                var searchIterator = manVisible.VisiblesTouchingRadius(
                    playerPos,
                    maxRange,
                    new Bitfield<ObjectTypes>(new ObjectTypes[] { ObjectTypes.Vehicle }),
                    false,
                    0
                );

                foreach (var visible in searchIterator)
                {
                    if (visible != null && visible.tank != null)
                    {
                        Tank enemyTank = visible.tank;

                        // プレイヤー自身はスキップ
                        if (enemyTank == playerTank) continue;

                        // 敵かどうかチェック
                        if (enemyTank.IsEnemy(playerTank.Team))
                        {
                            try
                            {
                                var blockman = enemyTank.blockman;
                                if (blockman != null)
                                {
                                    // CABブロックを取得
                                    TankBlock cabBlock = blockman.GetRootBlock();

                                    if (cabBlock != null && cabBlock.IsAttached)
                                    {
                                        // Outlineコンポーネントを取得
                                        cakeslice.Outline outline = cabBlock.GetComponent<cakeslice.Outline>();

                                        // CAB情報をキャッシュに追加
                                        EnemyCabInfo cabInfo = new EnemyCabInfo
                                        {
                                            tank = enemyTank,
                                            cabBlock = cabBlock,
                                            position = cabBlock.centreOfMassWorld,
                                            distance = Vector3.Distance(playerPos, cabBlock.centreOfMassWorld),
                                            corpName = enemyTank.name,
                                            outline = outline
                                        };
                                        cachedEnemyCABs.Add(cabInfo);

                                        // ESPが有効な場合、Outlineを有効化
                                        if (showEnemyCABs && outline != null && cabInfo.distance <= espRange)
                                        {
                                            outline.EnableOutline(true, cakeslice.Outline.OutlineEnableReason.ScriptHighlight);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"[ModMenu] Failed to scan enemy CAB: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ScanEnemyCABs failed: {ex}");
            }
        }

        /// <summary>
        /// キャッシュされた全てのOutlineを無効化
        /// </summary>
        private void DisableAllCachedOutlines()
        {
            try
            {
                foreach (var cabInfo in cachedEnemyCABs)
                {
                    if (cabInfo != null && cabInfo.outline != null)
                    {
                        cabInfo.outline.EnableOutline(false, cakeslice.Outline.OutlineEnableReason.ScriptHighlight);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] DisableAllCachedOutlines failed: {ex}");
            }
        }

        /// <summary>
        /// 無限リソースを適用（全ブロックを999個に維持）
        /// </summary>
        private void ApplyInfiniteResources()
        {
            try
            {
                var manPlayer = Singleton.Manager<ManPlayer>.inst;
                if (manPlayer == null) return;

                // プライベートフィールドm_SaveDataにアクセスする必要があるため、
                // リフレクションを使用
                var saveDataField = typeof(ManPlayer).GetField("m_SaveData",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (saveDataField == null) return;

                var saveData = saveDataField.GetValue(manPlayer);
                if (saveData == null) return;

                var inventoryField = saveData.GetType().GetField("m_Inventory");
                if (inventoryField == null) return;

                var inventory = inventoryField.GetValue(saveData) as IInventory<BlockTypes>;
                if (inventory == null) return;

                // 全ブロックを999個に設定
                foreach (var item in inventory)
                {
                    if (item.Value < 999)
                    {
                        int addAmount = 999 - item.Value;
                        // HostAddItemメソッドを呼び出し
                        var addMethod = inventory.GetType().GetMethod("HostAddItem");
                        if (addMethod != null)
                        {
                            addMethod.Invoke(inventory, new object[] { item.Key, addAmount });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyInfiniteResources failed: {ex}");
            }
        }

        /// <summary>
        /// マップ生成範囲を拡張
        /// </summary>
        private void ApplyExpandMapRange()
        {
            try
            {
                var treadmill = Singleton.Manager<ManWorldTreadmill>.inst;
                if (treadmill == null) return;

                var field = typeof(ManWorldTreadmill).GetField("m_DistanceFromOriginBeforeMove",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (field != null)
                {
                    float newValue = expandMapRange ? 384f * mapRangeMultiplier : 384f;
                    field.SetValue(treadmill, newValue);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyExpandMapRange failed: {ex}");
            }
        }

        /// <summary>
        /// ストレージの収集範囲・速度を強化（ON時に一度だけ適用、OFF時に復元）
        /// </summary>
        private void ApplyEnhanceStorage()
        {
            try
            {
                // enhanceStorageがONで、Reflectionキャッシュが未初期化の場合のみ初期化
                if (enhanceStorage && !reflectionCacheInitialized)
                {
                    Debug.Log("[ModMenu] Initializing reflection cache for Storage Enhancement...");
                    InitializeReflectionCache();
                    reflectionCacheInitialized = true;
                }

                // スライダーの値が変更されたかチェック
                if (enhanceStorage && enhanceStorageApplied)
                {
                    if (System.Math.Abs(pickupRangeMultiplier - lastPickupRangeMultiplier) > 0.01f ||
                        System.Math.Abs(pickupSpeedMultiplier - lastPickupSpeedMultiplier) > 0.01f ||
                        System.Math.Abs(storageCapacityMultiplier - lastStorageCapacityMultiplier) > 0.01f)
                    {
                        Debug.Log($"[ModMenu] Multiplier changed! Range: {lastPickupRangeMultiplier:F1}x -> {pickupRangeMultiplier:F1}x, Speed: {lastPickupSpeedMultiplier:F1}x -> {pickupSpeedMultiplier:F1}x, Capacity: {lastStorageCapacityMultiplier:F1}x -> {storageCapacityMultiplier:F1}x");

                        // まず元の容量に復元（累積バグ防止）
                        foreach (var kvp in originalCapacities)
                        {
                            if (kvp.Key == null) continue;
                            try
                            {
                                int numStacks = kvp.Key.NumStacks;
                                if (numStacks > 0)
                                {
                                    int originalCapacityPerStack = kvp.Value / numStacks;
                                    kvp.Key.OverrideStackCapacity(originalCapacityPerStack);
                                }
                            }
                            catch { }
                        }

                        // 再適用するためにフラグをリセット
                        enhanceStorageApplied = false;
                        // 元の値をクリア（再取得するため）
                        originalRanges.Clear();
                        originalStrengths.Clear();
                        originalCapacities.Clear();
                    }
                }

                var playerTank = Singleton.playerTank;
                if (playerTank == null)
                {
                    enhanceStorageApplied = false;
                    return;
                }

                var blockman = playerTank.blockman;
                if (blockman == null)
                {
                    enhanceStorageApplied = false;
                    return;
                }

                // enhanceStorageがONで未適用の場合のみ実行
                if (enhanceStorage && !enhanceStorageApplied)
                {
                    Debug.Log("[ModMenu] Applying storage enhancement...");

                    int pickupCount = 0;
                    int holderCount = 0;
                    int magnetCount = 0;

                    // ModuleItemPickupの設定（検索機能）
                    foreach (var pickup in blockman.IterateBlockComponents<ModuleItemPickup>())
                    {
                        if (pickup == null) continue;

                        try
                        {
                            // 元の値を保存
                            if (rangeField != null && !originalRanges.ContainsKey(pickup))
                            {
                                float originalRange = (float)rangeField.GetValue(pickup);
                                originalRanges[pickup] = originalRange;
                                // 収集範囲拡張（加算方式で軽量化：元の値 + (倍率-1)*5m）
                                // 例：1xなら+0m、5xなら+20m、10xなら+45m
                                float additionalRange = (pickupRangeMultiplier - 1.0f) * 5.0f;
                                rangeField.SetValue(pickup, originalRange + additionalRange);
                            }

                            // 収集速度向上（検索間隔を適度に短縮：0.2秒 = 約5フレーム毎）
                            // 0.01fだとほぼ毎フレーム検索して激重になる
                            float searchInterval = Mathf.Max(0.2f, 1.0f / pickupSpeedMultiplier);
                            if (intervalField != null)
                                intervalField.SetValue(pickup, searchInterval);

                            // 拾得後の最小間隔を短縮
                            if (pickupIntervalField != null)
                                pickupIntervalField.SetValue(pickup, searchInterval);

                            // 受け渡し後の最小間隔を短縮
                            if (handoverIntervalField != null)
                                handoverIntervalField.SetValue(pickup, searchInterval);

                            // PrePickupPeriodも短縮
                            if (prePickupField != null)
                                prePickupField.SetValue(pickup, searchInterval);

                            pickupCount++;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[ModMenu] Failed to apply ModuleItemPickup enhancement: {ex}");
                        }
                    }
                    Debug.Log($"[ModMenu] Enhanced {pickupCount} ModuleItemPickup components");

                    // ModuleItemHolderの競合期間を0に（即座に転送）+ スタック容量増加
                    foreach (var holder in blockman.IterateBlockComponents<ModuleItemHolder>())
                    {
                        if (holder == null) continue;

                        try
                        {
                            // PickupContentionPeriodを短縮（速度倍率に応じて調整）
                            float contentionPeriod = Mathf.Max(0.05f, 0.2f / pickupSpeedMultiplier);
                            if (contentionProp != null && contentionProp.CanWrite)
                                contentionProp.SetValue(holder, contentionPeriod);

                            // スタック容量を増加（NEW）
                            if (storageCapacityMultiplier > 1.0f)
                            {
                                // 元の容量を保存（GetTotalCapacityForLimiter()で現在の総容量を取得）
                                if (!originalCapacities.ContainsKey(holder))
                                {
                                    int originalTotalCapacity = holder.GetTotalCapacityForLimiter();
                                    originalCapacities[holder] = originalTotalCapacity;

                                    // スタック単位の容量を計算（総容量 / スタック数）
                                    int numStacks = holder.NumStacks;
                                    if (numStacks > 0)
                                    {
                                        int originalCapacityPerStack = originalTotalCapacity / numStacks;

                                        // 容量を倍率に応じて増加（OverrideStackCapacity()を使用）
                                        int newCapacityPerStack = Mathf.RoundToInt(originalCapacityPerStack * storageCapacityMultiplier);
                                        holder.OverrideStackCapacity(newCapacityPerStack);

                                        Debug.Log($"[ModMenu] Increased holder capacity: {originalTotalCapacity} -> {newCapacityPerStack * numStacks} ({storageCapacityMultiplier:F1}x)");
                                    }
                                }
                            }

                            holderCount++;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[ModMenu] Failed to apply ModuleItemHolder enhancement: {ex}");
                        }
                    }
                    Debug.Log($"[ModMenu] Enhanced {holderCount} ModuleItemHolder components");

                    // ModuleItemHolderMagnetの強化（マグネット式ストレージ）
                    foreach (var magnet in blockman.IterateBlockComponents<ModuleItemHolderMagnet>())
                    {
                        if (magnet == null) continue;

                        try
                        {
                            // 元の値を保存
                            if (strengthField != null && !originalStrengths.ContainsKey(magnet))
                            {
                                float originalStrength = (float)strengthField.GetValue(magnet);
                                originalStrengths[magnet] = originalStrength;
                                // マグネットの強さを増幅（対数スケールで軽量化）
                                // 最大でも元の値の3倍まで（物理演算の負荷を抑える）
                                float adjustedMultiplier = 1.0f + Mathf.Log(pickupSpeedMultiplier, 2.0f) * 0.5f;
                                adjustedMultiplier = Mathf.Min(adjustedMultiplier, 3.0f); // 最大3倍
                                strengthField.SetValue(magnet, originalStrength * adjustedMultiplier);
                            }

                            // 離すまでの時間を短縮（速度倍率に応じて調整）
                            float dropTime = Mathf.Max(0.05f, 0.2f / pickupSpeedMultiplier);
                            if (dropAfterField != null)
                                dropAfterField.SetValue(magnet, dropTime);

                            // 固定までの時間を短縮（速度倍率に応じて調整）
                            float glueTime = Mathf.Max(0.05f, 0.2f / pickupSpeedMultiplier);
                            if (glueField != null)
                                glueField.SetValue(magnet, glueTime);

                            magnetCount++;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[ModMenu] Failed to apply ModuleItemHolderMagnet enhancement: {ex}");
                        }
                    }
                    Debug.Log($"[ModMenu] Enhanced {magnetCount} ModuleItemHolderMagnet components");

                    // 現在の倍率を保存
                    lastPickupRangeMultiplier = pickupRangeMultiplier;
                    lastPickupSpeedMultiplier = pickupSpeedMultiplier;
                    lastStorageCapacityMultiplier = storageCapacityMultiplier;

                    enhanceStorageApplied = true;
                    Debug.Log($"[ModMenu] Storage enhancement applied successfully! Total: {pickupCount} pickups, {holderCount} holders, {magnetCount} magnets");
                }
                // enhanceStorageがOFFで適用済みの場合は復元
                else if (!enhanceStorage && enhanceStorageApplied)
                {
                    Debug.Log("[ModMenu] Restoring original storage values...");

                    // ModuleItemPickupの範囲を復元
                    foreach (var kvp in originalRanges)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            if (rangeField != null)
                                rangeField.SetValue(kvp.Key, kvp.Value);
                        }
                        catch { }
                    }
                    originalRanges.Clear();

                    // ModuleItemHolderMagnetの強度を復元
                    foreach (var kvp in originalStrengths)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            if (strengthField != null)
                                strengthField.SetValue(kvp.Key, kvp.Value);
                        }
                        catch { }
                    }
                    originalStrengths.Clear();

                    // ModuleItemHolderの容量を復元
                    foreach (var kvp in originalCapacities)
                    {
                        if (kvp.Key == null) continue;
                        try
                        {
                            // kvp.Valueは総容量なので、スタック数で割る
                            int numStacks = kvp.Key.NumStacks;
                            if (numStacks > 0)
                            {
                                int originalCapacityPerStack = kvp.Value / numStacks;
                                kvp.Key.OverrideStackCapacity(originalCapacityPerStack);
                            }
                        }
                        catch { }
                    }
                    originalCapacities.Clear();

                    enhanceStorageApplied = false;
                    Debug.Log("[ModMenu] Storage values restored");
                }
                // 毎フレームのタイマーリセットは削除（クラッシュの原因）
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyEnhanceStorage failed: {ex}");
                enhanceStorageApplied = false;
            }
        }

        /// <summary>
        /// GUI描画処理
        /// Unity IMGUIで描画
        /// </summary>
        private void OnGUI()
        {
            // ESP描画（メニューの表示状態に関係なく常に描画）
            if (showEnemyCABs)
            {
                DrawESP();
            }

            if (!showMenu) return;

            // ウィンドウを描画
            windowRect = GUI.Window(12345, windowRect, DrawWindow, "TerraTech ModMenu v1.0");
        }

        /// <summary>
        /// ウィンドウの内容を描画
        /// </summary>
        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical();

            // ヘッダー情報
            GUILayout.Label("TerraTech ModMenu v1.1 - Press INSERT to toggle", GUI.skin.box);
            GUILayout.Space(5);

            // タブボタン
            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle(currentTab == TabType.Cheats, "Cheats", GUI.skin.button))
                currentTab = TabType.Cheats;
            if (GUILayout.Toggle(currentTab == TabType.Tech, "Tech", GUI.skin.button))
                currentTab = TabType.Tech;
            if (GUILayout.Toggle(currentTab == TabType.Teleport, "Teleport", GUI.skin.button))
                currentTab = TabType.Teleport;
            if (GUILayout.Toggle(currentTab == TabType.ESP, "ESP", GUI.skin.button))
                currentTab = TabType.ESP;
            if (GUILayout.Toggle(currentTab == TabType.Settings, "Settings", GUI.skin.button))
                currentTab = TabType.Settings;
            if (GUILayout.Toggle(currentTab == TabType.Info, "Info", GUI.skin.button))
                currentTab = TabType.Info;
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            // スクロールビュー開始
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // 選択されたタブのコンテンツを描画
            switch (currentTab)
            {
                case TabType.Cheats:
                    DrawCheatsTab();
                    break;
                case TabType.Tech:
                    DrawTechTab();
                    break;
                case TabType.Teleport:
                    DrawTeleportTab();
                    break;
                case TabType.ESP:
                    DrawESPTab();
                    break;
                case TabType.Settings:
                    DrawSettingsTab();
                    break;
                case TabType.Info:
                    DrawInfoTab();
                    break;
            }

            // スクロールビュー終了
            GUILayout.EndScrollView();

            GUILayout.Space(10);

            // 閉じるボタン
            if (GUILayout.Button("Close Menu"))
            {
                // メニューを閉じる時は元のカーソル状態に復元
                showMenu = false;
                Cursor.visible = previousCursorVisible;
                Cursor.lockState = previousCursorLockState;
            }

            GUILayout.EndVertical();

            // ウィンドウをドラッグ可能にする
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        /// <summary>
        /// ヘッダー用のGUIStyleを取得
        /// </summary>
        private GUIStyle GetHeaderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 14;
            return style;
        }

        // ========================================
        // タブコンテンツ描画メソッド
        // ========================================

        /// <summary>
        /// Cheatsタブの内容を描画
        /// </summary>
        private void DrawCheatsTab()
        {
            GUILayout.Label("=== Basic Cheats ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("God Mode:", GUILayout.Width(150));
            godMode = GUILayout.Toggle(godMode, godMode ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("No Clip:", GUILayout.Width(150));
            noClip = GUILayout.Toggle(noClip, noClip ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Infinite Resources:", GUILayout.Width(150));
            infiniteResources = GUILayout.Toggle(infiniteResources, infiniteResources ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("One Hit Kill:", GUILayout.Width(150));
            oneHitKill = GUILayout.Toggle(oneHitKill, oneHitKill ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Destroy CAB Only:", GUILayout.Width(150));
            destroyCabOnly = GUILayout.Toggle(destroyCabOnly, destroyCabOnly ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Disable Enemy AI:", GUILayout.Width(150));
            disableEnemyAI = GUILayout.Toggle(disableEnemyAI, disableEnemyAI ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (disableEnemyAI)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"AI Disable Range: {(disableAIRange <= 0f ? "All" : $"{disableAIRange:F0}m")}", GUILayout.Width(200));
                GUILayout.EndHorizontal();
                disableAIRange = GUILayout.HorizontalSlider(disableAIRange, 0f, 1000f);

                GUILayout.Label("Tip: 0 = All enemies, >0 = Range limit", GUI.skin.box);
                GUILayout.Label($"Disabled AIs: {disabledAITechs.Count}", GUI.skin.box);
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Combat Assist ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Auto-Aim to CAB:", GUILayout.Width(150));
            autoAimToCAB = GUILayout.Toggle(autoAimToCAB, autoAimToCAB ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (autoAimToCAB)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Auto-Aim Range: {autoAimRange:F0}m", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                autoAimRange = GUILayout.HorizontalSlider(autoAimRange, 50f, 500f);
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Resources ===", GetHeaderStyle());

            if (GUILayout.Button("Add 10000 Money"))
            {
                AddMoney(10000);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Unlock All Blocks:", GUILayout.Width(150));
            unlockAllBlocks = GUILayout.Toggle(unlockAllBlocks, unlockAllBlocks ? "ON" : "OFF");
            if (unlockAllBlocks)
            {
                UnlockAllBlocks();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("=== Time Control ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Time Scale: {timeScale:F1}x", GUILayout.Width(150));
            GUILayout.EndHorizontal();

            timeScale = GUILayout.HorizontalSlider(timeScale, 0.1f, 5.0f);
            Time.timeScale = timeScale;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("0.5x")) timeScale = 0.5f;
            if (GUILayout.Button("1.0x")) timeScale = 1.0f;
            if (GUILayout.Button("2.0x")) timeScale = 2.0f;
            if (GUILayout.Button("5.0x")) timeScale = 5.0f;
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("=== Time of Day ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Time: {timeOfDay:F1}h ({GetTimeOfDayName(timeOfDay)})", GUILayout.Width(200));
            GUILayout.EndHorizontal();

            timeOfDay = GUILayout.HorizontalSlider(timeOfDay, 0f, 24f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Dawn (6h)")) timeOfDay = 6f;
            if (GUILayout.Button("Noon (12h)")) timeOfDay = 12f;
            if (GUILayout.Button("Dusk (18h)")) timeOfDay = 18f;
            if (GUILayout.Button("Night (0h)")) timeOfDay = 0f;
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("=== Weapon Enhancement ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Fire Rate: {fireRateMultiplier:F1}x", GUILayout.Width(150));
            GUILayout.EndHorizontal();

            fireRateMultiplier = GUILayout.HorizontalSlider(fireRateMultiplier, 0.1f, 10f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("0.5x")) fireRateMultiplier = 0.5f;
            if (GUILayout.Button("1.0x")) fireRateMultiplier = 1.0f;
            if (GUILayout.Button("2.0x")) fireRateMultiplier = 2.0f;
            if (GUILayout.Button("5.0x")) fireRateMultiplier = 5.0f;
            if (GUILayout.Button("10x")) fireRateMultiplier = 10f;
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("=== Auto Harvest ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Auto Harvest:", GUILayout.Width(150));
            autoHarvest = GUILayout.Toggle(autoHarvest, autoHarvest ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (autoHarvest)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Harvest Range: {autoHarvestRange:F0}m", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                autoHarvestRange = GUILayout.HorizontalSlider(autoHarvestRange, 10f, 200f);

                GUILayout.Label("Tip: Destroys all resources within range", GUI.skin.box);
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Enemy Tech Editor ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Tech Editor:", GUILayout.Width(150));
            showEnemyTechEditor = GUILayout.Toggle(showEnemyTechEditor, showEnemyTechEditor ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (showEnemyTechEditor)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Scan Range: {enemyTechScanRange:F0}m", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                enemyTechScanRange = GUILayout.HorizontalSlider(enemyTechScanRange, 50f, 500f);

                GUILayout.Space(5);

                if (GUILayout.Button("Scan Enemy Techs", GUILayout.Height(30)))
                {
                    List<Tank> enemyTechs = ScanEnemyTechs();

                    if (enemyTechs.Count > 0)
                    {
                        selectedEnemyTech = enemyTechs[0]; // 最も近い敵Techを選択
                        Debug.Log($"[ModMenu] Found {enemyTechs.Count} enemy techs. Selected: {selectedEnemyTech.name}");
                    }
                    else
                    {
                        selectedEnemyTech = null;
                        Debug.Log("[ModMenu] No enemy techs found in range");
                    }
                }

                if (selectedEnemyTech != null)
                {
                    GUILayout.Space(5);

                    Tank playerTank = Singleton.playerTank;
                    float distance = playerTank != null ? Vector3.Distance(playerTank.trans.position, selectedEnemyTech.trans.position) : 0f;

                    GUILayout.Label($"Selected Tech: {selectedEnemyTech.name}", GUI.skin.box);
                    GUILayout.Label($"Distance: {distance:F1}m | Blocks: {selectedEnemyTech.blockman.blockCount}", GUI.skin.box);

                    GUILayout.Space(5);

                    // Edit Mode controls
                    if (!isEditMode)
                    {
                        // Edit Modeに入るボタン
                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button("⚡ ENTER EDIT MODE ⚡", GUILayout.Height(40)))
                        {
                            EnterEditMode(selectedEnemyTech);
                        }
                        GUI.backgroundColor = Color.white;

                        GUILayout.Label("Enter Edit Mode to build on this tech with R key!", GUI.skin.box);

                        GUILayout.Space(5);

                        // ブロックリストをスクロール表示（マニュアルモード用）
                        GUILayout.Label("Manual Block Removal:", GetHeaderStyle());
                        enemyTechEditorScrollPos = GUILayout.BeginScrollView(enemyTechEditorScrollPos, GUILayout.Height(150));

                        var blocks = selectedEnemyTech.blockman.IterateBlocks();
                        int blockIndex = 0;

                        foreach (var block in blocks)
                        {
                            if (block == null) continue;

                            GUILayout.BeginHorizontal();

                            string blockName = block.name;
                            if (blockName.Contains("(Clone)"))
                            {
                                blockName = blockName.Replace("(Clone)", "").Trim();
                            }

                            GUILayout.Label($"{blockIndex + 1}. {blockName}", GUILayout.Width(250));

                            if (GUILayout.Button("Detach", GUILayout.Width(80)))
                            {
                                DetachBlockFromEnemyTech(selectedEnemyTech, block);
                            }

                            GUILayout.EndHorizontal();

                            blockIndex++;
                        }

                        GUILayout.EndScrollView();
                    }
                    else
                    {
                        // Edit Mode中の表示
                        GUI.backgroundColor = Color.yellow;
                        GUILayout.Label("🔧 EDIT MODE ACTIVE 🔧", GUI.skin.box);
                        GUI.backgroundColor = Color.white;

                        GUILayout.Label("You can now:", GUI.skin.box);
                        GUILayout.Label("• Press R to open block palette", GUI.skin.box);
                        GUILayout.Label("• Add/Remove blocks like your own tech", GUI.skin.box);
                        GUILayout.Label("• Camera follows this tech", GUI.skin.box);

                        GUILayout.Space(10);

                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("⚡ EXIT EDIT MODE ⚡", GUILayout.Height(40)))
                        {
                            ExitEditMode();
                        }
                        GUI.backgroundColor = Color.white;

                        GUILayout.Label("Warning: Exit to return to your tech!", GUI.skin.box);
                    }

                    GUILayout.Space(5);

                    if (!isEditMode && GUILayout.Button("Clear Selection"))
                    {
                        selectedEnemyTech = null;
                    }
                }
                else
                {
                    GUILayout.Label("No tech selected. Click 'Scan Enemy Techs' to find targets.", GUI.skin.box);
                }
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Infinite Collector ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Infinite Capacity:", GUILayout.Width(150));
            infiniteCollector = GUILayout.Toggle(infiniteCollector, infiniteCollector ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (infiniteCollector)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Collection Range: {collectorRangeMultiplier:F1}x", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                collectorRangeMultiplier = GUILayout.HorizontalSlider(collectorRangeMultiplier, 1.0f, 10.0f);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("1.0x")) collectorRangeMultiplier = 1.0f;
                if (GUILayout.Button("2.0x")) collectorRangeMultiplier = 2.0f;
                if (GUILayout.Button("5.0x")) collectorRangeMultiplier = 5.0f;
                if (GUILayout.Button("10x")) collectorRangeMultiplier = 10.0f;
                GUILayout.EndHorizontal();

                GUILayout.Label("Tip: Collectors can hold unlimited items!", GUI.skin.box);
            }
        }

        /// <summary>
        /// 時刻を人間が読める形式に変換
        /// </summary>
        private string GetTimeOfDayName(float hour)
        {
            if (hour >= 5f && hour < 7f) return "Dawn";
            if (hour >= 7f && hour < 17f) return "Day";
            if (hour >= 17f && hour < 19f) return "Dusk";
            return "Night";
        }

        /// <summary>
        /// Techタブの内容を描画
        /// </summary>
        private void DrawTechTab()
        {
            GUILayout.Label("=== Tech Spawner ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Spawn Count: {spawnCount}", GUILayout.Width(150));
            GUILayout.EndHorizontal();
            spawnCount = (int)GUILayout.HorizontalSlider(spawnCount, 1f, 10f);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Distance: {spawnDistance:F0}m", GUILayout.Width(150));
            GUILayout.EndHorizontal();
            spawnDistance = GUILayout.HorizontalSlider(spawnDistance, 10f, 100f);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Min Blocks: {minBlockCount}", GUILayout.Width(150));
            GUILayout.EndHorizontal();
            minBlockCount = (int)GUILayout.HorizontalSlider(minBlockCount, 1f, 200f);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Max Blocks: {maxBlockCount}", GUILayout.Width(150));
            GUILayout.EndHorizontal();
            maxBlockCount = (int)GUILayout.HorizontalSlider(maxBlockCount, 1f, 200f);

            // Min/Max の整合性チェック
            if (minBlockCount > maxBlockCount)
            {
                maxBlockCount = minBlockCount;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Team:", GUILayout.Width(150));
            if (GUILayout.Toggle(spawnTeam == 1, "Enemy", GUILayout.Width(80)))
            {
                spawnTeam = 1;
            }
            if (GUILayout.Toggle(spawnTeam == 0, "Friendly", GUILayout.Width(80)))
            {
                spawnTeam = 0;
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button($"Spawn {spawnCount} Random Tech(s)"))
            {
                SpawnRandomTech();
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Storage Enhancement ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enhance Storage:", GUILayout.Width(150));
            enhanceStorage = GUILayout.Toggle(enhanceStorage, enhanceStorage ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (enhanceStorage)
            {
                GUILayout.Label("Pickup Range Multiplier:", GUILayout.Width(200));
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{pickupRangeMultiplier:F1}x", GUILayout.Width(50));
                pickupRangeMultiplier = GUILayout.HorizontalSlider(pickupRangeMultiplier, 1.0f, 10.0f);
                GUILayout.EndHorizontal();

                GUILayout.Label("Pickup Speed Multiplier:", GUILayout.Width(200));
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{pickupSpeedMultiplier:F1}x", GUILayout.Width(50));
                pickupSpeedMultiplier = GUILayout.HorizontalSlider(pickupSpeedMultiplier, 1.0f, 10.0f);
                GUILayout.EndHorizontal();

                GUILayout.Label("Storage Capacity Multiplier:", GUILayout.Width(200));
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{storageCapacityMultiplier:F1}x", GUILayout.Width(50));
                storageCapacityMultiplier = GUILayout.HorizontalSlider(storageCapacityMultiplier, 1.0f, 20.0f);
                GUILayout.EndHorizontal();

                GUILayout.Space(5);
                GUILayout.Label("Tip: Capacity multiplier increases the number of blocks", GUI.skin.box);
                GUILayout.Label("that can be held in storage (e.g., 10x = 1000 blocks)", GUI.skin.box);
            }
        }

        /// <summary>
        /// Teleportタブの内容を描画
        /// </summary>
        private void DrawTeleportTab()
        {
            GUILayout.Label("=== Quick Teleport ===", GetHeaderStyle());

            if (GUILayout.Button("Teleport to 0,0,0"))
            {
                TeleportPlayer(Vector3.zero);
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Coordinate Input ===", GetHeaderStyle());

            if (GUILayout.Button(showCoordInput ? "Hide Coordinate Input" : "Show Coordinate Input"))
            {
                showCoordInput = !showCoordInput;
            }

            if (showCoordInput)
            {
                GUILayout.BeginVertical(GUI.skin.box);

                GUILayout.BeginHorizontal();
                GUILayout.Label("X:", GUILayout.Width(30));
                coordInputX = GUILayout.TextField(coordInputX, GUILayout.Width(100));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Y:", GUILayout.Width(30));
                coordInputY = GUILayout.TextField(coordInputY, GUILayout.Width(100));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Z:", GUILayout.Width(30));
                coordInputZ = GUILayout.TextField(coordInputZ, GUILayout.Width(100));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Teleport", GUILayout.Width(150)))
                {
                    try
                    {
                        float x = float.Parse(coordInputX);
                        float y = float.Parse(coordInputY);
                        float z = float.Parse(coordInputZ);
                        TeleportPlayer(new Vector3(x, y, z));
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ModMenu] Invalid coordinates: {ex.Message}");
                    }
                }

                if (GUILayout.Button("Set to Current Position", GUILayout.Width(150)))
                {
                    Tank playerTank = Singleton.playerTank;
                    if (playerTank != null)
                    {
                        Vector3 pos = playerTank.trans.position;
                        coordInputX = pos.x.ToString("F1");
                        coordInputY = pos.y.ToString("F1");
                        coordInputZ = pos.z.ToString("F1");
                    }
                }

                GUILayout.EndVertical();
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Waypoints ===", GetHeaderStyle());

            if (GUILayout.Button(showWaypoints ? "Hide Waypoints" : "Show Waypoints"))
            {
                showWaypoints = !showWaypoints;
            }

            if (showWaypoints)
            {
                DrawWaypointList();
            }
        }

        /// <summary>
        /// Settingsタブの内容を描画
        /// </summary>
        private void DrawSettingsTab()
        {
            GUILayout.Label("=== Map Settings ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Expand Map Range:", GUILayout.Width(150));
            expandMapRange = GUILayout.Toggle(expandMapRange, expandMapRange ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (expandMapRange)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Range Multiplier: {mapRangeMultiplier:F1}x", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                mapRangeMultiplier = GUILayout.HorizontalSlider(mapRangeMultiplier, 1.0f, 5.0f);
            }

            GUILayout.Space(10);

            GUILayout.Label("=== Storage Enhancement ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enhance Storage:", GUILayout.Width(150));
            enhanceStorage = GUILayout.Toggle(enhanceStorage, enhanceStorage ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (enhanceStorage)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Pickup Range: {pickupRangeMultiplier:F1}x", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                pickupRangeMultiplier = GUILayout.HorizontalSlider(pickupRangeMultiplier, 1.0f, 10.0f);

                GUILayout.BeginHorizontal();
                GUILayout.Label($"Pickup Speed: {pickupSpeedMultiplier:F1}x", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                pickupSpeedMultiplier = GUILayout.HorizontalSlider(pickupSpeedMultiplier, 1.0f, 10.0f);
            }
        }

        /// <summary>
        /// Infoタブの内容を描画
        /// </summary>
        private void DrawInfoTab()
        {
            GUILayout.Label("=== Performance ===", GetHeaderStyle());
            GUILayout.Label($"FPS: {(1.0f / Time.deltaTime):F0}");
            GUILayout.Label($"Time Scale: {Time.timeScale:F1}x");

            GUILayout.Space(10);

            GUILayout.Label("=== Player Info ===", GetHeaderStyle());
            GUILayout.Label($"Position: {GetPlayerPosition()}");

            Tank playerTank = Singleton.playerTank;
            if (playerTank != null)
            {
                GUILayout.Label($"Team: {playerTank.Team}");
                GUILayout.Label($"Anchored: {playerTank.IsAnchored}");
            }

            GUILayout.Space(10);

            GUILayout.Label("=== About ===", GetHeaderStyle());
            GUILayout.Label("TerraTech ModMenu v1.1");
            GUILayout.Label("Created by ModMenu Project");
            GUILayout.Label("Press INSERT to toggle menu");
        }

        /// <summary>
        /// ESPタブの内容を描画
        /// </summary>
        private void DrawESPTab()
        {
            GUILayout.Label("=== CAB Visualization (ESP) ===", GetHeaderStyle());

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Enemy CABs:", GUILayout.Width(150));
            showEnemyCABs = GUILayout.Toggle(showEnemyCABs, showEnemyCABs ? "ON" : "OFF");
            GUILayout.EndHorizontal();

            if (showEnemyCABs)
            {
                GUILayout.Space(10);

                GUILayout.Label("=== ESP Settings ===", GetHeaderStyle());

                GUILayout.BeginHorizontal();
                GUILayout.Label($"ESP Range: {espRange:F0}m", GUILayout.Width(150));
                GUILayout.EndHorizontal();
                espRange = GUILayout.HorizontalSlider(espRange, 50f, 1000f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Show Distance:", GUILayout.Width(150));
                espShowDistance = GUILayout.Toggle(espShowDistance, espShowDistance ? "ON" : "OFF");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Show Corp Name:", GUILayout.Width(150));
                espShowCorpName = GUILayout.Toggle(espShowCorpName, espShowCorpName ? "ON" : "OFF");
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.Label("=== ESP Info ===", GetHeaderStyle());
                GUILayout.Label($"Detected Enemy CABs: {cachedEnemyCABs.Count}");
                GUILayout.Label("Enemy CABs are highlighted with outlines");
                GUILayout.Label("Distance/name shown next to CAB blocks");
            }
        }

        /// <summary>
        /// ESP描画（キャッシュされた敵CAB情報を画面に表示）
        /// </summary>
        private void DrawESP()
        {
            try
            {
                if (Camera.main == null) return;

                // キャッシュされた敵CAB情報を描画
                foreach (var cabInfo in cachedEnemyCABs)
                {
                    if (cabInfo == null || cabInfo.cabBlock == null) continue;

                    // CABの3D位置を取得
                    Vector3 worldPos = cabInfo.position;

                    // 3D座標を2D画面座標に変換
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

                    // カメラの後ろにある場合はスキップ
                    if (screenPos.z < 0) continue;

                    // Unity IMGUIは左上原点、ScreenToWorldPointは左下原点なので変換
                    screenPos.y = Screen.height - screenPos.y;

                    // テキスト情報を描画（Outline表示があるのでテキストのみ）
                    string labelText = "";
                    if (espShowDistance)
                    {
                        labelText += $"{cabInfo.distance:F0}m";
                    }
                    if (espShowCorpName && !string.IsNullOrEmpty(cabInfo.corpName))
                    {
                        if (labelText.Length > 0) labelText += "\n";
                        labelText += cabInfo.corpName;
                    }

                    if (!string.IsNullOrEmpty(labelText))
                    {
                        // テキストのサイズを計算
                        GUIContent content = new GUIContent(labelText);
                        Vector2 textSize = GUI.skin.label.CalcSize(content);

                        // テキストを描画（CAB位置の右側に表示）
                        Rect textRect = new Rect(screenPos.x + 10, screenPos.y - textSize.y / 2, textSize.x + 10, textSize.y);

                        // 背景を描画（視認性向上）
                        GUI.color = new Color(0, 0, 0, 0.7f);
                        GUI.Box(textRect, "");

                        // テキストを描画（黄色で目立たせる）
                        GUI.color = Color.yellow;
                        GUI.Label(textRect, labelText);
                        GUI.color = Color.white;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] DrawESP failed: {ex}");
            }
        }

        /// <summary>
        /// 自動照準機能（プレイヤーの武器を最も近い敵CABに向ける）
        /// </summary>
        private void ApplyAutoAimToCAB()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // リフレクションを初回のみ初期化
                if (!weaponReflectionInitialized)
                {
                    InitializeWeaponReflection();
                }

                if (controlInputTargetedMethod == null) return;

                // 範囲内の最も近い敵CABを検索
                EnemyCabInfo closestCab = null;
                float closestDistance = float.MaxValue;

                foreach (var cabInfo in cachedEnemyCABs)
                {
                    if (cabInfo == null || cabInfo.cabBlock == null) continue;

                    // 範囲チェック
                    if (cabInfo.distance > autoAimRange) continue;

                    if (cabInfo.distance < closestDistance)
                    {
                        closestDistance = cabInfo.distance;
                        closestCab = cabInfo;
                    }
                }

                // 最も近い敵CABが見つからない場合は何もしない
                if (closestCab == null) return;

                Vector3 targetPosition = closestCab.position;

                // プレイヤーのTankから全ての武器を取得して照準
                var blockman = playerTank.blockman;
                if (blockman == null) return;

                foreach (var weapon in blockman.IterateBlockComponents<ModuleWeapon>())
                {
                    if (weapon == null) continue;

                    try
                    {
                        // ControlInputTargeted(Vector3 targetPosition, float distance)を呼び出し
                        controlInputTargetedMethod.Invoke(weapon, new object[] { targetPosition, closestDistance });
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ModMenu] Failed to control weapon: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyAutoAimToCAB failed: {ex}");
            }
        }

        /// <summary>
        /// 武器制御用のリフレクションを初期化
        /// </summary>
        private void InitializeWeaponReflection()
        {
            try
            {
                // ModuleWeapon.ControlInputTargeted(Vector3, float)メソッドを取得
                controlInputTargetedMethod = typeof(ModuleWeapon).GetMethod(
                    "ControlInputTargeted",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                    null,
                    new System.Type[] { typeof(Vector3), typeof(float) },
                    null
                );

                if (controlInputTargetedMethod != null)
                {
                    Debug.Log("[ModMenu] Weapon reflection initialized successfully");
                }
                else
                {
                    Debug.LogError("[ModMenu] Failed to find ControlInputTargeted method");
                }

                weaponReflectionInitialized = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] Failed to initialize weapon reflection: {ex}");
                weaponReflectionInitialized = true; // エラーでも再初期化を防ぐ
            }
        }

        /// <summary>
        /// 敵TechのAIを無効化（動きを止める）
        /// </summary>
        private void ApplyDisableEnemyAI()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
                if (manVisible == null) return;

                // disableEnemyAIがONの場合、敵AIを無効化
                if (disableEnemyAI)
                {
                    Vector3 playerPos = playerTank.trans.position;

                    // 範囲を決定（0 = 全範囲、それ以外 = 指定範囲）
                    float searchRange = (disableAIRange <= 0f) ? 10000f : disableAIRange;

                    // 指定範囲内の敵Techを検索
                    var searchIterator = manVisible.VisiblesTouchingRadius(
                        playerPos,
                        searchRange,
                        new Bitfield<ObjectTypes>(new ObjectTypes[] { ObjectTypes.Vehicle }),
                        false,
                        0
                    );

                    foreach (var visible in searchIterator)
                    {
                        if (visible != null && visible.tank != null)
                        {
                            Tank enemyTank = visible.tank;

                            // プレイヤー自身はスキップ
                            if (enemyTank == playerTank) continue;

                            // 敵かどうかチェック
                            if (enemyTank.IsEnemy(playerTank.Team))
                            {
                                try
                                {
                                    // TechAIコンポーネントを取得
                                    TechAI techAI = enemyTank.GetComponent<TechAI>();

                                    if (techAI != null)
                                    {
                                        // 新しい敵AIを記録
                                        if (!disabledAITechs.Contains(techAI))
                                        {
                                            disabledAITechs.Add(techAI);
                                            Debug.Log($"[ModMenu] Added AI to disable list: {enemyTank.name}");
                                        }

                                        // 毎フレーム、AIツリーを無効化
                                        var disableMethod = typeof(TechAI).GetMethod("DisableCurrentTree",
                                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                                        if (disableMethod != null)
                                        {
                                            disableMethod.Invoke(techAI, null);
                                        }

                                        // Rigidbodyを完全に停止（最も確実な方法）
                                        if (enemyTank.rbody != null)
                                        {
                                            // 速度を0に設定
                                            enemyTank.rbody.velocity = Vector3.zero;
                                            enemyTank.rbody.angularVelocity = Vector3.zero;

                                            // Kinematicモードにして物理演算を完全停止
                                            enemyTank.rbody.isKinematic = true;
                                        }

                                        // 全ての武器を無効化
                                        ModuleWeapon[] weapons = enemyTank.GetComponentsInChildren<ModuleWeapon>();
                                        foreach (var weapon in weapons)
                                        {
                                            if (weapon != null)
                                            {
                                                weapon.FireControl = false;
                                                weapon.enabled = false;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.LogError($"[ModMenu] Failed to disable enemy AI: {ex.Message}");
                                }
                            }
                        }
                    }
                }
                // disableEnemyAIがOFFの場合、全ての無効化したAIを復元
                else if (disabledAITechs.Count > 0)
                {
                    RestoreAllDisabledAI();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyDisableEnemyAI failed: {ex}");
            }
        }

        /// <summary>
        /// 無効化した全ての敵AIを復元
        /// </summary>
        private void RestoreAllDisabledAI()
        {
            try
            {
                Debug.Log($"[ModMenu] Restoring {disabledAITechs.Count} disabled AIs...");

                foreach (var techAI in disabledAITechs)
                {
                    if (techAI == null) continue;

                    try
                    {
                        // AIを再有効化（EnableCurrentTree()を使用）
                        var enableMethod = typeof(TechAI).GetMethod("EnableCurrentTree",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                        if (enableMethod != null)
                        {
                            enableMethod.Invoke(techAI, null);
                        }

                        // Rigidbodyの物理演算を復元
                        Tank enemyTank = techAI.GetComponent<Tank>();
                        if (enemyTank != null)
                        {
                            if (enemyTank.rbody != null)
                            {
                                enemyTank.rbody.isKinematic = false;
                            }

                            // 全ての武器を再有効化
                            ModuleWeapon[] weapons = enemyTank.GetComponentsInChildren<ModuleWeapon>();
                            foreach (var weapon in weapons)
                            {
                                if (weapon != null)
                                {
                                    weapon.enabled = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ModMenu] Failed to restore AI: {ex.Message}");
                    }
                }

                disabledAITechs.Clear();
                Debug.Log("[ModMenu] All disabled AIs restored");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] RestoreAllDisabledAI failed: {ex}");
            }
        }

        /// <summary>
        /// No Clip（壁抜け）を適用/解除
        /// </summary>
        private void ApplyNoClip()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // noClipがONで未適用の場合、コライダーを無効化
                if (noClip && !noClipApplied)
                {
                    Debug.Log("[ModMenu] Applying No Clip...");

                    // プレイヤーTankの全コライダーを取得
                    playerColliders.Clear();
                    Collider[] colliders = playerTank.GetComponentsInChildren<Collider>();

                    foreach (var collider in colliders)
                    {
                        if (collider != null && collider.enabled)
                        {
                            playerColliders.Add(collider);
                            collider.enabled = false;
                        }
                    }

                    noClipApplied = true;
                    Debug.Log($"[ModMenu] No Clip applied! Disabled {playerColliders.Count} colliders");
                }
                // noClipがOFFで適用済みの場合、コライダーを復元
                else if (!noClip && noClipApplied)
                {
                    Debug.Log("[ModMenu] Disabling No Clip...");

                    foreach (var collider in playerColliders)
                    {
                        if (collider != null)
                        {
                            collider.enabled = true;
                        }
                    }

                    playerColliders.Clear();
                    noClipApplied = false;
                    Debug.Log("[ModMenu] No Clip disabled! Colliders restored");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyNoClip failed: {ex}");
                noClipApplied = false;
            }
        }

        /// <summary>
        /// Fire Rate Multiplier（連射速度倍率）を適用
        /// </summary>
        private void ApplyFireRateMultiplier()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // プレイヤーTankの全武器を取得
                ModuleWeapon[] weapons = playerTank.GetComponentsInChildren<ModuleWeapon>();

                foreach (var weapon in weapons)
                {
                    if (weapon == null) continue;

                    // 元のfire rateを保存
                    if (!originalFireRates.ContainsKey(weapon))
                    {
                        originalFireRates[weapon] = weapon.m_ShotCooldown;
                    }

                    // fire rateを倍率で調整（cooldownを除算）
                    weapon.m_ShotCooldown = originalFireRates[weapon] / fireRateMultiplier;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyFireRateMultiplier failed: {ex}");
            }
        }

        /// <summary>
        /// Auto Harvest（自動採掘）を適用
        /// </summary>
        private void ApplyAutoHarvest()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                Vector3 playerPos = playerTank.trans.position;

                // 範囲内の資源を検索して破壊
                // ManVisible.VisiblesTouchingRadiusを使用して範囲内のオブジェクトを検索
                var visibles = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(
                    playerPos,
                    autoHarvestRange,
                    new Bitfield<ObjectTypes>()  // 全てのタイプ
                );

                foreach (var visible in visibles)
                {
                    if (visible == null) continue;

                    // Sceneryタイプ（木、岩など）のみ対象
                    if (visible.type != ObjectTypes.Scenery) continue;

                    // ResourceDispenserを優先的に使用（リソースドロップのため）
                    ResourceDispenser resdisp = visible.resdisp;
                    if (resdisp != null)
                    {
                        // RemoveFromWorldでリソースをスポーン
                        // spawnChunks=true でリソースドロップ
                        // neverRegrow=false で再生成を許可
                        resdisp.RemoveFromWorld(
                            spawnChunks: true,      // リソースをドロップ
                            neverRegrow: false,     // 再生成を許可
                            removeInstant: true,    // 即座に削除
                            removePersistentDamageStage: false
                        );
                    }
                    else
                    {
                        // ResourceDispenserがない場合は通常のダメージ処理
                        Damageable damageable = visible.damageable;
                        if (damageable != null && damageable.Health > 0)
                        {
                            ManDamage.DamageInfo damageInfo = new ManDamage.DamageInfo(
                                99999f,
                                ManDamage.DamageType.Standard,
                                playerTank,
                                null,
                                visible.centrePosition,
                                Vector3.zero,
                                0f,
                                0f
                            );

                            damageable.TryToDamage(damageInfo, true, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyAutoHarvest failed: {ex}");
            }
        }

        /// <summary>
        /// Time of Day（時間帯制御）を適用
        /// </summary>
        private void ApplyTimeOfDay()
        {
            try
            {
                var manTimeOfDay = Singleton.Manager<ManTimeOfDay>.inst;
                if (manTimeOfDay != null)
                {
                    // timeOfDayを時、分、秒に変換
                    int hour = Mathf.FloorToInt(timeOfDay);
                    int minute = Mathf.FloorToInt((timeOfDay - hour) * 60f);
                    int second = 0;

                    // TimeOfDayを設定（hour, minute, second, serverOnly）
                    manTimeOfDay.SetTimeOfDay(hour, minute, second, false);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyTimeOfDay failed: {ex}");
            }
        }

        /// <summary>
        /// 範囲内の敵Techをスキャンして取得
        /// </summary>
        private List<Tank> ScanEnemyTechs()
        {
            List<Tank> enemyTechs = new List<Tank>();

            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return enemyTechs;

                Vector3 playerPos = playerTank.trans.position;

                // 範囲内のTechを検索
                var visibles = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(
                    playerPos,
                    enemyTechScanRange,
                    new Bitfield<ObjectTypes>(new ObjectTypes[] { ObjectTypes.Vehicle })
                );

                foreach (var visible in visibles)
                {
                    if (visible == null || visible.tank == null) continue;

                    Tank tech = visible.tank;

                    // プレイヤー自身のTechは除外
                    if (tech == playerTank) continue;

                    // 敵かどうかをチェック
                    if (tech.IsEnemy(playerTank.Team))
                    {
                        enemyTechs.Add(tech);
                    }
                }

                // 距離でソート（近い順）
                enemyTechs.Sort((a, b) =>
                {
                    float distA = Vector3.Distance(playerPos, a.trans.position);
                    float distB = Vector3.Distance(playerPos, b.trans.position);
                    return distA.CompareTo(distB);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ScanEnemyTechs failed: {ex}");
            }

            return enemyTechs;
        }

        /// <summary>
        /// 敵Techからブロックを取り外す
        /// </summary>
        private void DetachBlockFromEnemyTech(Tank tech, TankBlock block)
        {
            try
            {
                if (tech == null || block == null) return;

                // ブロックをTechから取り外し
                tech.blockman.Detach(
                    block,
                    allowHeadlessTech: true,    // ヘッドレスTechを許可
                    rootTransfer: false,         // ルート転送なし
                    propagate: true,            // 接続ブロックにも伝播
                    detachCallback: null
                );

                Debug.Log($"[ModMenu] Detached block '{block.name}' from enemy tech");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] DetachBlockFromEnemyTech failed: {ex}");
            }
        }

        /// <summary>
        /// 敵Tech編集モードに入る
        /// </summary>
        private void EnterEditMode(Tank enemyTech)
        {
            try
            {
                if (enemyTech == null || isEditMode) return;

                Debug.Log($"[ModMenu] Entering Edit Mode for enemy tech: {enemyTech.name}");

                // 現在のプレイヤーTechを保存
                originalPlayerTech = Singleton.playerTank;

                // 敵Techの元のTeamを保存
                originalEnemyTeam = enemyTech.Team;

                // 敵TechをプレイヤーTeamに変更（これで編集可能になる）
                enemyTech.SetTeam(Singleton.Manager<ManPlayer>.inst.PlayerTeam, false);

                // プレイヤーの操作対象を敵Techに切り替え（正しいメソッド）
                Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(enemyTech, true);

                isEditMode = true;

                Debug.Log("[ModMenu] Edit Mode activated! You can now build on this tech with R key");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] EnterEditMode failed: {ex}");
                ExitEditMode(); // 失敗したら元に戻す
            }
        }

        /// <summary>
        /// 敵Tech編集モードから抜ける
        /// </summary>
        private void ExitEditMode()
        {
            try
            {
                if (!isEditMode) return;

                Debug.Log("[ModMenu] Exiting Edit Mode...");

                // 敵Techを元のTeamに戻す
                if (selectedEnemyTech != null && originalEnemyTeam != -1)
                {
                    selectedEnemyTech.SetTeam(originalEnemyTeam, false);
                    Debug.Log($"[ModMenu] Restored enemy tech team to: {originalEnemyTeam}");
                }

                // プレイヤーTechに戻す（正しいメソッド）
                if (originalPlayerTech != null)
                {
                    Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(originalPlayerTech, true);
                    Debug.Log("[ModMenu] Restored player tech");
                }

                isEditMode = false;
                originalEnemyTeam = -1;

                Debug.Log("[ModMenu] Edit Mode exited successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ExitEditMode failed: {ex}");
            }
        }

        /// <summary>
        /// Edit Modeの安全性をチェック（敵Techが破壊されたら自動で抜ける）
        /// </summary>
        private void CheckEditModeSafety()
        {
            try
            {
                if (!isEditMode) return;

                // 敵Techが破壊されたか、存在しなくなった場合
                if (selectedEnemyTech == null || !selectedEnemyTech.visible.isActive)
                {
                    Debug.LogWarning("[ModMenu] Enemy tech was destroyed! Auto-exiting Edit Mode...");
                    ExitEditMode();
                    return;
                }

                // プレイヤーTechがなくなった場合
                if (originalPlayerTech == null || !originalPlayerTech.visible.isActive)
                {
                    Debug.LogWarning("[ModMenu] Original player tech was lost! Resetting Edit Mode...");
                    isEditMode = false;
                    selectedEnemyTech = null;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] CheckEditModeSafety failed: {ex}");
            }
        }

        /// <summary>
        /// Infinite Collector（無限コレクター）を適用
        /// </summary>
        private void ApplyInfiniteCollector()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null) return;

                // プレイヤーTankの全ModuleItemHolderを取得
                ModuleItemHolder[] holders = playerTank.GetComponentsInChildren<ModuleItemHolder>();

                // m_CapacityPerStackフィールドへのリフレクションアクセス
                var capacityField = typeof(ModuleItemHolder).GetField("m_CapacityPerStack",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (capacityField == null)
                {
                    Debug.LogError("[ModMenu] m_CapacityPerStack field not found!");
                    return;
                }

                foreach (var holder in holders)
                {
                    if (holder == null) continue;

                    // 元の容量を保存
                    if (!originalCapacities.ContainsKey(holder))
                    {
                        int originalCapacity = (int)capacityField.GetValue(holder);
                        originalCapacities[holder] = originalCapacity;
                    }

                    if (infiniteCollector)
                    {
                        // 無限容量に設定（実質的に無制限）
                        capacityField.SetValue(holder, 999999);

                        // 収集範囲も拡大（ModuleItemPickupがある場合）
                        ModuleItemPickup pickup = holder.GetComponent<ModuleItemPickup>();
                        if (pickup != null && collectorRangeMultiplier > 1.0f)
                        {
                            // リフレクションでm_Radiusにアクセス
                            var radiusField = pickup.GetType().GetField("m_Radius",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);

                            if (radiusField != null)
                            {
                                float originalRadius = (float)radiusField.GetValue(pickup);
                                radiusField.SetValue(pickup, originalRadius * collectorRangeMultiplier);
                            }
                        }
                    }
                    else
                    {
                        // 元の容量に戻す
                        if (originalCapacities.ContainsKey(holder))
                        {
                            capacityField.SetValue(holder, originalCapacities[holder]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] ApplyInfiniteCollector failed: {ex}");
            }
        }

        // ========================================
        // 以下、チート機能の実装
        // ========================================

        /// <summary>
        /// お金を追加
        /// </summary>
        private void AddMoney(int amount)
        {
            try
            {
                if (Singleton.Manager<ManPlayer>.inst != null)
                {
                    Singleton.Manager<ManPlayer>.inst.AddMoney(amount);
                    Debug.Log($"[ModMenu] Added {amount} money! Current: {Singleton.Manager<ManPlayer>.inst.GetCurrentMoney()}");
                }
                else
                {
                    Debug.LogError("[ModMenu] ManPlayer instance not found!");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] AddMoney failed: {ex}");
            }
        }

        /// <summary>
        /// 全てのブロックをアンロック
        /// </summary>
        private void UnlockAllBlocks()
        {
            try
            {
                var manLicenses = Singleton.Manager<ManLicenses>.inst;
                if (manLicenses == null)
                {
                    Debug.LogError("[ModMenu] ManLicenses instance not found!");
                    return;
                }

                // SetBlockState(BlockTypes blockType, BlockState blockState)メソッドを取得
                var setBlockStateMethod = typeof(ManLicenses).GetMethod("SetBlockState",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (setBlockStateMethod == null)
                {
                    Debug.LogError("[ModMenu] SetBlockState method not found!");
                    return;
                }

                // BlockStateの型を取得
                var blockStateType = typeof(ManLicenses).GetNestedType("BlockState",
                    System.Reflection.BindingFlags.Public);

                if (blockStateType == null)
                {
                    Debug.LogError("[ModMenu] BlockState enum not found!");
                    return;
                }

                // BlockState.Discovered の値を取得（列挙型の値2）
                object discoveredState = System.Enum.ToObject(blockStateType, 2);

                int unlockedCount = 0;

                // 全てのBlockTypesを反復処理してDiscoveredに設定
                foreach (BlockTypes blockType in System.Enum.GetValues(typeof(BlockTypes)))
                {
                    try
                    {
                        setBlockStateMethod.Invoke(manLicenses, new object[] { blockType, discoveredState });
                        unlockedCount++;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"[ModMenu] Failed to unlock block {blockType}: {ex.Message}");
                    }
                }

                Debug.Log($"[ModMenu] Successfully unlocked {unlockedCount} blocks!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] UnlockAllBlocks failed: {ex}");
            }
        }

        /// <summary>
        /// ランダムなTechをスポーン
        /// </summary>
        private void SpawnRandomTech()
        {
            try
            {
                var manSpawn = Singleton.Manager<ManSpawn>.inst;
                var playerTank = Singleton.playerTank;

                if (manSpawn == null || playerTank == null)
                {
                    Debug.LogError("[ModMenu] ManSpawn or PlayerTank not found!");
                    return;
                }

                // プライベートフィールドm_AllTechPresetsにアクセス
                var presetsField = typeof(ManSpawn).GetField("m_AllTechPresets",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (presetsField == null)
                {
                    Debug.LogError("[ModMenu] m_AllTechPresets field not found!");
                    return;
                }

                var allPresets = presetsField.GetValue(manSpawn) as System.Collections.Generic.List<TankPreset>;

                if (allPresets == null || allPresets.Count == 0)
                {
                    Debug.LogError("[ModMenu] No tech presets available!");
                    return;
                }

                // ブロック数でフィルタリング
                var filteredPresets = new System.Collections.Generic.List<TankPreset>();
                foreach (var preset in allPresets)
                {
                    var techData = preset.GetTechDataFormatted();
                    int blockCount = techData.m_BlockSpecs.Count;

                    if (blockCount >= minBlockCount && blockCount <= maxBlockCount)
                    {
                        filteredPresets.Add(preset);
                    }
                }

                if (filteredPresets.Count == 0)
                {
                    Debug.LogError($"[ModMenu] No tech presets found with block count between {minBlockCount} and {maxBlockCount}!");
                    return;
                }

                Debug.Log($"[ModMenu] Filtered presets count: {filteredPresets.Count}");

                // 指定された数だけスポーン
                for (int i = 0; i < spawnCount; i++)
                {
                    // ランダムなプリセットを選択（フィルタリング済み）
                    var randomPreset = filteredPresets[UnityEngine.Random.Range(0, filteredPresets.Count)];
                    var techData = randomPreset.GetTechDataFormatted();

                    // スポーン位置計算（プレイヤー周辺のランダムな位置）
                    Vector3 offset = UnityEngine.Random.insideUnitSphere * spawnDistance;
                    offset.y = Mathf.Abs(offset.y) + 5f; // 地上より少し上
                    Vector3 spawnPos = playerTank.trans.position + offset;

                    Debug.Log($"[ModMenu] Attempting to spawn at {spawnPos}");

                    // TankSpawnParamsを作成（位置を指定できる）
                    var tankSpawnParamsType = typeof(ManSpawn).GetNestedType("TankSpawnParams",
                        System.Reflection.BindingFlags.Public);

                    if (tankSpawnParamsType == null)
                    {
                        Debug.LogError("[ModMenu] TankSpawnParams type not found!");
                        return;
                    }

                    var param = System.Activator.CreateInstance(tankSpawnParamsType);

                    // フィールドに値を設定（nullチェック追加）
                    var techDataField = tankSpawnParamsType.GetField("techData");
                    var blockIDsField = tankSpawnParamsType.GetField("blockIDs");
                    var teamIDField = tankSpawnParamsType.GetField("teamID");
                    var positionField = tankSpawnParamsType.GetField("position");
                    var rotationField = tankSpawnParamsType.GetField("rotation");
                    var placementField = tankSpawnParamsType.GetField("placement");

                    if (techDataField == null || teamIDField == null || positionField == null)
                    {
                        Debug.LogError($"[ModMenu] Required fields not found! techData={techDataField != null}, teamID={teamIDField != null}, position={positionField != null}");
                        return;
                    }

                    techDataField.SetValue(param, techData);
                    blockIDsField?.SetValue(param, null);
                    teamIDField.SetValue(param, spawnTeam);
                    positionField.SetValue(param, spawnPos);
                    rotationField?.SetValue(param, Quaternion.identity);

                    // placement = PlacedAtPosition (値2)を設定
                    if (placementField != null)
                    {
                        placementField.SetValue(param, 2); // PlacedAtPosition = 2
                    }

                    Debug.Log($"[ModMenu] TankSpawnParams created successfully");

                    // SpawnTankRefメソッドを呼び出し
                    var spawnMethod = typeof(ManSpawn).GetMethod("SpawnTankRef",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    if (spawnMethod == null)
                    {
                        Debug.LogError("[ModMenu] SpawnTankRef method not found!");
                        return;
                    }

                    var result = spawnMethod.Invoke(manSpawn, new object[] { param, true });
                    Debug.Log($"[ModMenu] Spawned tech: {randomPreset.name} ({techData.m_BlockSpecs.Count} blocks) at {spawnPos}, result={result}");
                }

                Debug.Log($"[ModMenu] Successfully spawned {spawnCount} tech(s)!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] SpawnRandomTech failed: {ex}");
            }
        }

        /// <summary>
        /// プレイヤーをテレポート（地形の10m上に安全に配置）
        /// </summary>
        private void TeleportPlayer(Vector3 targetPosition)
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank == null)
                {
                    Debug.LogError("[ModMenu] Player tank not found!");
                    return;
                }

                var manWorld = Singleton.Manager<ManWorld>.inst;
                if (manWorld == null)
                {
                    Debug.LogError("[ModMenu] ManWorld not found!");
                    return;
                }

                Vector3 finalPosition;

                // ManWorldのGetTerrainHeightで地形の高さを取得
                if (manWorld.GetTerrainHeight(targetPosition, out float terrainHeight))
                {
                    // 地形の高さ + 10m
                    finalPosition = new Vector3(targetPosition.x, terrainHeight + 10f, targetPosition.z);
                    Debug.Log($"[ModMenu] Terrain height at target: {terrainHeight}, teleporting 10m above to Y={finalPosition.y}");
                }
                else
                {
                    // 地形の高さが取得できない場合は目標位置のY + 10m（フォールバック）
                    finalPosition = new Vector3(targetPosition.x, targetPosition.y + 10f, targetPosition.z);
                    Debug.LogWarning($"[ModMenu] Could not get terrain height, using target Y + 10m: Y={finalPosition.y}");
                }

                // テレポート実行
                playerTank.trans.position = finalPosition;
                Debug.Log($"[ModMenu] Successfully teleported to {finalPosition}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] TeleportPlayer failed: {ex}");
            }
        }

        /// <summary>
        /// プレイヤーの現在位置を取得
        /// </summary>
        private string GetPlayerPosition()
        {
            try
            {
                Tank playerTank = Singleton.playerTank;
                if (playerTank != null)
                {
                    Vector3 pos = playerTank.trans.position;
                    return $"({pos.x:F1}, {pos.y:F1}, {pos.z:F1})";
                }
                return "N/A";
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] GetPlayerPosition failed: {ex}");
                return "Error";
            }
        }

        /// <summary>
        /// ウェイポイント一覧を描画（拡張版：座標、距離、方向表示）
        /// </summary>
        private void DrawWaypointList()
        {
            try
            {
                if (Singleton.Manager<ManVisible>.inst == null)
                {
                    GUILayout.Label("ManVisible not available");
                    return;
                }

                // プレイヤーの現在位置を取得
                Tank playerTank = Singleton.playerTank;
                Vector3 playerPos = playerTank != null ? playerTank.trans.position : Vector3.zero;

                var allTracked = Singleton.Manager<ManVisible>.inst.AllTrackedVisibles;
                int waypointCount = 0;

                foreach (var tracked in allTracked)
                {
                    if (tracked == null || tracked.visible == null) continue;

                    // ウェイポイントのみをフィルタ
                    if (tracked.visible.type == ObjectTypes.Waypoint)
                    {
                        waypointCount++;
                        Vector3 pos = tracked.visible.centrePosition;
                        string waypointName = tracked.visible.name;

                        // 距離を計算
                        float distance = Vector3.Distance(playerPos, pos);

                        // 方向を計算（N/S/E/W）
                        Vector3 direction = pos - playerPos;
                        string directionStr = GetDirectionString(direction);

                        // ウェイポイント情報を表示
                        GUILayout.BeginVertical(GUI.skin.box);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"{waypointName}", GUILayout.Width(200));
                        if (GUILayout.Button("TP", GUILayout.Width(40)))
                        {
                            // TeleportPlayer内で地形高度を考慮するので、そのまま渡す
                            TeleportPlayer(pos);
                        }
                        GUILayout.EndHorizontal();

                        // 座標と距離情報
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"Pos: ({pos.x:F1}, {pos.y:F1}, {pos.z:F1})", GUILayout.Width(200));
                        GUILayout.Label($"{distance:F0}m {directionStr}", GUILayout.Width(80));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        GUILayout.Space(5);
                    }
                }

                if (waypointCount == 0)
                {
                    GUILayout.Label("No game waypoints found");
                }
                else
                {
                    GUILayout.Label($"Total: {waypointCount} game waypoints", GetHeaderStyle());
                }

                GUILayout.Space(10);

                // カスタムウェイポイントセクション
                GUILayout.Label("=== Custom Waypoints ===", GetHeaderStyle());

                // カスタムウェイポイント作成ボタン
                if (GUILayout.Button(showCustomWaypointCreation ? "Hide Creation Panel" : "Create New Waypoint"))
                {
                    showCustomWaypointCreation = !showCustomWaypointCreation;
                }

                if (showCustomWaypointCreation)
                {
                    GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.Label("Waypoint Name:");
                    newWaypointName = GUILayout.TextField(newWaypointName, GUILayout.Width(200));

                    if (GUILayout.Button("Add Current Location", GUILayout.Width(200)))
                    {
                        AddCurrentLocationAsWaypoint();
                        showCustomWaypointCreation = false;
                    }
                    GUILayout.EndVertical();
                }

                GUILayout.Space(5);

                // カスタムウェイポイント一覧
                if (customWaypoints.waypoints.Count > 0)
                {
                    for (int i = 0; i < customWaypoints.waypoints.Count; i++)
                    {
                        CustomWaypoint wp = customWaypoints.waypoints[i];
                        Vector3 wpPos = wp.GetPosition();
                        float distance = Vector3.Distance(playerPos, wpPos);
                        Vector3 direction = wpPos - playerPos;
                        string directionStr = GetDirectionString(direction);

                        GUILayout.BeginVertical(GUI.skin.box);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"[Custom] {wp.name}", GUILayout.Width(150));
                        if (GUILayout.Button("TP", GUILayout.Width(40)))
                        {
                            // TeleportPlayer内で地形高度を考慮するので、そのまま渡す
                            TeleportPlayer(wpPos);
                        }
                        if (GUILayout.Button("X", GUILayout.Width(30)))
                        {
                            RemoveCustomWaypoint(i);
                            break; // foreachを抜ける（リストが変更されるため）
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"Pos: ({wpPos.x:F1}, {wpPos.y:F1}, {wpPos.z:F1})", GUILayout.Width(200));
                        GUILayout.Label($"{distance:F0}m {directionStr}", GUILayout.Width(80));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        GUILayout.Space(5);
                    }
                }
                else
                {
                    GUILayout.Label("No custom waypoints");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] DrawWaypointList failed: {ex}");
                GUILayout.Label($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// ベクトルから方向文字列を取得（N/NE/E/SE/S/SW/W/NW）
        /// </summary>
        private string GetDirectionString(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;

            if (angle >= 337.5f || angle < 22.5f) return "N";
            else if (angle >= 22.5f && angle < 67.5f) return "NE";
            else if (angle >= 67.5f && angle < 112.5f) return "E";
            else if (angle >= 112.5f && angle < 157.5f) return "SE";
            else if (angle >= 157.5f && angle < 202.5f) return "S";
            else if (angle >= 202.5f && angle < 247.5f) return "SW";
            else if (angle >= 247.5f && angle < 292.5f) return "W";
            else return "NW";
        }

        /// <summary>
        /// カスタムウェイポイントを読み込み
        /// </summary>
        private void LoadCustomWaypoints()
        {
            try
            {
                if (File.Exists(waypointSavePath))
                {
                    string[] lines = File.ReadAllLines(waypointSavePath);
                    customWaypoints.waypoints.Clear();
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 4)
                        {
                            string name = parts[0];
                            float x = float.Parse(parts[1]);
                            float y = float.Parse(parts[2]);
                            float z = float.Parse(parts[3]);
                            customWaypoints.waypoints.Add(new CustomWaypoint(name, new Vector3(x, y, z)));
                        }
                    }
                    Debug.Log($"[ModMenu] Loaded {customWaypoints.waypoints.Count} custom waypoints");
                }
                else
                {
                    Debug.Log("[ModMenu] No saved waypoints found, creating new list");
                    customWaypoints = new CustomWaypointList();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] Failed to load custom waypoints: {ex}");
                customWaypoints = new CustomWaypointList();
            }
        }

        /// <summary>
        /// カスタムウェイポイントを保存
        /// </summary>
        private void SaveCustomWaypoints()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var wp in customWaypoints.waypoints)
                {
                    lines.Add($"{wp.name}|{wp.x}|{wp.y}|{wp.z}");
                }
                File.WriteAllLines(waypointSavePath, lines.ToArray());
                Debug.Log($"[ModMenu] Saved {customWaypoints.waypoints.Count} custom waypoints");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModMenu] Failed to save custom waypoints: {ex}");
            }
        }

        /// <summary>
        /// 現在地をカスタムウェイポイントとして追加
        /// </summary>
        private void AddCurrentLocationAsWaypoint()
        {
            Tank playerTank = Singleton.playerTank;
            if (playerTank != null)
            {
                Vector3 currentPos = playerTank.trans.position;
                CustomWaypoint newWaypoint = new CustomWaypoint(newWaypointName, currentPos);
                customWaypoints.waypoints.Add(newWaypoint);
                SaveCustomWaypoints();
                Debug.Log($"[ModMenu] Added custom waypoint: {newWaypointName} at {currentPos}");
            }
        }

        /// <summary>
        /// カスタムウェイポイントを削除
        /// </summary>
        private void RemoveCustomWaypoint(int index)
        {
            if (index >= 0 && index < customWaypoints.waypoints.Count)
            {
                customWaypoints.waypoints.RemoveAt(index);
                SaveCustomWaypoints();
                Debug.Log($"[ModMenu] Removed custom waypoint at index {index}");
            }
        }
    }
}
