using Api_SalesDatePrediction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Definimos las DbSet para cada entidad
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Customers> Customers { get; set; }
    public DbSet<Employees> Employees { get; set; }
    public DbSet<Shippers> Shippers { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    public DbSet<Products> products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Asegura que la entidad "Order" esté correctamente mapeada a la tabla "Order"
        modelBuilder.Entity<Orders>().ToTable("Orders");
        // Si tienes más entidades, agrégalas aquí
        modelBuilder.Entity<Customers>().ToTable("Customers"); // Verifica si necesitas definir claves primarias
        modelBuilder.Entity<Employees>().ToTable("Employees");
        modelBuilder.Entity<Shippers>().ToTable("Shippers");
        modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");
        modelBuilder.Entity<Products>().ToTable("Products");

        // Configuración de la entidad Order
        modelBuilder.Entity<Orders>(entity =>
         {
             entity.ToTable("Orders", schema: "Sales");

             entity.HasKey(e => e.Orderid); // Clave primaria

             entity.Property(e => e.Orderid).HasColumnName("orderid");
             entity.Property(e => e.Custid).HasColumnName("custid");
             entity.Property(e => e.Empid).HasColumnName("empid");
             entity.Property(e => e.Orderdate).HasColumnName("orderdate");
             entity.Property(e => e.Requireddate).HasColumnName("requireddate");
             entity.Property(e => e.Shippeddate).HasColumnName("shippeddate");
             entity.Property(e => e.Shipperid).HasColumnName("shipperid");
             entity.Property(e => e.Freight).HasColumnName("freight");
             entity.Property(e => e.Shipname).HasColumnName("shipname");
             entity.Property(e => e.Shipaddress).HasColumnName("shipaddress");
             entity.Property(e => e.Shipcity).HasColumnName("shipcity");
             entity.Property(e => e.Shipregion).HasColumnName("shipregion");
             entity.Property(e => e.Shippostalcode).HasColumnName("shippostalcode");
             entity.Property(e => e.Shipcountry).HasColumnName("shipcountry");

             // Relaciones con otras tablas
             entity.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.Custid);

             entity.HasOne(o => o.Employee)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(o => o.Empid);

             entity.HasOne(o => o.Shipper)
                   .WithMany(s => s.Orders)
                   .HasForeignKey(o => o.Shipperid);
         });

         // Configuración de la entidad Customer
         modelBuilder.Entity<Customers>(entity =>
         {
             entity.ToTable("Customers", schema: "Sales");
             entity.HasKey(c => c.Custid); // Definimos la clave primaria

             entity.Property(c => c.Custid).HasColumnName("custid");
             entity.Property(c => c.Companyname).HasColumnName("companyname");
             entity.Property(c => c.Contactname).HasColumnName("contactname");
             entity.Property(c => c.Address).HasColumnName("address");
             entity.Property(c => c.City).HasColumnName("city");
             entity.Property(c => c.Postalcode).HasColumnName("postalcode");
             entity.Property(c => c.Country).HasColumnName("country");
         });

         // Configuración de la entidad Employee
         modelBuilder.Entity<Employees>(entity =>
         {
             entity.ToTable("Employees", schema: "HR"); // Especifica el esquema HR
             entity.HasKey(e => e.Empid);

             entity.Property(e => e.Empid).HasColumnName("empid");
             entity.Property(e => e.Firstname).HasColumnName("firstname");
             entity.Property(e => e.Lastname).HasColumnName("lastname");
         });

        // Configuración de la entidad Employee
        modelBuilder.Entity<Products>(entity =>
        {
            entity.ToTable("Products", schema: "Production"); // Especifica el esquema HR
            entity.HasKey(e => e.Productid);

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Productname).HasColumnName("productname");
        });

        // Configuración de la entidad Shipper
        modelBuilder.Entity<Shippers>(entity =>
         {
             entity.ToTable("Shippers", schema: "Sales");
             entity.HasKey(s => s.Shipperid);

             entity.Property(s => s.Shipperid).HasColumnName("shipperid");
             entity.Property(s => s.Companyname).HasColumnName("companyname");
         });

         // Configuración de la entidad OrderDetail
         modelBuilder.Entity<OrderDetails>(entity =>
         {
             entity.ToTable("OrderDetails");

             entity.HasKey(od => new { od.Orderid, od.Productid }); // Clave compuesta

             entity.Property(od => od.Orderid).HasColumnName("OrderID");
             entity.Property(od => od.Productid).HasColumnName("ProductID");
             entity.Property(od => od.Qty).HasColumnName("Quantity");

             entity.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.Orderid);
         });
    }
}
