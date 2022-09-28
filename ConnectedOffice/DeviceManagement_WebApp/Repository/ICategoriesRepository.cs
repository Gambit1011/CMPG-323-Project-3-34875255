using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Category GetMostRecentCategory();
        Task<int> updateCategories(Category cat);
        Task<int> saveAsync();        
        Task<Category> Details(Guid? id);
        Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category);
        Task<Category> Edit(Guid? id);
        Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category);
        Task<Category> Delete(Guid? id);
        Task<IActionResult> DeleteConfirmed(Guid id);
        bool CategoryExists(Guid id);
        
    }
}
