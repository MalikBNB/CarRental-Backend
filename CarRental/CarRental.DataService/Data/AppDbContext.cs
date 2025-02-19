using CarRental.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataService.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set; }
        public virtual DbSet<CarCategory> CarCategories { get; set; }
        public virtual DbSet<VehicleReturn> VehicleReturns { get; set; }
        public virtual DbSet<RentalBooking> RentalBookings { get; set; }
        public virtual DbSet<RentalTransaction> RentalTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");

            // One-To-Many Relationships
            builder.Entity<Maintenance>()
                   .HasOne(o => o.Vehicle)
                   .WithMany(o => o.Maintenances)
                   .HasForeignKey(o => o.VehicleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<RentalBooking>()
                   .HasOne(o => o.Vehicle)
                   .WithMany(o => o.RentalBookings)
                   .HasForeignKey(o => o.VehicleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Vehicle>()
                   .HasOne(o => o.CarCategory)
                   .WithMany(o => o.Vehicles)
                   .HasForeignKey(o => o.CarCategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<RentalBooking>()
                   .HasOne(o => o.Customer)
                   .WithMany(o => o.RentalBookings)
                   .HasForeignKey(o => o.CustomerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientCascade);

            // One-To-One Relationships 
            builder.Entity<RentalTransaction>()
                   .HasOne(o => o.RentalBooking)
                   .WithOne(o => o.RentalTransaction)
                   .HasForeignKey<RentalTransaction>(o => o.RentalBookingId)
                   .IsRequired();

            builder.Entity<VehicleReturn>()
                   .HasOne(o => o.RentalTransaction)
                   .WithOne(o => o.VehicleReturn)
                   .HasForeignKey<VehicleReturn>(o => o.RentalTransactionId)
                   .IsRequired();
        }
    }
}
