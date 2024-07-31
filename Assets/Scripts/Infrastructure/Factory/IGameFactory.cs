using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreatePaintObject();
        public GameObject CreatePaintRay(GameObject paintGo);
        public GameObject CreateHUD();
    }
}