using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Zone GetMostRecentZone()
        {
            return _context.Zone.OrderByDescending(zone => zone.DateCreated).FirstOrDefault();
        }
        public void updateZone(Zone zon)
        {
            _context.Zone.Update(zon);
        }
        public async Task<int> saveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // GET: Categories/Details/5
        public async Task<Zone> Details(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
            if (zone == null)
            {
                return null;
            }

            return zone;
        }
        // GET: Categories/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}        
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _context.Add(zone);
            await _context.SaveChangesAsync();
            return null;
        }

        // GET: Categories/Edit/5
        public async Task<Zone> Edit(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var zone = await _context.Zone.FindAsync(id);
            if (zone == null)
            {
                return null;
            }
            return zone;
        }
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return null;
            }
            try
            {
                _context.Update(zone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // GET: Categories/Delete/5
        public async Task<Zone> Delete(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
            if (zone == null)
            {
                return null;
            }

            return (zone);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(zone);
            await _context.SaveChangesAsync();
            return null;
        }

        public bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }
    }
}
