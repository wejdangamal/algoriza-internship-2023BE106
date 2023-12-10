using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Doctor>().ToTable("Doctors").HasKey(x => x.Id);

            builder.Entity<Appointments>()
            .HasOne(b => b.Doctor)
            .WithMany(a => a.Appointments)
            .HasPrincipalKey(b=>b.ApplicationUserId)
            .HasForeignKey(b => b.doctorId);

            builder.Entity<DiscountCode_Coupon>().ToTable("DiscountCodeCoupons")
                .HasIndex(name=>name.Code)
                .IsUnique();

            builder.Entity<ApplicationUser>()
                .Property(c => c.gender)
                .HasConversion(
                c => c.ToString(),
                s => (Gender)Enum.Parse(typeof(Gender), s));

            builder.Entity<ApplicationUser>()
                .Property(c => c.Role)
                .HasConversion(
                c => c.ToString(),
                s => (UserRole)Enum.Parse(typeof(UserRole), s));
        }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<DiscountCode_Coupon> DiscountCode_Coupons { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
