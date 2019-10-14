using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoEmpresa : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal clientefixoempresaid { get; set; }
        public decimal empresaid { get; set; }
        public int clientefixoid { get; set; }
    }
}