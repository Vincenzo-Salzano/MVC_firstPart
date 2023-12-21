﻿using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DataAccess.Repository.IRepository;
using Project.DataAccess.Data;

namespace Project.DataAccess.Repository
{
     class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        

        public void Update(Category obj)
        {
            _context.Update(obj);
        }
    }
    
}
