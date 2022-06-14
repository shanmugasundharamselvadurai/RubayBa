using System.Threading.Tasks;

namespace Walkland.Core.Services.Interfaces
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}