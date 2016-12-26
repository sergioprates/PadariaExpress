using PadariaExpress.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadariaExpress.Infra.Dados.Contexto
{
    public class PadariaExpressContexto : DbContext
    {
        public PadariaExpressContexto()
            : base("PadariaExpress")
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<BandeiraCartao> BandeirasCartao { get; set; }
        public DbSet<EnderecoUsuario> EnderecosUsuario { get; set; }
        public DbSet<FormaDePagamento> FormasDePagamento { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }        
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Padaria> Padarias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PeriodoFuncionamento> PeriodosFuncionamento { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ConviteFuncionario> ConvitesFuncionario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Configurando Campos Chave primária
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                    .Configure(p => p.IsKey());
            //Configurando campos do tipo texto para varchar
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            //Configurando campos do tipo texto maxlength = 200
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(200));
            modelBuilder.Entity<Padaria>().Property(x => x.Descricao).IsMaxLength();
            modelBuilder.Entity<Padaria>().Ignore(x => x.Distancia);
            modelBuilder.Entity<Produto>().Property(x => x.Descricao).IsMaxLength();
            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<Proprietario>().ToTable("Proprietarios");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Usuario>()
                .HasRequired(t => t.Sexo)
                    .WithMany(x=> x.Usuarios)
                            .Map(x=> x.MapKey("SexoId"));

            modelBuilder.Entity<EnderecoUsuario>()
                .HasRequired(t => t.Usuario)
                    .WithMany(t => t.EnderecosUsuario)
                        .Map(x => x.MapKey("UsuarioId"));

            modelBuilder.Entity<FormaDePagamento>()
                .HasOptional(t => t.BandeiraCartao)
                    .WithMany(x=> x.FormasDePagamentos)
                        .Map(x => x.MapKey("BandeiraCartaoId"));

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.Pedidos)
                    .WithRequired(x => x.Padaria)
                        .Map(x => x.MapKey("PadariaId"));

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.Categorias)
                    .WithRequired(x => x.Padaria)
                        .Map(x => x.MapKey("PadariaId"));

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.PeriodosDeFuncionamento)
                    .WithRequired(x => x.Padaria)
                        .Map(x => x.MapKey("PadariaId"));

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.FormasDePagamento)
                    .WithRequired(x => x.Padaria)
                        .Map(x => x.MapKey("PadariaId"));

            modelBuilder.Entity<Categoria>()
                .HasMany(x => x.Produtos)
                .WithRequired(x => x.Categoria)
                    .Map(x => x.MapKey("CategoriaId"));

            modelBuilder.Entity<Pedido>()
                .HasMany(t => t.Itens)
                    .WithRequired(x => x.Pedido)
                        .Map(x => x.MapKey("PedidoId"));

            modelBuilder.Entity<Cliente>()
                .HasMany(t => t.Pedidos)
                    .WithRequired(x => x.Cliente)
                        .Map(x => x.MapKey("UsuarioId"));

            modelBuilder.Entity<ItemPedido>()
                .HasRequired(t => t.Produto)
                    .WithMany(x=> x.ItensPedido)
                        .Map(x => x.MapKey("ProdutoId"));

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.Funcionarios)
                    .WithMany(t => t.Padarias)
                        .Map(m =>
                        {
                            m.ToTable("PadariaFuncionario");
                            m.MapLeftKey("PadariaId");
                            m.MapRightKey("UsuarioId");
                        });

            modelBuilder.Entity<Padaria>()
                .HasMany(t => t.Proprietarios)
                    .WithMany(t => t.Padarias)
                        .Map(m =>
                        {
                            m.ToTable("PadariaProprietario");
                            m.MapLeftKey("PadariaId");
                            m.MapRightKey("UsuarioId");
                        });

            
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    entry.Property("DataAlteracao").IsModified = true;
                }
            }

            return base.SaveChanges();
        }
    }
}
