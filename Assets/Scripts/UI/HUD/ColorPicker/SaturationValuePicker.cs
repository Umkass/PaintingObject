using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.HUD.ColorPicker
{
    public class SaturationValuePicker : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        [SerializeField] private RectTransform _satValTransform;
        [SerializeField] private RectTransform _satValPickerTransform;
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private Image _pickerImage;
        [SerializeField] private float _sizeFactor = 0.5f;

        public void OnDrag(PointerEventData eventData) =>
            UpdateColor(eventData);

        public void OnPointerClick(PointerEventData eventData) =>
            UpdateColor(eventData);

        private void UpdateColor(PointerEventData eventData)
        {
            Vector3 pos = _satValTransform.InverseTransformPoint(eventData.position);
            float deltaX = _satValTransform.sizeDelta.x * _sizeFactor;
            float deltaY = _satValTransform.sizeDelta.y * _sizeFactor;
            pos.x = Mathf.Clamp(pos.x, -deltaX, deltaX);
            pos.y = Mathf.Clamp(pos.y, -deltaY, deltaY);

            float x = pos.x + deltaX;
            float y = pos.y + deltaY;
            float xNorm = x / _satValTransform.sizeDelta.x;
            float yNorm = y / _satValTransform.sizeDelta.y;

            _satValPickerTransform.localPosition = pos;
            _pickerImage.color = Color.HSVToRGB(0f, 0f, 1f - yNorm);
            _colorPicker.SetSaturationAndValue(xNorm, yNorm);
        }
    }
}