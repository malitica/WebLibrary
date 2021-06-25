using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBiblioteka.Models;

namespace WebBiblioteka.Models
{
    public class KomentarContext : DbContext
    {
        public KomentarContext(DbContextOptions<KomentarContext> opcije) : base(opcije)
        {
        }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<WebBiblioteka.Models.Book> Book { get; set; }
    }
}
