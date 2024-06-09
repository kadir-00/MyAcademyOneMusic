using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Context
{
    public class OneMusicContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=KDR;database=OneMusicDb;integrated security=true;trustServerCertificate=true");
        }

        // DbSetlerimi olusturucam 
        // tekil olanlar bizim entities'lerimiz cogullar sql'e yansitacagimiz tablo ismi 
        public DbSet<About> Abouts { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<Message> Messages  { get; set; }
        public DbSet<Singer> Singers  { get; set; }
        public DbSet<Song> Songs  { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
