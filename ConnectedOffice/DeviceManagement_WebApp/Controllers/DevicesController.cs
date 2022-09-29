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

namespace DeviceManagement_WebApp.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _DeviceRepository;

        public DevicesController(IDeviceRepository DeviceRepository)
        {
            _DeviceRepository = DeviceRepository;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {           
            return View(await _DeviceRepository.Index());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(await _DeviceRepository.Details(id));
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = _DeviceRepository.getListCategory();
            ViewData["ZoneId"] = _DeviceRepository.getListZone();
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            await _DeviceRepository.Create(device);
            return RedirectToAction(nameof(Index));


        }
        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {           
            Device device = await _DeviceRepository.Edit(id);
            ViewData["CategoryId"] = _DeviceRepository.getListCategoryByID(device.CategoryId,device);
            ViewData["ZoneId"] = _DeviceRepository.getListZoneByID(device.ZoneId,device);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Device device)
        {
            await _DeviceRepository.Edit(id,device);
            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
           

            var device = await _DeviceRepository.Delete(id);
           

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _DeviceRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
