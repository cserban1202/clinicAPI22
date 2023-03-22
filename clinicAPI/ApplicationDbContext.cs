using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category1> Categories { get; set; }
        public DbSet<Category2> Categories2 { get; set; }
        public DbSet<Category3> Categories3 { get; set; }

        public DbSet<Category4> Categories4 { get; set; }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Consultation>Consultations { get; set; }

        public DbSet<Examination> Examinations { get; set; }
    }
}
