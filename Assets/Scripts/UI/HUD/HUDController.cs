using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Button _btnSavePainting;
        [SerializeField] private Button _btnLoadPainting;
        [SerializeField] private Button _btnClearPainting;
        [SerializeField] private Button _btnBrushWidth;
        [SerializeField] private Button _btnColor;
        [SerializeField] private GameObject _brushWidth;
        [SerializeField] private GameObject _colorPicker;

        public event Action OnLoadPainting;
        public event Action OnSavePainting;
        public event Action OnClearPainting;
        public event Action<int> OnBrushWidthUpdated;
        public event Action<Color> OnBrushColorUpdated;

        private void Awake()
        {
            _btnSavePainting.onClick.AddListener(SavePainting);
            _btnLoadPainting.onClick.AddListener(LoadPainting);
            _btnClearPainting.onClick.AddListener(ClearPainting);
            _btnBrushWidth.onClick.AddListener(ShowHideBrushWidth);
            _btnColor.onClick.AddListener(ShowHideColorPicker);
        }

        public void UpdateBrushWidth(int width) =>
            OnBrushWidthUpdated?.Invoke(width);

        public void UpdateBrushColor(Color color) =>
            OnBrushColorUpdated?.Invoke(color);

        private void SavePainting() =>
            OnSavePainting?.Invoke();

        private void LoadPainting() =>
            OnLoadPainting?.Invoke();

        private void ClearPainting() =>
            OnClearPainting?.Invoke();

        private void ShowHideBrushWidth()
        {
            _brushWidth.SetActive(!_brushWidth.activeInHierarchy);
            _colorPicker.SetActive(false);
        }

        private void ShowHideColorPicker()
        {
            _colorPicker.SetActive(!_colorPicker.activeInHierarchy);
            _brushWidth.SetActive(false);
        }
    }
}