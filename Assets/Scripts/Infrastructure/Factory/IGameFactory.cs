using Infrastructure.Services;
using Logic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePaintObject();
        PaintingBrushRay CreatePaintBrushRay(GameObject paintGo);
        void CreateHUD(PaintingBrushRay paintRay);
    }
}