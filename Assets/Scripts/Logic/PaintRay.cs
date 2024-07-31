using System.Collections.Generic;
using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using UnityEngine;

namespace Logic
{
    public class PaintRay : MonoBehaviour
    {
        [SerializeField] private float offsetCoef;
        [SerializeField] private float _width;
        private Camera _cam;
        private int _layerMask;
        private LineRenderer _currentLine;
        private List<Vector3> _currentPositions;

        private IInputService _inputService;
        private IAssetProvider _assetProvider;
        private GameObject _paintGo;

        private void Awake()
        {
            _cam = Camera.main;
            _layerMask = (1 << LayersData.PaintObject);
        }

        public void Construct(IInputService inputService, IAssetProvider assetProvider, GameObject paintGo)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _paintGo = paintGo;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (_inputService.IsPaintClick())
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

        private void Paint(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                Vector3 pos = hit.point + hit.normal * offsetCoef;
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
            _currentPositions = new();
            _currentLine.startWidth = _width;
            _currentLine.endWidth = _width;
        }

        private void UpdatePositions()
        {
            _currentLine.positionCount = _currentPositions.Count;
            _currentLine.SetPositions(_currentPositions.ToArray());
        }
    }
}