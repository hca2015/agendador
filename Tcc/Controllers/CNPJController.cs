using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Tcc.Entity;

namespace Tcc.Controllers
{
    [Authorize]
    public class CNPJController : AppController
    {
        [HttpGet]
        public ActionResult CadastrarCNPJ()
        {
            return View();
        }

        public ActionResult MinhaEmpresa()
        {
            ViewBag.data = DateTime.Now.ToShortDateString();
            return View();
        }

        public JsonResult cnpjReceita(string cnpj)
        {
            cnpj = Utility.removerCaracteresEspeciais(cnpj);

            if (ValidaCNPJ.IsCnpj(cnpj))
            {
                ConsultaReceita cs = new ConsultaReceita()
                {
                    cnpj = Regex.Replace(cnpj, @"\W+", "")
                };

                string link = "https://www.receitaws.com.br/v1/cnpj/" + cs.cnpj;

                WebRequest _request = WebRequest.Create(link);

                _request.Method = "GET";

                WebResponse response = _request.GetResponse();

                string responseText;

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                {
                    responseText = reader.ReadToEnd();
                }

                Empresa responseObject = JsonConvert.DeserializeObject<Empresa>(responseText);
                //return View("CadastrarCNPJ", responseObject);
                return Json(responseObject);
            }
            else
            {
                aContextoExecucao.addErro("CNPJ Inválido para consulta");
                Response.StatusCode = 500; //Write your own error code
                Response.Write(JsonConvert.SerializeObject(aContextoExecucao.Messages));
                return null;
            }
            //return Json(new Message("CNPJ Inválido para consulta", Message.kdType.Error));
        }

        public ActionResult cadastrarEmpresa(Empresa prEmpresa)
        {
            IncluirEmpresa incluirEmpresa = new IncluirEmpresa();

            if (string.IsNullOrWhiteSpace(prEmpresa.cnpj))
            {
                aContextoExecucao.addErro("Informar parametro");
                return Json(new Message("Informar parametro", Message.kdType.Error));
            }

            if (incluirEmpresa.incluir(prEmpresa, usuario.userid))
            {
                return Json(incluirEmpresa.Messages);
            }

            return Json(incluirEmpresa.Messages);
        }
    }
}