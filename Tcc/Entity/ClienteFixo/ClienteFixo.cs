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
        public int clientefixoid { get; set; }
        public int servicoid { get; set; }
        public int clienteid { get; set; }
        public DateTime? dataultimoservico { get; set; }
        public int diasemana { get; set; }
        public int tipofrequencia { get; set; }
        public int horario { get; set; }
    }
}