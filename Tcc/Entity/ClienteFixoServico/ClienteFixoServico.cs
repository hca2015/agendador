using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoServico : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal clientefixoservicoid { get; set; }
        public decimal empresaid { get; set; }
        public int clientefixoid { get; set; }
    }
}