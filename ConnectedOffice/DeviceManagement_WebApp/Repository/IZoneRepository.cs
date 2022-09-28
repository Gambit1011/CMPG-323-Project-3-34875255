using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Zone GetMostRecentZone();        
        void updateZone(Zone zon);
        Task<int> saveAsync();
        Task<Zone> Details(Guid? id);
        Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone);
        Task<Zone> Edit(Guid? id);
        Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone);
        Task<Zone> Delete(Guid? id);
        Task<IActionResult> DeleteConfirmed(Guid id);
        bool ZoneExists(Guid id);
    }
}
