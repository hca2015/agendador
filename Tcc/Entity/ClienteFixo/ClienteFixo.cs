using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixo : Modelo
    {
        public enum TipoFrequencia
        {
            Diario,
            Semanal,
            Quinzenal,
            Mensal
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal clienteid { get; set; }
        public DateTime dataultimopagamento { get; set; }
        public int tipofrequencia { get; set; }
        public int horario { get; set; }
    }
}