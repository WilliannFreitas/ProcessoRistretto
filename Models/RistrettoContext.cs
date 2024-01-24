using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ProcessoRistretto.Models
{
    public class RistrettoContext : DbContext
    {
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Correção: O parâmetro deve ser DbContextOptions<RistrettoContext>
        public RistrettoContext(DbContextOptions<RistrettoContext> options) : base(options)
        {

        }
    }
}
