using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class Agenda : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal agendaid { get; set; }
        public int empresaid { get; set; }
        public decimal servicoid { get; set; }
        public decimal clienteid { get; set; }
        public int horaini { get; set; }
        public int horafim { get; set; }
        public int JANELA { get; set; }
    }
}