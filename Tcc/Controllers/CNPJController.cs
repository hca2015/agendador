using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Tcc.Entity;
using Tcc.Models;

namespace Tcc.Controllers
{
    //[Authorize]
    public class CNPJController : Controller
    {
        [HttpGet]
        public ActionResult CadastrarCNPJ()
        {
            return View();
        }    
                
        public JsonResult cnpjReceita(string cnpj)
        {
            ConsultaReceita cs = new ConsultaReceita()
            {
                cnpj = Regex.Replace(cnpj, @"\W+", "")
            };

            var link = "https://www.receitaws.com.br/v1/cnpj/"+cs.cnpj;

            WebRequest _request = WebRequest.Create(link);

            _request.Method = "GET";

            WebResponse response = _request.GetResponse();

            string responseText;

            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
            {
                responseText = reader.ReadToEnd();
            }

            var responseObject = JsonConvert.DeserializeObject<empresa>(responseText);

            //return View("CadastrarCNPJ", responseObject);
            return Json(responseObject);
        }  
        
        public JsonResult cadastrarEmpresa(empresa prEmpresa)
        {
            EmpresaRepository empresaRepository = new EmpresaRepository();

            if (!empresaRepository.add(prEmpresa))
                return Json(false);
            else
                return Json(true);
        }
    }
}