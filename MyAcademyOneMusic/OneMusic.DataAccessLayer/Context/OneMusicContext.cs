using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Context
{
    public class OneMusicContext : IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-R0GSR22\\SQLEXPRESS;database=OneMusicDbSo;user=sa;Password=1;trustServerCertificate=true");
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongsListenDetails> SongsListenDetails { get; set; }
        public DbSet<Category>  categories { get; set; }
        public DbSet<Event>  Events { get; set; }


    }
}
