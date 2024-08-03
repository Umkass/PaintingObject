using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class BrushWidthSlider : MonoBehaviour
    {
        [SerializeField] private HUDController _hudController;
        [SerializeField] private TextMeshProUGUI _widthText;
        [SerializeField] private RectTransform _widthImage;
        [SerializeField] private Slider _widthSlider;
        [SerializeField] private float _minWidthImageSize = 5f;

        private void Awake()
        {
            InitializeSlider();
            gameObject.SetActive(false);
        }

        private void InitializeSlider()
        {
            _widthSlider.onValueChanged.AddListener(UpdateBrushWidth);
            _widthSlider.value = 1f;
        }

        private void UpdateBrushWidth(float value)
        {
            int width = Mathf.RoundToInt(value);
            UpdateWidthImageSize(width);
            UpdateWidthText(width);
            _hudController.UpdateBrushWidth(width);
        }

        private void UpdateWidthImageSize(int width)
        {
            float newSize = width + _minWidthImageSize;
            _widthImage.sizeDelta = new Vector2(newSize, newSize);
        }

        private void UpdateWidthText(int width) =>
            _widthText.text = width.ToString();
    }
}