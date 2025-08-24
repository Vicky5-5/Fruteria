namespace LogicaBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCascadeRules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidos", "idCarrito", c => c.Guid(nullable: false));
            CreateIndex("dbo.Pedidos", "idUsuario");
            CreateIndex("dbo.Pedidos", "idCarrito");
            AddForeignKey("dbo.Pedidos", "idCarrito", "dbo.Carrito", "idCarrito");
            AddForeignKey("dbo.Pedidos", "idUsuario", "dbo.Usuario", "idUsuario");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidos", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Pedidos", "idCarrito", "dbo.Carrito");
            DropIndex("dbo.Pedidos", new[] { "idCarrito" });
            DropIndex("dbo.Pedidos", new[] { "idUsuario" });
            DropColumn("dbo.Pedidos", "idCarrito");
        }
    }
}
