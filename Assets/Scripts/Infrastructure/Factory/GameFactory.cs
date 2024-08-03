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

        public PaintingBrushRay CreatePaintBrushRay(GameObject paintObject)
        {
            GameObject paintBrushRayGo = _assetProvider.Instantiate(AssetAddress.PaintBrushRayPath);
            PaintingBrushRay paintBrushRay = paintBrushRayGo.GetComponent<PaintingBrushRay>();
            paintBrushRay.Construct(_inputService, _assetProvider, _progressService, _saveLoadService, paintObject);
            return paintBrushRay;
        }

        public void CreateHUD(PaintingBrushRay paintRay)
        {
            GameObject hudGo = _assetProvider.Instantiate(AssetAddress.HudPath);
            HUDController hud = hudGo.GetComponent<HUDController>();
            hud.OnSavePainting += paintRay.SavePainting;
            hud.OnLoadPainting += paintRay.LoadPainting;
            hud.OnClearPainting += paintRay.ClearPainting;
            hud.OnBrushWidthUpdated += paintRay.UpdateBrushWidth;
            hud.OnBrushColorUpdated += paintRay.UpdateBrushColor;
        }
    }
}