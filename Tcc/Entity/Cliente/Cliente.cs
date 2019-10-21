using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class Cliente : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int clienteid { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public DateTime? datanascimento { get; set; }
    }
}