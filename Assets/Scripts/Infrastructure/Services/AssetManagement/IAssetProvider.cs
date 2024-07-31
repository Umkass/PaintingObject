using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string address);
        GameObject Instantiate(string address, Transform parent);
        GameObject Instantiate(string address, Transform parent, bool worldPositionStays);
        GameObject Instantiate(GameObject gameObject);
        GameObject Instantiate(GameObject gameObject, Transform parent);
    }
}