using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AttrOleo.Models;
using System.Linq;

namespace AttrOleo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileDescription> FileDescriptions { get; set; }
        public DbSet<Pressione> Pressioni { get; set; }
        public DbSet<Valvola> Valvole { get; set; }
        public DbSet<Disco> Dischi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileDescription>().Property(t => t.File).HasColumnType("VARBINARY(MAX) FILESTREAM");
            
            //modelBuilder.Entity<Pressione>().HasMany(p => p.Files).WithOne();
            
            modelBuilder.Entity<FileDescription>().Property(m => m.Id).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();

            modelBuilder.Entity<PressioneValvola>().ToTable("PressioneValvola");

            modelBuilder.Entity<PressioneValvola>()
                .HasKey(c => new { c.PressioneID, c.ValvolaID });
            // ...

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
