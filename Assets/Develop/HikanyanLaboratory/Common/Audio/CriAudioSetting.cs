using System;
using CriWare;
using CriWare.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HikanyanLaboratory.Common
{
    [Serializable, CreateAssetMenu(fileName = "CriAudioSetting", menuName = "HikanyanLaboratory/Audio/CriAudioSetting")]
    public class CriAudioSetting : ScriptableObject
    {
        [SerializeField] private string _acbAddress;

        private CriAtomExAcb _acb;

        public async UniTask InitializeAsync()
        {
            // Addressablesの初期化およびカタログの更新処理
            await InitializeAddressablesAsync();

            // ACBデータのロード
            _acb = await LoadAcbFromAddressable(_acbAddress);
            if (_acb == null)
            {
                Debug.LogError($"Failed to load ACB from address: {_acbAddress}");
            }
        }

        /// <summary>
        /// Addressables経由でACBを非同期ロードする
        /// </summary>
        private static async UniTask<CriAtomExAcb> LoadAcbFromAddressable(string address)
        {
            try
            {
                // ACBアセットのAddressableからのロード
                var acbAsset = await Addressables.LoadAssetAsync<CriAtomAcbAsset>(address);
                if (!acbAsset.Loaded)
                {
                    // ACBアセットのキューシートのロード
                    acbAsset.LoadAsync();
                    await new WaitUntil(() => acbAsset.Loaded || acbAsset.Status == CriAtomExAcbLoader.Status.Error);
                }

                if (acbAsset.Loaded)
                {
                    return acbAsset.Handle;
                }
                else
                {
                    Debug.LogError($"Failed to load ACB asset: {address}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception occurred while loading ACB: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Addressablesの初期化とカタログの更新処理を行う
        /// </summary>
        private static async UniTask InitializeAddressablesAsync()
        {
            // リモートカタログの有無を確認し、Addressablesを初期化
            var catalogs = await Addressables.CheckForCatalogUpdates().Task;
            if (catalogs.Count > 0)
            {
                await Addressables.UpdateCatalogs(catalogs).Task;
            }
            else
            {
                await Addressables.InitializeAsync().Task;
            }

            // Addressables内のCriAddressables用の情報を修正
            CriAddressables.ModifyLocators();
        }

        /// <summary>
        /// ACBの取得を外部から参照できるようにする
        /// </summary>
        public CriAtomExAcb GetAcb()
        {
            return _acb;
        }
    }
}