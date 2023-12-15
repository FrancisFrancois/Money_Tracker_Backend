using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _CategoryRepository.GetAll().Select(c => c.ToModel());
        }

        public Category? GetById(int id)
        {
            return _CategoryRepository.GetById(id)?.ToModel();
        }

        public Category Create(Category category)
        {
            return _CategoryRepository.Create(category.ToEntity()).ToModel();
        }

        public bool Update(int id, Category category)
        {
            bool updated = _CategoryRepository.Update(id, category.ToEntity());
            if (!updated)
            {
                throw new NotFoundException("Category not found");
            }
            return updated;
        }

        public bool Delete(int id)
        {
            bool deleted = _CategoryRepository.Delete(id);
            if (!deleted)
            {
                throw new NotFoundException("Category not found");
            }
            return deleted;
        }
    }
}
