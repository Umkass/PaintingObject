using Data;
using Infrastructure.Factory;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class GameState : IDefaultState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public GameState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(SceneNames.Game, OnLoaded);
        }

        private void OnLoaded()
        {
            _loadingCurtain.Hide();
            GameObject paintGo = _gameFactory.CreatePaintObject();
            _gameFactory.CreatePaintRay(paintGo);
            _gameFactory.CreateHUD();
        }

        public void Exit()
        {
        }
    }
}