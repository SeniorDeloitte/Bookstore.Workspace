using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        // --- AGREGAMOS ESTE MÉTODO PARA CONFIGURAR EL DECIMAL ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la propiedad Price para que tenga un máximo de 18 dígitos y 2 decimales
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);
        }
    }
}
