using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class Servico : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int servicoid { get; set; }
        public int empresaid { get; set; }
        [Display(Name = "Nome do serviço")]
        public string descricao { get; set; }
        [Display(Name = "Valor do serviço")]
        public decimal valorservico { get; set; }
    }
}