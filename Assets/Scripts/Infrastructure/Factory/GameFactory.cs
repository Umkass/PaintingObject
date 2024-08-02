using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveLoad;
using Logic;
using UI.HUD;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public GameFactory(IInputService inputService, IAssetProvider assetProvider, IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
            paintRay.Construct(_inputService, _assetProvider, _progressService, _saveLoadService, paintObject);
            return paintRayGo;
        }

        public GameObject CreateHUD(PaintingBrushRay paintRay)
        {
            GameObject hudGo = _assetProvider.Instantiate(AssetAddress.HudPath);
            HUDController hud = hudGo.GetComponent<HUDController>();
            hud.OnSavePainting += paintRay.SavePainting;
            hud.OnLoadPainting += paintRay.LoadPainting;
            hud.OnCleanUpPainting += paintRay.CleanUpPainting;
            hud.OnBrushWidthUpdated += paintRay.UpdateBrushWidth;
            hud.OnBrushColorUpdated += paintRay.UpdateBrushColor;
            return hudGo;
        }
    }
}