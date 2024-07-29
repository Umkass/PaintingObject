using Data;
using Infrastructure.Services;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IDefaultState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        private void RegisterServices()
        {
       
        }

        public void Enter() =>
            _sceneLoader.Load(SceneNames.Initial, onLoaded: EnterGameState);

        public void Exit()
        {
        }

        private void EnterGameState() =>
            _stateMachine.Enter<GameState>();
    }
}