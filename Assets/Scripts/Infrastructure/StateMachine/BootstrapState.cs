using Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.InputService;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

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
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IProgressService>(new ProgressService());
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IProgressService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IInputService>(),
                _services.Single<IAssetProvider>(), _services.Single<IProgressService>(),
                _services.Single<ISaveLoadService>()));
        }

        private IInputService InputService()
        {
            return Application.isEditor ? new PCInputService() : null;
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