using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tcc.Models
{
    public abstract class CadastrarModels
    {       
        
    }

    [Authorize]
    public class CadastrarCNPJ : Modelo
    {
        [MaxLength(120, ErrorMessage = "Não deve ultrapassar 120 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string NomeFantasia { get; set; }
        [Display(Name = "Razao Social")]
        [MaxLength(120, ErrorMessage = "Não deve ultrapassar 120 caracteres")]
        [Required]
        public string RazaoSocial { get; set; }
        [Display(Name = "CNPJ ou CPF")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CNPJ/CPF deve ser numérico")]
        [MinLength(11)]
        [MaxLength(14)]
        [Required]
        public string CNPJ { get; set; }
        [Display(Name = "Serviço oferecido")]
        [MaxLength(60, ErrorMessage = "Não deve ultrapassar 60 caracteres")]
        [Required]
        public string Servico { get; set; }            
    }
}