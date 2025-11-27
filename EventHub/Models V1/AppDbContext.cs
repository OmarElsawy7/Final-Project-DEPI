//using Microsoft.EntityFrameworkCore;
//using EventHub.ModelsV1;

//namespace EventHub.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//        {
//        }

//        // Registering all our models as tables in the database
//        public DbSet<User> Users { get; set; }
//        public DbSet<Event> Events { get; set; }
//        public DbSet<Ticket> Tickets { get; set; }
//        public DbSet<Payment> Payments { get; set; }
//        public DbSet<Pending> Pendings { get; set; }
//        public DbSet<CheckIn> CheckIns { get; set; }

//        // (Optional) If we need to customize relationships later, we do it here
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Example: Setting decimal precision for all money fields to avoid warnings
//            modelBuilder.Entity<Event>()
//                .Property(e => e.Price)
//                .HasColumnType("decimal(18,2)");

//            modelBuilder.Entity<Ticket>()
//               .Property(t => t.Price)
//               .HasColumnType("decimal(18,2)");

//            modelBuilder.Entity<Payment>()
//               .Property(p => p.Amount)
//               .HasColumnType("decimal(18,2)");

//            modelBuilder.Entity<Pending>()
//               .Property(p => p.TotalAmount)
//               .HasColumnType("decimal(18,2)");
//        }
//    }
//}