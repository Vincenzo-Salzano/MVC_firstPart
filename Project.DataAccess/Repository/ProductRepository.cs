﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repository
{
     class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            _context.Update(obj);
        }
    }
}
