using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Device GetMostRecentDevice();
        Task<IList<Device>> Index();
        Task<Device> Details(Guid? id);
        SelectList getListCategory();
        SelectList getListZone();
        Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device);
        SelectList getListCategoryByID(Guid? id, Device dev);
        SelectList getListZoneByID(Guid? id, Device dev);
        Task<Device> Edit(Guid? id);
        Task<Device> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device);
        Task<Device> Delete(Guid? id);
        Task<Device> DeleteConfirmed(Guid id);
        bool DeviceExists(Guid id);
    }
}
