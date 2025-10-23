# TerraTech API Reference

このドキュメントは、TerraTech（Unity 2018.4.13f1）のModding用APIリファレンスです。
DLL Injection方式でModを作成する際に使用できる主要なクラスとメソッドをまとめています。

---

## 目次

1. [コアクラス](#コアクラス)
2. [マネージャークラス](#マネージャークラス)
3. [モジュールクラス](#モジュールクラス)
4. [ユーティリティ](#ユーティリティ)
5. [使用例](#使用例)

---

## コアクラス

### Singleton

TerraTechのシングルトンマネージャーシステム。

#### プロパティ

```csharp
static Tank playerTank
```
- プレイヤーの現在のTechを取得
- 例: `Tank playerTank = Singleton.playerTank;`

#### メソッド

```csharp
static T Manager<T>.inst
```
- マネージャーインスタンスを取得
- 例: `ManPlayer manPlayer = Singleton.Manager<ManPlayer>.inst;`

---

### Tank

Techを表すクラス。プレイヤーやAIのTechを管理。

#### プロパティ

```csharp
Transform trans { get; private set; }
```
- Techの Transform コンポーネント（位置、回転など）
- 読み取り専用（privateなセッター）
- 例: `Vector3 position = tank.trans.position;`

```csharp
BlockManager blockman { get; private set; }
```
- Techのブロック管理クラス（BlockManager型）
- 読み取り専用（privateなセッター）
- 例: `BlockManager blockman = tank.blockman;`

```csharp
Rigidbody rbody { get; private set; }
```
- TechのRigidbodyコンポーネント（物理演算）
- 例: `Vector3 velocity = tank.rbody.velocity;`

```csharp
Visible visible { get; private set; }
```
- Techの可視オブジェクトコンポーネント
- 例: `int id = tank.visible.ID;`

```csharp
int Team { get; }
```
- Techのチーム番号（0=プレイヤー、1=敵など）
- 例: `int team = tank.Team;`

```csharp
bool IsPlayer { get; }
```
- このTechがプレイヤーのTechかどうか
- 例: `if (tank.IsPlayer) { ... }`

```csharp
bool IsAnchored { get; }
```
- Techがアンカー（固定）されているか
- 例: `bool anchored = tank.IsAnchored;`

```csharp
Vector3 boundsCentreWorld { get; set; }
```
- Techのワールド空間における境界中心位置
- 設定可能
- 例: `Vector3 center = tank.boundsCentreWorld;`

```csharp
Bounds blockBounds { get; }
```
- Techのローカル空間におけるブロック境界
- 例: `Bounds bounds = tank.blockBounds;`

```csharp
Vector3 WorldCenterOfMass { get; }
```
- Techのワールド空間における重心位置
- 例: `Vector3 com = tank.WorldCenterOfMass;`

```csharp
TechControl control { get; private set; }
```
- Techの制御システム
- 例: `TechControl control = tank.control;`

```csharp
TechAI AI { get; private set; }
```
- TechのAIシステム
- 例: `TechAI ai = tank.AI;`

```csharp
TechAnchors Anchors { get; private set; }
```
- Techのアンカーシステム
- 例: `int numAnchored = tank.Anchors.NumAnchored;`

#### メソッド

```csharp
void SetInvulnerable(bool invulnerable, bool forever)
```
- Techを無敵状態にする
- `invulnerable`: 無敵にするかどうか
- `forever`: 永続的にするか（falseなら一時的）
- 例: `tank.SetInvulnerable(true, false);`

```csharp
void SetTeam(int teamID)
```
- Techのチームを設定
- `teamID`: チーム番号（0=プレイヤー、1=敵など）
- 例: `tank.SetTeam(1);`

```csharp
bool IsFriendly()
```
- Techがプレイヤーに友好的かどうか
- 戻り値: 友好的ならtrue
- 例: `if (tank.IsFriendly()) { ... }`

```csharp
bool IsEnemy()
```
- Techがプレイヤーの敵かどうか
- 戻り値: 敵ならtrue
- 例: `if (tank.IsEnemy()) { ... }`

```csharp
void ResetPhysics(bool SendEventUpdate = false)
```
- Techの物理演算をリセット（質量、慣性、重心を再計算）
- `SendEventUpdate`: イベントを送信するか
- 例: `tank.ResetPhysics(true);`

```csharp
int GetValue()
```
- Techの総価値を取得（全ブロックの購入価格の合計）
- 戻り値: Tech の総価値
- 例: `int value = tank.GetValue();`

---

### TankBlockMan

Techのブロックを管理するクラス。

#### メソッド

```csharp
IEnumerable<T> IterateBlockComponents<T>() where T : MonoBehaviour
```
- Tech上の特定のコンポーネントを反復処理
- 例: `foreach (var module in blockman.IterateBlockComponents<ModuleItemPickup>()) { ... }`

---

## マネージャークラス

### ManPlayer

プレイヤー管理クラス（シングルトン）。お金、インベントリ、パレット、心臓ブロックなどを管理。

#### 取得方法
```csharp
ManPlayer manPlayer = Singleton.Manager<ManPlayer>.inst;
```

#### プロパティ

```csharp
IInventory<BlockTypes> PlayerInventory { get; }
```
- プレイヤーのブロックインベントリ
- 例: `var inventory = manPlayer.PlayerInventory;`

```csharp
bool InventoryIsUnrestricted { get; }
```
- インベントリが無制限かどうか（クリエイティブモード）
- 例: `bool unlimited = manPlayer.InventoryIsUnrestricted;`

```csharp
bool HasHeartBlock { get; }
```
- プレイヤーがハートブロック（リスポーン地点）を持っているか
- 例: `bool hasHeart = manPlayer.HasHeartBlock;`

```csharp
IEnumerable<WorldPosition> PlayerDeathLocations { get; }
```
- プレイヤーの死亡地点一覧
- 例: `foreach (var pos in manPlayer.PlayerDeathLocations) { ... }`

```csharp
bool PaletteUnlocked { get; }
```
- パレット（ブロックメニュー）がアンロックされているか
- 例: `bool unlocked = manPlayer.PaletteUnlocked;`

```csharp
bool PlayerIndestructible { get; set; }
```
- プレイヤーが破壊不能かどうか（チート設定）
- 読み書き可能
- 例: `manPlayer.PlayerIndestructible = true;`

```csharp
int PlayerTeam { get; }
```
- プレイヤーのチーム番号
- マルチプレイヤーでのみ意味がある
- 例: `int team = manPlayer.PlayerTeam;`

#### メソッド

```csharp
void AddMoney(int amount)
```
- プレイヤーにお金を追加（ホストのみ）
- `amount`: 追加する金額
- 統計追跡とSE再生も含む
- 例: `manPlayer.AddMoney(1000);`

```csharp
void PayMoney(int amount)
```
- プレイヤーのお金を支払う（減らす）
- `amount`: 支払う金額（所持金以上は支払えない）
- 統計追跡とSE再生も含む
- 例: `manPlayer.PayMoney(500);`

```csharp
int GetCurrentMoney()
```
- 現在のお金を取得
- 戻り値: 現在の所持金
- 例: `int money = manPlayer.GetCurrentMoney();`

```csharp
bool CanAfford(int amount)
```
- 指定額を支払えるかチェック
- `amount`: チェックする金額
- 戻り値: 支払い可能ならtrue
- 例: `if (manPlayer.CanAfford(1000)) { ... }`

```csharp
void Debug_SetMoney(int newMoney)
```
- お金を直接設定（デバッグ用、ホストのみ）
- `newMoney`: 新しい金額
- 例: `manPlayer.Debug_SetMoney(999999);`

```csharp
void EnablePalette(bool enable, bool usePlayerInventory = true)
```
- パレット（ブロックメニュー）の有効化/無効化
- `enable`: 有効化するか
- `usePlayerInventory`: プレイヤーインベントリを使用するか
- 例: `manPlayer.EnablePalette(true);`

```csharp
void AddBlockToInventory(BlockTypes blockType)
```
- プレイヤーインベントリにブロックを追加
- `blockType`: 追加するブロックの種類
- 例: `manPlayer.AddBlockToInventory(BlockTypes.GSOCockpit_111);`

```csharp
void TrackTechWithHeart(Tank tech)
```
- ハートブロックを持つTechを追跡開始
- `tech`: 追跡するTech
- 例: `manPlayer.TrackTechWithHeart(tank);`

```csharp
TrackedVisible GetNearestHeartBlock(Vector3 position)
```
- 指定位置に最も近いハートブロックを取得
- `position`: 基準位置
- 戻り値: 最も近いハートブロックのTrackedVisible（なければnull）
- 例: `var nearest = manPlayer.GetNearestHeartBlock(playerPos);`

```csharp
bool DoesTechHavePlayerHeartBlock(Tank tech)
```
- 指定Techがプレイヤーのハートブロックを持っているかチェック
- `tech`: チェックするTech
- 戻り値: ハートブロックを持っていればtrue
- 例: `if (manPlayer.DoesTechHavePlayerHeartBlock(tank)) { ... }`

```csharp
void AddDeathLocation(Vector3 position)
```
- プレイヤーの死亡地点を記録
- `position`: 死亡地点の位置
- 例: `manPlayer.AddDeathLocation(tankPos);`

#### プライベートフィールド（Reflection経由）

```csharp
// m_SaveDataフィールド（リフレクション経由でアクセス）
var saveDataField = typeof(ManPlayer).GetField("m_SaveData",
    BindingFlags.NonPublic | BindingFlags.Instance);
var saveData = saveDataField.GetValue(manPlayer);

// SaveData内部クラス構造:
// - m_Money: int（お金）
// - m_Inventory: IInventory<BlockTypes>（ブロックインベントリ）
// - m_PaletteUnlocked: bool（パレットアンロック状態）
// - m_PlayerIndestructible: bool（破壊不能状態）
// - m_TrackedIDs: List<int>（追跡中のID）
// - m_HotswapMap: HotswapMap（ホットスワップ設定）
// - m_PlayerDeathLocations: List<WorldPosition>（死亡地点）
// - m_HasEnabledCheatCommands: bool（チート有効化フラグ）

// Inventoryへのアクセス例
var inventoryField = saveData.GetType().GetField("m_Inventory");
var inventory = inventoryField.GetValue(saveData) as IInventory<BlockTypes>;
```

---

### ManSpawn

Tech・敵のスポーン管理クラス（シングルトン）。Techやブロックのスポーンを管理。

#### 取得方法
```csharp
ManSpawn manSpawn = Singleton.Manager<ManSpawn>.inst;
```

#### メソッド

```csharp
TrackedVisible SpawnTankRef(TankSpawnParams param, bool addToObjectManager)
```
- Techをスポーンする
- `param`: スポーンパラメータ（TankSpawnParams構造体）
- `addToObjectManager`: オブジェクトマネージャーに追加するか
- 戻り値: スポーンされたTechのTrackedVisible（失敗時はnull）
- 例: `var tracked = manSpawn.SpawnTankRef(param, true);`

```csharp
TrackedVisible SpawnItemRef(ItemTypeInfo itemTypeInfo, Vector3 position,
                             Quaternion rotation, bool addToObjectManager,
                             bool forceSpawn)
```
- ブロックやアイテムをスポーンする
- `itemTypeInfo`: アイテムタイプ情報
- `position`: スポーン位置
- `rotation`: スポーン時の回転
- `addToObjectManager`: オブジェクトマネージャーに追加するか
- `forceSpawn`: 強制的にスポーンするか
- 戻り値: スポーンされたアイテムのTrackedVisible
- 例: `var item = manSpawn.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Block, (int)blockType), pos, Quaternion.identity, true, false);`

```csharp
void RemoveScenery(Vector3 position, float radius, bool removeResources,
                   SceneryRemovalFlags flags)
```
- 指定範囲内のシーナリー（岩、木など）を削除
- `position`: 削除範囲の中心位置
- `radius`: 削除範囲の半径
- `removeResources`: リソースも削除するか
- `flags`: 削除フラグ（チャンク生成なし、再生成防止など）
- 例: `manSpawn.RemoveScenery(tankPos, 50f, true, SceneryRemovalFlags.PreventRegrow);`

#### 構造体

**TankSpawnParams**

Techをスポーンする際のパラメータ構造体。全てpublicフィールド。

```csharp
// 直接構造体を使用（推奨）
ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
{
    techData = techData,          // TechData: Tech設計図
    blockIDs = null,              // int[]: 特定ブロックIDを使用（通常null）
    teamID = 1,                   // int: チームID（0=味方、1=敵）
    position = spawnPos,          // Vector3: スポーン位置
    rotation = Quaternion.identity, // Quaternion: スポーン時の回転
    placement = ManSpawn.TankSpawnParams.Placement.PlacedAtPosition, // 配置方法
    hideMarker = false,           // bool: レーダーマーカーを非表示にするか
    inventory = null,             // IInventory<BlockTypes>: 使用するインベントリ（通常null）
    isPopulation = false,         // bool: 人口（ワールドスポーン）Tech扱いか
    isInvulnerable = false,       // bool: 無敵状態でスポーンするか
    grounded = true,              // bool: 地面に配置するか
    forceSpawn = false,           // bool: 強制的にスポーンするか
    ignoreSceneryOnSpawnProjection = false, // bool: スポーン時にシーナリーを無視するか
    hasRewardValue = true,        // bool: 報酬価値を持つか（倒した時にお金をドロップ）
    shouldExplodeDetachingBlocks = false, // bool: ブロック脱落時に爆発するか
    explodeDetachingBlocksDelay = 0f      // float: ブロック脱落から爆発までの遅延
};

// Reflectionを使用（非推奨、互換性のため残す）
var tankSpawnParamsType = typeof(ManSpawn).GetNestedType("TankSpawnParams", BindingFlags.Public);
var param = System.Activator.CreateInstance(tankSpawnParamsType);
tankSpawnParamsType.GetField("techData").SetValue(param, techData);
tankSpawnParamsType.GetField("teamID").SetValue(param, 1);
tankSpawnParamsType.GetField("position").SetValue(param, spawnPosition);
tankSpawnParamsType.GetField("rotation").SetValue(param, Quaternion.identity);
tankSpawnParamsType.GetField("placement").SetValue(param, 2); // PlacedAtPosition = 2
```

**TankSpawnParams.Placement列挙型**
```csharp
ManSpawn.TankSpawnParams.Placement.BaseCentredAtPosition        // = 0: ベースブロック中心を位置に合わせる
ManSpawn.TankSpawnParams.Placement.BoundsCentredAtPosition      // = 1: Tech全体の中心を位置に合わせる
ManSpawn.TankSpawnParams.Placement.PlacedAtPosition             // = 2: 指定位置に配置（通常これを使用）
```

**SceneryRemovalFlags列挙型**
```csharp
ManSpawn.SceneryRemovalFlags.SpawnNoChunks           // = 1: チャンクを生成しない
ManSpawn.SceneryRemovalFlags.PreventRegrow           // = 2: 再生成を防止
ManSpawn.SceneryRemovalFlags.RemoveInstant           // = 4: 即座に削除
ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage // = 8: 永続ダメージステージを削除
```

#### プライベートフィールド（Reflection経由）

```csharp
// m_AllTechPresetsフィールド（全Techプリセット）
var presetsField = typeof(ManSpawn).GetField("m_AllTechPresets",
    BindingFlags.NonPublic | BindingFlags.Instance);
var allPresets = presetsField.GetValue(manSpawn) as IEnumerable;

// プリセットの反復処理
foreach (var preset in allPresets)
{
    TechData techData = preset as TechData;
    if (techData != null)
    {
        Debug.Log($"Tech: {techData.Name}");
    }
}
```

---

### ManVisible

可視オブジェクト（ウェイポイント、Tech等）管理クラス（シングルトン）。TrackedVisibleの管理やコリジョン検索を行う。

#### 取得方法
```csharp
ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
```

#### プロパティ

```csharp
IEnumerable<TrackedVisible> AllTrackedVisibles { get; }
```
- すべての追跡中の可視オブジェクト
- 例: `foreach (var tracked in manVisible.AllTrackedVisibles) { ... }`

```csharp
IEnumerable<int> UnsavedTrackedVisibleIDs { get; }
```
- セーブされない追跡中のオブジェクトID一覧
- 例: `var unsavedIDs = manVisible.UnsavedTrackedVisibleIDs;`

```csharp
int VisiblePickerMask { get; private set; }
```
- 可視オブジェクト検索用のレイヤーマスク
- Tank、Cosmetic、Scenery、Pickupレイヤーを含む
- 例: `int mask = manVisible.VisiblePickerMask;`

```csharp
int VisiblePickerMaskNoTechs { get; private set; }
```
- Tech以外の可視オブジェクト検索用レイヤーマスク
- Tank、Scenery、Pickupレイヤーを含む（Cosmeticを除外）
- 例: `int mask = manVisible.VisiblePickerMaskNoTechs;`

#### メソッド

```csharp
void TrackVisible(TrackedVisible refToTrack, bool addToUnsavedList = false)
```
- 可視オブジェクトの追跡を開始
- `refToTrack`: 追跡するTrackedVisible
- `addToUnsavedList`: セーブしないリストに追加するか
- 例: `manVisible.TrackVisible(trackedVisible);`

```csharp
void StopTrackingVisible(int ID)
```
- 可視オブジェクトの追跡を停止
- `ID`: 停止するオブジェクトのID
- 例: `manVisible.StopTrackingVisible(visibleID);`

```csharp
TrackedVisible GetTrackedVisible(int ID)
```
- IDから追跡中の可視オブジェクトを取得
- `ID`: オブジェクトのID
- 戻り値: TrackedVisible（見つからない場合null）
- 例: `var tracked = manVisible.GetTrackedVisible(id);`

```csharp
TrackedVisible GetTrackedVisibleByHostID(int hostID)
```
- ホストIDから追跡中の可視オブジェクトを取得（マルチプレイヤー用）
- `hostID`: ホストが割り当てたID
- 戻り値: TrackedVisible（見つからない場合null）
- 例: `var tracked = manVisible.GetTrackedVisibleByHostID(hostID);`

```csharp
SearchIterator VisiblesTouchingRadius(Vector3 scenePos, float radius,
                                      Bitfield<ObjectTypes> types,
                                      bool includeTriggers = false,
                                      int pickerMask = 0)
```
- 指定範囲内の可視オブジェクトを検索
- `scenePos`: 検索範囲の中心位置
- `radius`: 検索半径
- `types`: 検索するオブジェクトタイプ（Bitfield）
- `includeTriggers`: トリガーコライダーを含むか
- `pickerMask`: カスタムレイヤーマスク（0なら自動設定）
- 戻り値: 検索結果のイテレータ
- 例: `var iterator = manVisible.VisiblesTouchingRadius(pos, 50f, new Bitfield<ObjectTypes>(ObjectTypes.Block));`

```csharp
Visible FindVisible(Collider c)
```
- Colliderから対応するVisibleを検索
- `c`: 検索するCollider
- 戻り値: Visible（見つからない場合null）
- 例: `var visible = manVisible.FindVisible(collider);`

```csharp
void ObliterateTrackedVisibleFromWorld(TrackedVisible trackedVis)
```
- 追跡中の可視オブジェクトを完全に削除
- ワールドから削除し、追跡も停止する
- `trackedVis`: 削除するTrackedVisible
- 例: `manVisible.ObliterateTrackedVisibleFromWorld(tracked);`

```csharp
void RegisterColliderToVisibleLookup(Visible v, Collider c)
```
- ColliderとVisibleの対応関係を登録
- `v`: Visible
- `c`: Collider
- 通常は自動的に呼ばれる
- 例: `manVisible.RegisterColliderToVisibleLookup(visible, collider);`

```csharp
void UnregisterColliderToVisibleLookup(Collider c)
```
- ColliderとVisibleの対応関係を解除
- `c`: Collider
- 通常は自動的に呼ばれる
- 例: `manVisible.UnregisterColliderToVisibleLookup(collider);`

#### イベント

```csharp
Event<TrackedVisible> OnStartedTrackingVisible
```
- 追跡開始時に発火するイベント
- 例: `manVisible.OnStartedTrackingVisible.Subscribe((tv) => Debug.Log($"Started tracking {tv.ID}"));`

```csharp
Event<TrackedVisible> OnStoppedTrackingVisible
```
- 追跡停止時に発火するイベント
- 例: `manVisible.OnStoppedTrackingVisible.Subscribe((tv) => Debug.Log($"Stopped tracking {tv.ID}"));`

#### TrackedVisible クラス

追跡中の可視オブジェクトを表すクラス。

```csharp
// TrackedVisibleの主要プロパティ
int ID { get; }                      // オブジェクトの一意ID
int HostID { get; set; }             // ホストが割り当てたID（マルチプレイヤー用）
ObjectTypes ObjectType { get; }      // オブジェクトタイプ（Waypoint、Tank等）
RadarTypes RadarType { get; set; }   // レーダー表示タイプ
int TeamID { get; set; }             // チームID
int RadarTeamID { get; set; }        // レーダー表示用チームID
Visible visible { get; }             // 対応するVisibleオブジェクト
Vector3 Position { get; }            // 現在位置（ワールド座標）
WorldPosition GetWorldPosition()     // ワールド位置を取得
bool IsQuestObject { get; set; }     // クエストオブジェクトか
```

#### Visible クラス

可視オブジェクトの基底クラス。

```csharp
// Visibleオブジェクトの主要プロパティ
int ID { get; }                      // オブジェクトの一意ID
ObjectTypes type { get; }            // ObjectTypes（Waypoint, Tank, Block等）
string name { get; }                 // オブジェクト名
int ItemType { get; }                // アイテムタイプ（ブロックタイプなど）
Vector3 centrePosition { get; }      // 中心位置（ワールド座標）
Transform trans { get; }             // Transformコンポーネント
bool isActive { get; }               // アクティブ状態か
bool Killed { get; }                 // 破壊されたか
bool CanBeCollected { get; }         // 収集可能か
float Radius { get; }                // オブジェクトの半径

// オブジェクトタイプ別プロパティ
Tank tank { get; }                   // type == ObjectTypes.Vehicle の場合のみ有効
TankBlock block { get; }             // type == ObjectTypes.Block の場合のみ有効
Waypoint Waypoint { get; }           // type == ObjectTypes.Waypoint の場合のみ有効
Crate crate { get; }                 // type == ObjectTypes.Crate の場合のみ有効
ResourcePickup pickup { get; }       // type == ObjectTypes.Chunk の場合のみ有効

// 例
if (visible.type == ObjectTypes.Waypoint)
{
    string name = visible.name;
    Vector3 pos = visible.centrePosition;
    Debug.Log($"Waypoint '{name}' at {pos}");
}
```

---

### ManWorldTreadmill

ワールドの生成範囲を管理するクラス（シングルトン）。

#### 取得方法
```csharp
ManWorldTreadmill treadmill = Singleton.Manager<ManWorldTreadmill>.inst;
```

#### プライベートフィールド（Reflection経由）

```csharp
// m_DistanceFromOriginBeforeMoveフィールド（マップ生成範囲）
var field = typeof(ManWorldTreadmill).GetField("m_DistanceFromOriginBeforeMove",
    BindingFlags.NonPublic | BindingFlags.Instance);
float currentValue = (float)field.GetValue(treadmill);
field.SetValue(treadmill, currentValue * multiplier);
```

---

## モジュールクラス

### ModuleItemPickup

アイテム収集モジュールのクラス（ストレージブロックの検索機能）。

#### プライベートフィールド（Reflection経由）

```csharp
// m_PickupRange（収集範囲）
var rangeField = typeof(ModuleItemPickup).GetField("m_PickupRange",
    BindingFlags.NonPublic | BindingFlags.Instance);
float currentRange = (float)rangeField.GetValue(pickup);
rangeField.SetValue(pickup, newRange);

// m_VisionRefreshInterval（検索間隔）
var intervalField = typeof(ModuleItemPickup).GetField("m_VisionRefreshInterval",
    BindingFlags.NonPublic | BindingFlags.Instance);
intervalField.SetValue(pickup, 0.2f);

// m_VisionRefreshTimer（検索タイマー）
var timerField = typeof(ModuleItemPickup).GetField("m_VisionRefreshTimer",
    BindingFlags.NonPublic | BindingFlags.Instance);
timerField.SetValue(pickup, 0f);

// m_PickupAfterMinInterval（拾得後の最小間隔）
var pickupIntervalField = typeof(ModuleItemPickup).GetField("m_PickupAfterMinInterval",
    BindingFlags.NonPublic | BindingFlags.Instance);

// m_HandoverAfterMinInterval（受け渡し後の最小間隔）
var handoverIntervalField = typeof(ModuleItemPickup).GetField("m_HandoverAfterMinInterval",
    BindingFlags.NonPublic | BindingFlags.Instance);

// m_PrePickupPeriod（事前ピックアップ期間）
var prePickupField = typeof(ModuleItemPickup).GetField("m_PrePickupPeriod",
    BindingFlags.NonPublic | BindingFlags.Instance);
```

---

### ModuleItemHolder

アイテムホルダーモジュールのクラス（ストレージブロックの転送機能）。

#### プロパティ

```csharp
float PickupContentionPeriod { get; set; }
```
- アイテムをピックアップする際の競合期間
- 例: `holder.PickupContentionPeriod = 0.05f;`

---

### ModuleItemHolderMagnet

マグネット式アイテムホルダーモジュールのクラス（磁力でアイテムを吸引）。

#### プライベートフィールド（Reflection経由）

```csharp
// m_Strength（マグネットの強さ）
var strengthField = typeof(ModuleItemHolderMagnet).GetField("m_Strength",
    BindingFlags.NonPublic | BindingFlags.Instance);
float strength = (float)strengthField.GetValue(magnet);
strengthField.SetValue(magnet, strength * multiplier);

// m_DropAfterMinTime（アイテムを離すまでの最小時間）
var dropAfterField = typeof(ModuleItemHolderMagnet).GetField("m_DropAfterMinTime",
    BindingFlags.NonPublic | BindingFlags.Instance);

// m_GluePeriod（固定までの時間）
var glueField = typeof(ModuleItemHolderMagnet).GetField("m_GluePeriod",
    BindingFlags.NonPublic | BindingFlags.Instance);
```

---

## ユーティリティ

### ObjectTypes

オブジェクトタイプの列挙型。

```csharp
ObjectTypes.Waypoint    // ウェイポイント
ObjectTypes.Tank        // Tech
ObjectTypes.Block       // ブロック
```

---

### BlockTypes

ブロックタイプの列挙型。

```csharp
// 使用例（インベントリ内のブロック）
IInventory<BlockTypes> inventory;
foreach (var item in inventory)
{
    BlockTypes blockType = item.Key;
    int count = item.Value;
}
```

---

## 使用例

### 1. プレイヤーを無敵にする

```csharp
Tank playerTank = Singleton.playerTank;
if (playerTank != null)
{
    playerTank.SetInvulnerable(true, false);
}
```

### 2. お金を追加する

```csharp
ManPlayer manPlayer = Singleton.Manager<ManPlayer>.inst;
if (manPlayer != null)
{
    manPlayer.AddMoney(1000);
    int currentMoney = manPlayer.GetCurrentMoney();
    Debug.Log($"Current money: {currentMoney}");
}
```

### 3. プレイヤーをテレポートする

```csharp
Tank playerTank = Singleton.playerTank;
if (playerTank != null)
{
    Vector3 targetPosition = new Vector3(100f, 50f, 200f);
    playerTank.trans.position = targetPosition;
}
```

### 4. ウェイポイント一覧を取得する

```csharp
ManVisible manVisible = Singleton.Manager<ManVisible>.inst;
if (manVisible != null)
{
    var allTracked = manVisible.AllTrackedVisibles;
    foreach (var tracked in allTracked)
    {
        if (tracked.visible.type == ObjectTypes.Waypoint)
        {
            string name = tracked.visible.name;
            Vector3 position = tracked.visible.centrePosition;
            Debug.Log($"Waypoint: {name} at {position}");
        }
    }
}
```

### 5. Techをスポーンする

```csharp
ManSpawn manSpawn = Singleton.Manager<ManSpawn>.inst;
Tank playerTank = Singleton.playerTank;

if (manSpawn != null && playerTank != null)
{
    // TechDataを取得（既存のプリセットから）
    var presetsField = typeof(ManSpawn).GetField("m_AllTechPresets",
        BindingFlags.NonPublic | BindingFlags.Instance);
    var allPresets = presetsField.GetValue(manSpawn) as IEnumerable;

    TechData techData = null;
    foreach (var preset in allPresets)
    {
        techData = preset as TechData;
        if (techData != null) break;
    }

    // スポーン位置を計算
    Vector3 playerPos = playerTank.trans.position;
    Vector3 spawnPos = playerPos + new Vector3(30f, 0f, 0f);

    // TankSpawnParamsを作成
    var tankSpawnParamsType = typeof(ManSpawn).GetNestedType("TankSpawnParams",
        BindingFlags.Public);
    var param = System.Activator.CreateInstance(tankSpawnParamsType);

    tankSpawnParamsType.GetField("techData").SetValue(param, techData);
    tankSpawnParamsType.GetField("teamID").SetValue(param, 1); // 敵
    tankSpawnParamsType.GetField("position").SetValue(param, spawnPos);
    tankSpawnParamsType.GetField("rotation").SetValue(param, Quaternion.identity);
    tankSpawnParamsType.GetField("placement").SetValue(param, 2);

    // スポーン
    var spawnMethod = typeof(ManSpawn).GetMethod("SpawnTankRef",
        BindingFlags.Public | BindingFlags.Instance);
    Tank spawnedTank = spawnMethod.Invoke(manSpawn, new object[] { param, true }) as Tank;
}
```

### 6. ストレージの収集範囲を拡張する

```csharp
Tank playerTank = Singleton.playerTank;
if (playerTank != null)
{
    var blockman = playerTank.blockman;

    // ModuleItemPickupコンポーネントを取得
    foreach (var pickup in blockman.IterateBlockComponents<ModuleItemPickup>())
    {
        // 収集範囲を取得
        var rangeField = typeof(ModuleItemPickup).GetField("m_PickupRange",
            BindingFlags.NonPublic | BindingFlags.Instance);

        if (rangeField != null)
        {
            float originalRange = (float)rangeField.GetValue(pickup);
            float newRange = originalRange * 5.0f; // 5倍に拡張
            rangeField.SetValue(pickup, newRange);
        }
    }
}
```

---

## 注意事項

### Reflectionの使用

多くのフィールドやメソッドは`private`または`internal`であるため、Reflectionを使用してアクセスする必要があります。

```csharp
using System.Reflection;

// フィールド取得の例
var field = typeof(ClassName).GetField("fieldName",
    BindingFlags.NonPublic | BindingFlags.Instance);
var value = field.GetValue(instance);
field.SetValue(instance, newValue);
```

### パフォーマンス

Reflectionは通常のアクセスより遅いため、以下の点に注意：

1. **キャッシュする**: FieldInfo/MethodInfoを初期化時に取得してキャッシュ
2. **毎フレーム実行しない**: 必要な時だけReflectionを使用
3. **一度だけ適用**: 値を設定する場合、一度だけ設定してフラグ管理

```csharp
// 良い例：キャッシュを使用
private FieldInfo rangeField;

void Start()
{
    rangeField = typeof(ModuleItemPickup).GetField("m_PickupRange",
        BindingFlags.NonPublic | BindingFlags.Instance);
}

void ApplyOnce()
{
    if (rangeField != null)
    {
        rangeField.SetValue(pickup, newValue);
    }
}
```

### エラーハンドリング

Reflectionやゲームオブジェクトのアクセスは例外が発生する可能性があるため、常にtry-catchで囲む：

```csharp
try
{
    Tank playerTank = Singleton.playerTank;
    if (playerTank == null) return;

    // 処理...
}
catch (Exception ex)
{
    Debug.LogError($"Error: {ex}");
}
```

---

## 参考リンク

- [TerraTech Official Wiki - Modding](https://terratech.fandom.com/wiki/Official_TerraTech_Mod_Support)
- [Nuterra Modding Framework](https://github.com/Nuterra/Nuterra)
- [TTQMM - Config based patch management](https://github.com/Aceba1/TTQMM)

---

**更新日**: 2025-10-07
**対応バージョン**: TerraTech (Unity 2018.4.13f1)
**作成者**: ModMenu Project