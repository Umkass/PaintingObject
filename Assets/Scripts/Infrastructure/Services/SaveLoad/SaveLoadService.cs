using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data;
using Data.PaintingData;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IProgressService _progressService;

        public SaveLoadService(IProgressService progressService) =>
            _progressService = progressService;

        public void SavePaintingData()
        {
            if (_progressService.PaintingData == null)
            {
                Debug.LogWarning("No painting data to save.");
                return;
            }

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using FileStream fileStream = new FileStream(Consts.SaveFilePath, FileMode.Create);
                formatter.Serialize(fileStream, _progressService.PaintingData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save painting data: {ex.Message}");
            }
        }

        public PaintingData LoadPaintingData()
        {
            if (!File.Exists(Consts.SaveFilePath))
            {
                Debug.LogWarning("Save file not found.");
                return new PaintingData();
            }

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using FileStream fileStream = new FileStream(Consts.SaveFilePath, FileMode.Open);
                return (PaintingData)formatter.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load painting data: {ex.Message}");
                return new PaintingData();
            }
        }
    }
}