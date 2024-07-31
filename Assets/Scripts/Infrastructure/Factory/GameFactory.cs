using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using Logic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;

        public GameFactory(IAssetProvider assetProvider, IInputService inputService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
        }

        public GameObject CreatePaintObject()
        {
            GameObject paintGo = _assetProvider.Instantiate(AssetAddress.SpherePath);
            return paintGo;
        }

        public GameObject CreatePaintRay(GameObject paintGo)
        {
            GameObject paintRayGo = _assetProvider.Instantiate(AssetAddress.PaintRayPath);
            paintRayGo.GetComponent<PaintRay>().Construct(_inputService,_assetProvider, paintGo);
            return paintGo;
        }

        public GameObject CreateHUD()
        {
            GameObject hudGo = _assetProvider.Instantiate(AssetAddress.HudPath);
            return hudGo;
        }
    }
}