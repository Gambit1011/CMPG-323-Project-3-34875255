using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Device GetMostRecentDevice()
        {
            return _context.Device.OrderByDescending(device => device.DateCreated).FirstOrDefault();
        }
        public async Task<IList<Device>> Index()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return await connectedOfficeContext.ToListAsync();
        }
        public async Task<Device> Details(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return null;
            }

            return device;
        }
        public SelectList getListCategory()
        {
            return new SelectList(_context.Category, "CategoryId", "CategoryName");
        }
        public SelectList getListZone()
        {
            return new SelectList(_context.Zone, "ZoneId", "ZoneName");
        }
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
            return null;
        }
        public SelectList getListCategoryByID(Guid? id,Device dev)
        {
            return new SelectList(_context.Category, "CategoryId", "CategoryName", dev.CategoryId);
        }
        public SelectList getListZoneByID(Guid? id, Device dev)
        {
            return new SelectList(_context.Zone, "ZoneId", "ZoneName", dev.ZoneId);
        }
        public async Task<Device> Edit(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var device = await _context.Device.FindAsync(id);
            if (device == null)
            {
                return null;
            }
           
            return device;
        }
        public async Task<Device> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return null;
            }
            try
            {
                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return null;

        }

        public async Task<Device> Delete(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return null;
            }

            return device;
        }

        public async Task<Device> DeleteConfirmed(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return null;
        }

        public bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

    }
}
