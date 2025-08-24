using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LogicaBiblioteca.Modelos;


namespace LogicaBiblioteca.Contexto
{
    public class FruteriaContext : DbContext
    {
        public FruteriaContext() : base("FrutaContext")
        {
        }

        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<DetallesPedidos> DetallesPedidos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Evita eliminación en cascada de Pedidos al eliminar Usuario
            modelBuilder.Entity<Pedidos>()
                .HasRequired(p => p.oUsuario)
                .WithMany()
                .HasForeignKey(p => p.idUsuario)
                .WillCascadeOnDelete(false);

            // Evita eliminación en cascada de Pedidos al eliminar Carrito
            modelBuilder.Entity<Pedidos>()
                .HasRequired(p => p.oCarrito)
                .WithMany()
                .HasForeignKey(p => p.idCarrito)
                .WillCascadeOnDelete(false);
        }
    }
}