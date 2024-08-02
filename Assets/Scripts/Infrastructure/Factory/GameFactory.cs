using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using Logic;
using UI.HUD;
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

        public GameObject CreatePaintRay(GameObject paintObject)
        {
            GameObject paintRayGo = _assetProvider.Instantiate(AssetAddress.PaintRayPath);
            PaintingBrushRay paintRay = paintRayGo.GetComponent<PaintingBrushRay>();
            paintRay.Construct(_inputService, _assetProvider, paintObject);
            return paintRayGo;
        }

        public GameObject CreateHUD(PaintingBrushRay paintRay)
        {
            GameObject hudGo = _assetProvider.Instantiate(AssetAddress.HudPath);
            HUDController hud = hudGo.GetComponent<HUDController>();
            hud.OnCleanUpPainting += paintRay.CleanUpPainting;
            hud.OnBrushWidthUpdated += paintRay.UpdateBrushWidth;
            hud.OnBrushColorUpdated += paintRay.UpdateBrushColor;
            return hudGo;
        }
    }
}