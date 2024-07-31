using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using UI;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(GameState)] = new GameState(sceneLoader, loadingCurtain, services.Single<IGameFactory>()),
            };
        }

        public void Enter<TState>() where TState : class, IDefaultState
        {
            IDefaultState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}