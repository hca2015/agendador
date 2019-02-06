using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tcc.Entity
{
    //https://www.receitaws.com.br/v1/cnpj/27865757000102
    public class ConsultaReceita
    {
        [Display(Name = "CNPJ")]
        [DisplayFormat(DataFormatString = "{0:00.000.000/0000-00}", ApplyFormatInEditMode = true)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "CNPJ deve ser numérico")]
        [MaxLength(14, ErrorMessage = "Náo deve exceder 14 caracteres")]   
        [Required]
        [Key]
        public string cnpj { get; set; }

        [Display(Name = "Razao Social")]
        public string nome { get; set; }

        [Display(Name = "Situação")]
        public string situacao { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string fantasia { get; set; }

        [Display(Name = "Logradouro")]
        public string logradouro { get; set; }

        [Display(Name = "Número")]
        public string numero { get; set; }

        [Display(Name = "Bairro")]
        public string bairro { get; set; }    
        
        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Display(Name = "Municipio")]
        public string municipio { get; set; }

        [Display(Name = "Estado")]
        public string uf { get; set; }

        public List<atividades> atividade_principal { get; set; }
    }

    public class atividades
    {
        public string text { get; set; }
        [Key]
        public string code { get; set; }
    }
}