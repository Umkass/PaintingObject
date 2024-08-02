using System.IO;
using UnityEngine;

namespace Data
{
    public static class Consts
    {
        public const float BrushWidthCoefficient = 0.002f;
        public static string SaveFilePath => Path.Combine(Application.persistentDataPath, "paintingData.dat");
    }
}