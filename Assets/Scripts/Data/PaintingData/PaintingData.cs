using System;
using System.Collections.Generic;

namespace Data.PaintingData
{
    [Serializable]
    public class PaintingData
    {
        public List<LineData> lines = new();
    }
}