using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string address);
        GameObject Instantiate(string address, Transform parent, bool worldPositionStays);
    }
}