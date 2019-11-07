using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoEmpresa : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int clientefixoempresaid { get; set; }
        public int empresaid { get; set; }
        public int clientefixoid { get; set; }
    }
}