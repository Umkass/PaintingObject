using Data.PaintingData;

namespace Infrastructure.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public PaintingData PaintingData { get; set; }
    }
}