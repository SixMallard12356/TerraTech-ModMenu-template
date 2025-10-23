using System;
using UnityEngine;

namespace TerraTechModMenu
{
    /// <summary>
    /// ModMenuのエントリーポイント
    /// DLLインジェクション後、このLoadメソッドが呼ばれる
    /// </summary>
    public class Loader
    {
        private static GameObject loaderObject;

        /// <summary>
        /// Modのロード処理
        /// インジェクター側から呼び出される
        /// </summary>
        public static void Load()
        {
            try
            {
                // Unityのメインスレッドで実行されるようにGameObjectを作成
                loaderObject = new GameObject("TerraTechModMenu");
                loaderObject.AddComponent<ModMenuManager>();

                // シーン遷移時に破棄されないようにする
                UnityEngine.Object.DontDestroyOnLoad(loaderObject);

                Debug.Log("[TerraTech ModMenu] Successfully loaded!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TerraTech ModMenu] Load failed: {ex}");
            }
        }

        /// <summary>
        /// Modのアンロード処理
        /// </summary>
        public static void Unload()
        {
            try
            {
                if (loaderObject != null)
                {
                    UnityEngine.Object.Destroy(loaderObject);
                    loaderObject = null;
                }

                Debug.Log("[TerraTech ModMenu] Unloaded!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TerraTech ModMenu] Unload failed: {ex}");
            }
        }
    }
}
