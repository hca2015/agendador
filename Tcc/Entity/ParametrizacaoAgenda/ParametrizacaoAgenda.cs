using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ParametrizacaoAgenda : Modelo
    {
        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PARAMETRIZACAOAGENDAID { get; set; }
        //[ForeignKey("EMPRESA")]
        public int empresaid { get; set; }
        public int HORAINI { get; set; }
        public int HORAFIM { get; set; }
    }
}