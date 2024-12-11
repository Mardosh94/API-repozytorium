using API_Auth.Modules.Customers;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Invoices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Data
{
    public class AppDbContext : IdentityDbContext<MyUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); // Wywołanie konfiguracji tożsamości

            //// Konfiguracja relacji Addresses
            //modelBuilder.Entity<Addresses>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //    entity.Property(x => x.City).IsRequired().HasMaxLength(100);
            //    entity.Property(x => x.PostCode).HasMaxLength(10);
            //});

            //// Konfiguracja relacji Suppliers
            //modelBuilder.Entity<Suppliers>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //    entity.HasOne(x => x.Address) // Relacja jeden-do-jeden
            //          .WithOne()
            //          .HasForeignKey<Suppliers>(s => s.AddressId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Konfiguracja relacji Customers
            //modelBuilder.Entity<Customers>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //    entity.HasOne(x => x.Address) // Relacja wiele-do-jednego
            //          .WithMany(x => x.Customers)
            //          .HasForeignKey(x => x.AddressId);
            //});

            //// Konfiguracja relacji Invoices
            //modelBuilder.Entity<Invoices>(entity =>
            //{
            //    //coś tu nie pasuje chyba bo w invoices.cs jest
            //    //info o tym ze raz taki wpis a raz taki

            //    entity.HasKey(x => x.Id);
            //    entity.HasOne(x => x.Customer)
            //    .WithOne()
            //    .HasForeignKey<Invoices>(x => x.CustomerId); //Relacja jeden-do-jeden
            //    entity.HasOne(x => x.Supplier)
            //    .WithOne()
            //    .HasForeignKey<Invoices>(x => x.SupplierId);//Relacja jeden-do-jeden
            //});

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, City = "Warszawa", PostCode = "00-001", Street = "Marszałkowska", BuildingNumber = "10" },
                new Address { Id = 2, City = "Kraków", PostCode = "30-001", Street = "Floriańska", BuildingNumber = "5" }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Supplier A", AddressId = 1, PhoneNumber = "123456789", Email = "supplierA@example.com" },
                new Supplier { Id = 2, Name = "Supplier B", AddressId = 2, PhoneNumber = "987654321", Email = "supplierB@example.com" }
            );
        }
    }
}
