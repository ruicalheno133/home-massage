namespace HomeMassageWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Massagem")]
    public partial class Massagem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Massagem()
        {
            Servicoes = new HashSet<Servico>();
        }

        [Key]
        public int Id_Massagem { get; set; }

        [Required]
        [StringLength(15)]
        public string Nome { get; set; }

        [Column(TypeName = "money")]
        public decimal Preco { get; set; }

        public int Duracao { get; set; }

        [Required]
        [StringLength(250)]
        public string Descricao { get; set; }

        [Column(TypeName = "image")]
        public byte[] Imagem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servico> Servicoes { get; set; }
    }
}
