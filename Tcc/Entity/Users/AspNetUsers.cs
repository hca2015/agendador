using System.ComponentModel.DataAnnotations;

namespace Tcc.Entity
{
    public class AspNetUsers : Modelo
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
    }
}