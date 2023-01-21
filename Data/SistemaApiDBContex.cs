using Microsoft.EntityFrameworkCore;
using SistemaAPI.Data.Map;
using SistemaAPI.Model;

namespace SistemaAPI.Data
{
    public class SistemaApiDBContex : DbContext
    {
        public SistemaApiDBContex(DbContextOptions<SistemaApiDBContex> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
