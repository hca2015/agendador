using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tcc.Entity;
using Tcc.Models;

namespace Tcc.Controllers
{
    [Authorize]
    public class CNPJController : Controller
    {
        [HttpGet]
        public ActionResult CadastrarCNPJ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarCNPJIncluir(CadastrarCNPJ model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Contexto c = new Contexto();
            c.EntityManager(model, Contexto.KdOperation.kdInsert);

            return null;
        }
    }
}