using Microsoft.EntityFrameworkCore;
using qualitybook2.Data.interfaces;
using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QualityBookDbContext _qualityBookDbContext;
        public CategoryRepository(QualityBookDbContext qualityBookDbContext)
        {
            _qualityBookDbContext = qualityBookDbContext;
        }

        IEnumerable<Category> ICategoryRepository.Categories => _qualityBookDbContext.Categories;
    }
}
