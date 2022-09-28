using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Category GetMostRecentCategory()
        {
            return _context.Category.OrderByDescending(category => category.DateCreated).FirstOrDefault();
        }
        public async Task<int> updateCategories(Category cat)
        {
            _context.Category.Update(cat);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> saveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        

        // GET: Categories/Details/5
        public async Task<Category> Details(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return null;
            }

            return category;
        }

        // GET: Categories/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}        
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
            return null;
        }

        // GET: Categories/Edit/5
        public async Task<Category> Edit(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            return category;
        }      
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return null;
            }
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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
        public async Task<Category> Delete(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return null;
            }

            return (category);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
             await _context.SaveChangesAsync();
            return null;
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
