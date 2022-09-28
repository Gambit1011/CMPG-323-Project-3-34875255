using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.EntityFrameworkCore.Internal;

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly IZoneRepository _ZoneRepository;

        public ZonesController(IZoneRepository ZoneRepository)
        {
            _ZoneRepository = ZoneRepository;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_ZoneRepository.GetAll());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(await _ZoneRepository.Details(id));
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zone zone)
        {
            await _ZoneRepository.Create(zone);
            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            return View(await _ZoneRepository.Edit(id));
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Zone zone)
        {
            await _ZoneRepository.Edit(id, zone);
            return RedirectToAction(nameof(Index));

        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View(await _ZoneRepository.Delete(id));
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _ZoneRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
