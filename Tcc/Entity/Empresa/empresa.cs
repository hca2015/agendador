using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class empresa : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EMPRESAID { get; set; }
        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }

        [Display(Name = "Razao Social")]
        public string nome { get; set; }

        [Display(Name = "Situação")]
        public string situacao { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string fantasia { get; set; }

        [Display(Name = "Logradouro")]
        public string logradouro { get; set; }

        [Display(Name = "Número")]
        public string numero { get; set; }

        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Display(Name = "Municipio")]
        public string municipio { get; set; }

        [Display(Name = "Estado")]
        public string uf { get; set; }
    }
}