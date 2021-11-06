using System;
using System.Collections.Generic;
using System.Text;
using everest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace everest.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppRole,string,IdentityUserClaim<string>,AppUserRole
            ,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<StoreClassification> StoreClassifications { get; set; }
        public DbSet<ClinicClassification> ClinicClassifications { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<CompanyPhoto> CompanyPhotos { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<Comment>()
                .HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Comment>()
                .HasOne(p => p.Product)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

            builder.Entity<AppUser>()
                .HasOne(u => u.Store)
                .WithOne(s => s.User)
                .HasForeignKey<AppUser>(u => u.StoreId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);


            builder.Entity<AppUser>()
                .HasOne(u => u.Clinic)
                .WithOne(c => c.User)
                .HasForeignKey<AppUser>(u => u.ClinicId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Store>()
                .HasOne(s => s.CompanyPhoto)
                .WithOne(cp => cp.Store)
                .HasForeignKey<CompanyPhoto>(cp => cp.StoreId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Clinic>()
                .HasOne(cl => cl.CompanyPhoto)
                .WithOne(cp => cp.Clinic)
                .HasForeignKey<Clinic>(cl => cl.CompanyPhotoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Store>()
                .HasMany(s => s.Classifications)
                .WithOne(c => c.Store)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Classification>()
                .HasMany(c => c.Stores)
                .WithOne(s => s.Classification)
                .HasForeignKey(c => c.ClassificationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Clinic>()
                .HasMany(c => c.Classifications)
                .WithOne(c => c.Clinic)
                .HasForeignKey(c => c.ClinicId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<Classification>()
                .HasMany(c => c.Clinics)
                .WithOne(c => c.Classification)
                .HasForeignKey(c => c.ClassificationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);


        }
    }
}
