using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class User : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        public string membershipid { get; set; }
        public string nome { get; set; }
        public DateTime? datanascimento { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public int? usermasterid { get; set; }
        public int ativo { get; set; }

        public static User hydrate(int userid)
        {
            return new UsersRepository().getId(userid);
        }

        public bool cpfDiferente(string cpf)
        {
            if (!string.IsNullOrWhiteSpace(this.cpf))
                return this.cpf == cpf;

            return true;
        }

        public bool isSlave()
        {
            return usermasterid != null;
        }
    }
}