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
        // GET: Devices
        public async Task<IList<Device>> Index()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return await connectedOfficeContext.ToListAsync();
        }
        // GET: Devices/Details/5
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
        //GET list of categories to diplay in dropdowns
        public SelectList getListCategory()
        {
            return new SelectList(_context.Category, "CategoryId", "CategoryName");
        }
        //GET list of Zones to diplay in dropdowns
        public SelectList getListZone()
        {
            return new SelectList(_context.Zone, "ZoneId", "ZoneName");
        }
        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
            return null;
        }
        //GET list of categories to diplay in dropdowns withe the selected category
        public SelectList getListCategoryByID(Guid? id,Device dev)
        {
            return new SelectList(_context.Category, "CategoryId", "CategoryName", dev.CategoryId);
        }
        //GET list of zone to diplay in dropdowns withe the selected zone
        public SelectList getListZoneByID(Guid? id, Device dev)
        {
            return new SelectList(_context.Zone, "ZoneId", "ZoneName", dev.ZoneId);
        }
        // GET: Devices/Edit/5
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
        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // GET: Devices/Delete/5
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
        // POST: Devices/Delete/5
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
