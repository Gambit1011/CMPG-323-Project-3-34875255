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
    public class CategoriesController : Controller
    {
        //private readonly ConnectedOfficeContext _context;
        private readonly ICategoriesRepository _CategoryRepository;

        public CategoriesController(ICategoriesRepository CategoriesRepository)
        {
            _CategoryRepository = CategoriesRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_CategoryRepository.GetAll());//Get all categories and send to the view to display
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {            
            return View(await _CategoryRepository.Details(id)); //show more category information per category
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create(Category category)
        {           
            await _CategoryRepository.Create(category);
            return RedirectToAction(nameof(Index)); //redirect the page back to the category list after new category added
        }



        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {            
            return View(await _CategoryRepository.Edit(id));
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Category category)
        {
            await _CategoryRepository.Edit(id,category);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View(await _CategoryRepository.Delete(id));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _CategoryRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
