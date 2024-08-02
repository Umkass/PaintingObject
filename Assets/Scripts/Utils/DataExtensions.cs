using System;
using Data;
using UnityEngine;

namespace Utils
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => 
            new Vector3Data(vector.x, vector.y, vector.z);
    
        public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
        
        public static float[] ColorToFloatArray(Color color)
        {
            return new float[] { color.r, color.g, color.b, color.a };
        }

        public static Color FloatArrayToColor(float[] array)
        {
            if (array.Length != 4)
                throw new ArgumentException("Array must have 4 elements to convert to Color.");
            return new Color(array[0], array[1], array[2], array[3]);
        }
    }
}