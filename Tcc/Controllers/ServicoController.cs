using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcc.Entity;

namespace Tcc.Controllers
{
    [Authorize]
    public class ServicoController : AppController
    {
        public ActionResult Servico()
        {
            List<Servico> result = new ServicoRepository().getUser(usuario.userid);

            if (result == null)
                result = new List<Servico>();

            return View(result);
        }

        public ActionResult Edit(int id)
        {
            return View(new ServicoRepository().getId(id));
        }

        public ActionResult Delete(int id)
        {
            return View(new ServicoRepository().getId(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult Inserir(Servico model)
        {
            if (!ModelState.IsValid)
            {
                aContextoExecucao.addErro("Verifique as informações inseridas!");
                return Json(aContextoExecucao.Messages);
            }

            IncluirServicoTaskActivity lIncluirServicoTaskActivity = new IncluirServicoTaskActivity();

            if (!lIncluirServicoTaskActivity.incluir(model))
                return Json(lIncluirServicoTaskActivity.Messages);

            return Json(aContextoExecucao.Messages);
        }

        public ActionResult Editar(Servico model)
        {
            if (!ModelState.IsValid)
            {
                aContextoExecucao.addErro("Verifique as informações inseridas!");
                return View("Edit", model);
            }

            AlterarServicoTaskActivity lAlterarServicoTaskActivity = new AlterarServicoTaskActivity();

            if(!lAlterarServicoTaskActivity.alterar(model))
                return View("Edit", model);

            return RedirectToAction("Servico");
        }

        public ActionResult Excluir(int id)
        {
            Servico lServico = new ServicoRepository().getId(id);

            if (lServico == null)
                return View("Error");

            ExcluirServicoTaskActivity lExcluirServicoTaskActivity = new ExcluirServicoTaskActivity();

            if(!lExcluirServicoTaskActivity.excluir(lServico))
                return RedirectToAction("Delete", lServico);

            return RedirectToAction("Servico");
        }

        public ActionResult searchservico(string term)
        {
            return Json(new ServicoRepository().Servicos.Where(c => c.descricao.ToUpper().StartsWith(term.ToUpper())).Select(a => new { label = a.descricao, id = a.servicoid }), JsonRequestBehavior.AllowGet);
        }
    }
}