using Data.PaintingData;

namespace Infrastructure.Services.Progress
{
    public interface IProgressService : IService
    {
        PaintingData PaintingData { get; set; }
    }
}