using everest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppRole,string,IdentityUserClaim<string>,AppUserRole,IdentityUserLogin<string>,
                                                 IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<StorePhoto> StorePhotos { get; set; }
        public DbSet<Slide> Slides { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<AppUser>()
        //        .HasMany(u => u.UserRoles)
        //        .WithOne(ur => ur.User)
        //        .HasForeignKey(ur => ur.UserId)
        //        .IsRequired();

        //    builder.Entity<AppRole>()
        //        .HasMany(r => r.UserRoles)
        //        .WithOne(ur => ur.Role)
        //        .HasForeignKey(ur => ur.RoleId)
        //        .IsRequired();


        //    builder.Entity<Store>()
        //        .HasOne(s => s.StorePhoto)
        //        .WithOne(sp => sp.Store)
        //        .HasForeignKey<StorePhoto>(sp => sp.StoreId)
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired(false);

        //}
    }
}
