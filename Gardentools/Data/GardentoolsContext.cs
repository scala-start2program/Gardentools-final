using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gardentools.Models;

namespace Gardentools.Data
{
    public class GardentoolsContext : DbContext
    {
        public GardentoolsContext (DbContextOptions<GardentoolsContext> options)
            : base(options)
        {
        }

        public DbSet<Gardentools.Models.Article> Article { get; set; } = default!;
        public DbSet<Gardentools.Models.Basket> Basket { get; set; }
        public DbSet<Gardentools.Models.Brand> Brand { get; set; }
        public DbSet<Gardentools.Models.Category> Category { get; set; }
        public DbSet<Gardentools.Models.Order> Order { get; set; }
        public DbSet<Gardentools.Models.OrderDetail> OrderDetail { get; set; }
        public DbSet<Gardentools.Models.User> User { get; set; }
        public DbSet<Gardentools.Models.Score> Score { get; set; }

    }
}
