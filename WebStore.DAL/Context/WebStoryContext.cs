using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainCorr.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebStory.DAL.Context 
{
    public class WebStoryContext : DbContext
    {
        public WebStoryContext(DbContextOptions<WebStoryContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
    }
}
