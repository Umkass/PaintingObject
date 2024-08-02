using System.Collections.Generic;
using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class PaintingBrushRay : MonoBehaviour
    {
        [SerializeField] private float _offsetCoef;
        private Camera _cam;
        private int _layerMask;

        private readonly List<LineRenderer> _currentLineRenderers = new();
        private LineRenderer _currentLine;
        private List<Vector3> _currentPositions;
        private float _currentWidth;
        private Color _currentColor;

        private IInputService _inputService;
        private IAssetProvider _assetProvider;
        private GameObject _paintGo;

        public void Construct(IInputService inputService, IAssetProvider assetProvider, GameObject paintGo)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _paintGo = paintGo;
        }

        private void Awake()
        {
            _cam = Camera.main;
            _layerMask = (1 << LayersData.PaintObject);
            InitializeBrush();
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (_inputService.IsPaintClick() && Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                if (_currentLine == null)
                    CreateNewPaintLine();
            }
            else if (_inputService.IsPaintHold() && _currentLine != null)
            {
                Paint(ray);
            }
            else if (_inputService.IsPaintUp())
            {
                _currentLine = null;
            }
        }

        public void UpdateBrushWidth(int width) =>
            _currentWidth = width * Consts.BrushWidthCoefficient;

        public void UpdateBrushColor(Color color) =>
            _currentColor = color;

        public void CleanUpPainting()
        {
            foreach (var lineRenderer in _currentLineRenderers)
                Destroy(lineRenderer.gameObject);

            _currentLineRenderers.Clear();
        }

        private void InitializeBrush()
        {
            UpdateBrushWidth(1);
            UpdateBrushColor(Color.black);
        }

        private void Paint(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                Vector3 pos = hit.point + hit.normal * _offsetCoef;
                if (_currentPositions.Count == 0 || _currentPositions[_currentPositions.Count - 1] != pos)
                {
                    _currentPositions.Add(pos);
                    UpdatePositions();
                }
            }
            else
            {
                _currentLine = null;
            }
        }

        private void CreateNewPaintLine()
        {
            GameObject paintLineGo = _assetProvider.Instantiate(AssetAddress.PaintLinePath, _paintGo.transform, true);
            _currentLine = paintLineGo.GetComponent<LineRenderer>();
            _currentLineRenderers.Add(_currentLine);
            _currentLine.sortingOrder = _currentLineRenderers.Count;
            _currentPositions = new();
            _currentLine.startWidth = _currentWidth;
            _currentLine.endWidth = _currentWidth;
            _currentLine.startColor = _currentColor;
            _currentLine.endColor = _currentColor;
        }

        private void UpdatePositions()
        {
            _currentLine.positionCount = _currentPositions.Count;
            _currentLine.SetPositions(_currentPositions.ToArray());
        }
    }
}