using Infrastructure.Services;

namespace Infrastructure.StateMachine
{
    public interface IGameStateMachine :IService
    {
        void Enter<TState>() where TState : class, IDefaultState;
    }
}