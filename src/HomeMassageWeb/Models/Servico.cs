namespace HomeMassageWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servico")]
    public partial class Servico
    {
        [Key]
        public int Id_Servico { get; set; }

        public int Cliente { get; set; }

        public int Funcionario { get; set; }

        public int Massagem { get; set; }

        public DateTime Data { get; set; }

        [Required]
        [StringLength(19)]
        public string Cartao_Credito { get; set; }

        public bool Estado { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(8)]
        public string Codigo_Postal { get; set; }

        public virtual Cliente Cliente1 { get; set; }

        public virtual Funcionario Funcionario1 { get; set; }

        public virtual Massagem Massagem1 { get; set; }
    }
}
