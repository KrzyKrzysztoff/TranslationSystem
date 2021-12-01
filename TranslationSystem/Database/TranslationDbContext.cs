using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationSystem.Models;

namespace TranslationSystemMVC.Database
{
    public class TranslationDbContext : DbContext
    {
        public TranslationDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Content> Content { get; set; }
    }
}
