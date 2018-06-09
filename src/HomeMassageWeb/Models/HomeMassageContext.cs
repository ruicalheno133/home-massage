namespace HomeMassageWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HomeMassageContext : DbContext
    {
        public HomeMassageContext()
            : base("name=HomeMassageContext")
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Massagem> Massagems { get; set; }
        public virtual DbSet<Servico> Servicoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Numero_Contribuinte)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Servicoes)
                .WithRequired(e => e.Cliente1)
                .HasForeignKey(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .HasMany(e => e.Servicoes)
                .WithRequired(e => e.Funcionario1)
                .HasForeignKey(e => e.Funcionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Massagem>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Massagem>()
                .Property(e => e.Preco)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Massagem>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Massagem>()
                .HasMany(e => e.Servicoes)
                .WithRequired(e => e.Massagem1)
                .HasForeignKey(e => e.Massagem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servico>()
                .Property(e => e.Cartao_Credito)
                .IsUnicode(false);

            modelBuilder.Entity<Servico>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Servico>()
                .Property(e => e.Codigo_Postal)
                .IsUnicode(false);
        }
    }
}
