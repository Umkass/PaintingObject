using System.Collections.Generic;
using System.Linq;
using Data;
using Data.PaintingData;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

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
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private GameObject _paintGo;

        public void Construct(IInputService inputService, IAssetProvider assetProvider,
            IProgressService progressService, ISaveLoadService saveLoadService, GameObject paintGo)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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

        public void SavePainting()
        {
            PaintingData paintingData = new PaintingData();

            foreach (var lineRenderer in _currentLineRenderers)
            {
                LineData lineData = new LineData
                {
                    positions = GetLinePositions(lineRenderer).Select(p => p.AsVectorData()).ToArray(),
                    startColor = DataExtensions.ColorToFloatArray(lineRenderer.startColor),
                    endColor = DataExtensions.ColorToFloatArray(lineRenderer.endColor),
                    startWidth = lineRenderer.startWidth,
                    endWidth = lineRenderer.endWidth,
                    sortingOrder = lineRenderer.sortingOrder
                };
                paintingData.lines.Add(lineData);
            }

            _progressService.PaintingData = paintingData;
            _saveLoadService.SavePaintingData();
        }

        public void LoadPainting()
        {
            PaintingData paintingData = _saveLoadService.LoadPaintingData();
            _progressService.PaintingData = paintingData;

            CleanUpPainting();

            foreach (var lineData in paintingData.lines)
            {
                GameObject paintLineGo = _assetProvider.Instantiate(AssetAddress.PaintLinePath, _paintGo.transform, true);
                LineRenderer lineRenderer = paintLineGo.GetComponent<LineRenderer>();
                lineRenderer.positionCount = lineData.positions.Length;
                lineRenderer.SetPositions(lineData.positions.Select(p => p.AsUnityVector()).ToArray());
                lineRenderer.startColor = DataExtensions.FloatArrayToColor(lineData.startColor);
                lineRenderer.endColor = DataExtensions.FloatArrayToColor(lineData.endColor);
                lineRenderer.startWidth = lineData.startWidth;
                lineRenderer.endWidth = lineData.endWidth;
                lineRenderer.sortingOrder = lineData.sortingOrder;

                _currentLineRenderers.Add(lineRenderer);
            }
        }

        public void CleanUpPainting()
        {
            foreach (var lineRenderer in _currentLineRenderers)
                Destroy(lineRenderer.gameObject);

            _currentLineRenderers.Clear();
        }

        public void UpdateBrushWidth(int width) =>
            _currentWidth = width * Consts.BrushWidthCoefficient;

        public void UpdateBrushColor(Color color) =>
            _currentColor = color;

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

        private Vector3[] GetLinePositions(LineRenderer lineRenderer)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            return positions;
        }
    }
}