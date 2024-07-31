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

        public GameObject Instantiate(string address, Transform parent)
        {
            GameObject loadedGo = Resources.Load(address) as GameObject;
            return Object.Instantiate(loadedGo, parent);
        }

        public GameObject Instantiate(string address, Transform parent, bool worldPositionStays)
        {
            GameObject loadedGo = Resources.Load(address) as GameObject;
            return Object.Instantiate(loadedGo, parent, worldPositionStays);
        }

        public GameObject Instantiate(GameObject gameObject) =>
            Object.Instantiate(gameObject);

        public GameObject Instantiate(GameObject gameObject, Transform parent) =>
            Object.Instantiate(gameObject, parent);
    }
}