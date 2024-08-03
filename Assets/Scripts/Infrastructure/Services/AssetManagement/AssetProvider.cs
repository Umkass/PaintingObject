using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string address)
        {
            GameObject loadedGo = Resources.Load(address) as GameObject;
            return Object.Instantiate(loadedGo);
        }

        public GameObject Instantiate(string address, Transform parent, bool worldPositionStays)
        {
            GameObject loadedGo = Resources.Load(address) as GameObject;
            return Object.Instantiate(loadedGo, parent, worldPositionStays);
        }
    }
}