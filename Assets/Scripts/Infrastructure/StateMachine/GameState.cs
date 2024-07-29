using Data;
using UI;

namespace Infrastructure.StateMachine
{
    public class GameState : IDefaultState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public GameState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(SceneNames.Game, OnLoaded);
        }

        private void OnLoaded() => 
            _loadingCurtain.Hide();

        public void Exit()
        {
        }
    }
}