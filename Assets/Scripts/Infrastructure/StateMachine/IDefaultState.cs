namespace Infrastructure.StateMachine
{
    public interface IDefaultState : IState
    {
        void Enter();
    }
}