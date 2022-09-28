using DeviceManagement_WebApp.Models;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Zone GetMostRecentZone();        
        void updateZone(Zone zon);
        Task<int> saveAsync();
    }
}
