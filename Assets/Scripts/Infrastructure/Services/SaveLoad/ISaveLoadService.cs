using Data.PaintingData;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SavePaintingData();

        PaintingData LoadPaintingData();
    }
}