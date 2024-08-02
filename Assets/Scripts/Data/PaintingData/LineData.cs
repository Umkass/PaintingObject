using System;
using UnityEngine;
using Utils;

namespace Data.PaintingData
{
    [Serializable]
    public class LineData
    {
        public Vector3Data[] positions;
        public float[] startColor;
        public float[] endColor;
        public float startWidth;
        public float endWidth;
        public int sortingOrder;

        public LineData()
        {
            positions = Array.Empty<Vector3Data>();
            startColor = DataExtensions.ColorToFloatArray(Color.black);
            endColor = DataExtensions.ColorToFloatArray(Color.black);
            startWidth = 1f;
            endWidth = 1f;
            sortingOrder = 0;
        }
    }
}