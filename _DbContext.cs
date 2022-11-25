using Microsoft.EntityFrameworkCore;
using ClientesModel;

namespace _DbContext
{
    public class ClienteContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;database=dbCliente;uid=sa;password=dojpM88NHC&JNAm56Z&;TrustServerCertificate=True;Encrypt=false");

    }
}