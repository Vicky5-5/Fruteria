namespace LogicaBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaCarrito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrito",
                c => new
                    {
                        idCarrito = c.Guid(nullable: false),
                        idProducto = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        NombreProducto = c.String(),
                        CantidadCompra = c.Int(nullable: false),
                        totalProductos = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idCarrito)
                .ForeignKey("dbo.Productos", t => t.idProducto, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idProducto)
                .Index(t => t.idUsuario);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        idProducto = c.Int(nullable: false, identity: true),
                        ImagenURL = c.String(),
                        NombreProducto = c.String(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Categoria = c.Int(nullable: false),
                        Oferta = c.Decimal(precision: 18, scale: 2),
                        Origen = c.String(nullable: false),
                        EnTemporada = c.Boolean(nullable: false),
                        Usuario_idUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.idProducto)
                .ForeignKey("dbo.Usuario", t => t.Usuario_idUsuario)
                .Index(t => t.Usuario_idUsuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 4000),
                        Estado = c.Boolean(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        FechaBaja = c.DateTime(),
                        Direccion = c.String(),
                        Administrador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idUsuario);
            
            CreateTable(
                "dbo.DetallesPedidos",
                c => new
                    {
                        idPedido = c.Int(nullable: false),
                        idProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Oferta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.idPedido, t.idProducto })
                .ForeignKey("dbo.Pedidos", t => t.idPedido, cascadeDelete: true)
                .Index(t => t.idPedido);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        idPedido = c.Int(nullable: false, identity: true),
                        idUsuario = c.Int(nullable: false),
                        FechaPedido = c.DateTime(nullable: false),
                        FechaEstimadaEntrega = c.DateTime(nullable: false),
                        EstadoPedido = c.Boolean(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idPedido);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetallesPedidos", "idPedido", "dbo.Pedidos");
            DropForeignKey("dbo.Carrito", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Productos", "Usuario_idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Carrito", "idProducto", "dbo.Productos");
            DropIndex("dbo.DetallesPedidos", new[] { "idPedido" });
            DropIndex("dbo.Productos", new[] { "Usuario_idUsuario" });
            DropIndex("dbo.Carrito", new[] { "idUsuario" });
            DropIndex("dbo.Carrito", new[] { "idProducto" });
            DropTable("dbo.Pedidos");
            DropTable("dbo.DetallesPedidos");
            DropTable("dbo.Usuario");
            DropTable("dbo.Productos");
            DropTable("dbo.Carrito");
        }
    }
}
