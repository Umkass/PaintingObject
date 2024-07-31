namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        public bool IsPaintClick();

        public bool IsPaintHold();

        public bool IsPaintUp();
    }
}