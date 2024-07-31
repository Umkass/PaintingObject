namespace Infrastructure.Services.InputService
{
    public abstract class InputService : IInputService
    {
        public abstract bool IsPaintClick();

        public abstract bool IsPaintHold();

        public abstract bool IsPaintUp();
    }
}