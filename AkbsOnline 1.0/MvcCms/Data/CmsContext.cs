using Microsoft.AspNet.Identity.EntityFramework;
using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcCms.Data
{
    public class CmsContext : IdentityDbContext<CmsUser>
    {
        public CmsContext(): base("akbsonli_AKBS") 
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Tesis> Tesisler { get; set; }
        public DbSet<StaticInput> StaticInputs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            //Bu satir yeni database yaratilirken commentli olmali.
            //Database.SetInitializer<CmsContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasKey(e => e.Id)
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Post>()
                .HasRequired(e => e.Author);

            modelBuilder.Entity<Record>()
                .HasKey(e => e.Id)
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Record>()
                .HasRequired(e => e.Author);

            modelBuilder.Entity<Record>()
                .HasRequired(e => e.Tesis);

            modelBuilder.Entity<Tesis>()
                .HasKey(e => e.Id)
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<StaticInput>()
                .HasKey(e => e.Id);
        }
    }
}