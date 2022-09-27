using DeviceManagement_WebApp.Models;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Category GetMostRecentCategory();
        Task<int> updateCategories(Category cat);
        Task<int> save();
    }
}
