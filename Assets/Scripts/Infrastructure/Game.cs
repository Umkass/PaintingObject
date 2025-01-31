using Infrastructure.Services;
using Infrastructure.StateMachine;
using UI;

namespace Infrastructure
{
    public class Game
    {
        public readonly IGameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain) => 
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
    }
}