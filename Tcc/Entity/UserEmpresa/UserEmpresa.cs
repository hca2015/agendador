using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class UserEmpresa : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userempresaid { get; set; }

        public int empresaid { get; set; }

        public int userid { get; set; }
    }
}