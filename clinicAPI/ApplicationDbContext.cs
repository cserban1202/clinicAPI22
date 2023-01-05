using clinicAPI.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;


namespace clinicAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute]DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Category1> Categories { get; set; }
        public DbSet<Category2> Categories2 { get; set; }
        public DbSet<Category3> Categories3 { get; set; }
    }
}
