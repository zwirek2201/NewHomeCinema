using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface ICategoriesManager
    {
        Task<List<Category>> GetCategories();

        Task<Category> GetCategory(int id);

        Task<Category> AddCategory(Category category);

        Task DeleteCategory(int id);
    }
}
