using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.ColorPicker
{
    public class ColorPicker : MonoBehaviour
    {
        [SerializeField] private HUDController _hudController;
        [SerializeField] private Slider _hueSlider;
        [SerializeField] private RawImage _hueImage;
        [SerializeField] private RawImage _saturationValueImage;
        [SerializeField] private RawImage _outputImage;

        private Texture2D _hueTexture;
        private Texture2D _satValTexture;
        private Texture2D _outputTexture;

        private float _currentColorHue, _currentColorSaturation, _currentColorValue;

        private void Awake()
        {
            CreateHueImage();
            CreateSatValImage();
            CreateOutputImage();
            gameObject.SetActive(false);
        }

        private void CreateHueImage()
        {
            _hueTexture = new Texture2D(1, 16) { wrapMode = TextureWrapMode.Clamp };

            for (int i = 0; i < _hueTexture.height; i++)
                _hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / _hueTexture.height, 1f, 1f));

            _hueTexture.Apply();
            _currentColorHue = 0f;
            _hueImage.texture = _hueTexture;
        }

        private void CreateSatValImage()
        {
            _satValTexture = new Texture2D(16, 16) { wrapMode = TextureWrapMode.Clamp };
            UpdateSatValTexture();
            _currentColorSaturation = 0f;
            _currentColorValue = 0f;
            _saturationValueImage.texture = _satValTexture;
        }

        private void CreateOutputImage()
        {
            _outputTexture = new Texture2D(1, 16) { wrapMode = TextureWrapMode.Clamp };
            UpdateOutputTexture();
            _outputImage.texture = _outputTexture;
        }

        public void SetSaturationAndValue(float sat, float val)
        {
            _currentColorSaturation = sat;
            _currentColorValue = val;
            UpdateOutputTexture();
        }

        public void OnUpdateHueSlider()
        {
            _currentColorHue = _hueSlider.value;
            UpdateSatValTexture();
            UpdateOutputTexture();
        }

        private void UpdateOutputTexture()
        {
            Color currentColor = Color.HSVToRGB(_currentColorHue, _currentColorSaturation, _currentColorValue);

            for (int i = 0; i < _outputTexture.height; i++)
                _outputTexture.SetPixel(0, i, currentColor);

            _outputTexture.Apply();
            _hudController.UpdateBrushColor(currentColor);
        }

        private void UpdateSatValTexture()
        {
            for (int y = 0; y < _satValTexture.height; y++)
            {
                for (int x = 0; x < _satValTexture.width; x++)
                {
                    _satValTexture.SetPixel(x, y, Color.HSVToRGB(_currentColorHue,
                        (float)x / _satValTexture.width,
                        (float)y / _satValTexture.height));
                }
            }

            _satValTexture.Apply();
        }
    }
}