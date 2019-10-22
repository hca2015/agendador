using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tcc.Entity;

namespace Tcc.Controllers
{
    [Authorize]
    public class MinhaEmpresaController : AppController
    {
        public ActionResult MinhaEmpresa()
        {
            ViewBag.data = DateTime.Now.ToShortDateString();
            ViewBag.parametrizacao = getParametrizacao();
            ViewBag.agenda = getCreateAgenda();
            return View();
        }

        public string getParametrizacao()
        {
            ParametrizacaoAgenda lRetorno = null;

            ParametrizacaoAgendaRepository lParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();

            lRetorno = lParametrizacaoAgendaRepository.getUser(usuario.userid);

            if (lRetorno == null)
                aContextoExecucao.addMessage("Parametrização de horários não encontrada! Incluir nova!", Message.kdType.Info);
                        
            return aContextoExecucao.withErrororWarn() ? JsonConvert.SerializeObject(aContextoExecucao.Messages) : JsonConvert.SerializeObject(lRetorno);
        }

        public JsonResult incluirAlterarParametrizacao(ParametrizacaoAgenda prParametrizacaoAgenda)
        {
            ParametrizacaoAgenda lRetorno = null;

            if (prParametrizacaoAgenda.PARAMETRIZACAOAGENDAID == 0)
            {
                IncluirParametrizacaoAgenda lIncluirParametrizacaoAgenda = new IncluirParametrizacaoAgenda();

                if(!lIncluirParametrizacaoAgenda.incluir(prParametrizacaoAgenda))
                    aContextoExecucao.add(lIncluirParametrizacaoAgenda.Messages);
            }
            else
            {
                AlterarParametrizacaoAgenda lAlterarParametrizacaoAgenda = new AlterarParametrizacaoAgenda();

                if(!lAlterarParametrizacaoAgenda.alterar(prParametrizacaoAgenda))
                    aContextoExecucao.add(lAlterarParametrizacaoAgenda.Messages);
            }

            if (aContextoExecucao.withoutError())
                lRetorno = prParametrizacaoAgenda;
           
            return aContextoExecucao.withError() ? Json(aContextoExecucao.Messages) : Json(lRetorno);
        }
        
        public List<AgendaDTO> getCreateAgenda()
        {
            AgendaRepository lAgendaRepository = new AgendaRepository();
            List<AgendaDTO> lRetorno = lAgendaRepository.getAgendaDTO(DateTime.Now.Date);

            if (lRetorno.Count == 0) {
                CriarAgendaAutomatica lCriarAgendaAutomatica = new CriarAgendaAutomatica(aContextoExecucao);
                lRetorno = lCriarAgendaAutomatica.acoAgendaDTO;
            }

            return lRetorno;
        }

        public JsonResult getCreateAgenda(DateTime prData)
        {
            AgendaRepository lAgendaRepository = new AgendaRepository();
            List<AgendaDTO> lRetorno = lAgendaRepository.getAgendaDTO(prData);

            if (lRetorno.Count == 0)
            {
                CriarAgendaAutomatica lCriarAgendaAutomatica = new CriarAgendaAutomatica(aContextoExecucao);
                lRetorno = lCriarAgendaAutomatica.acoAgendaDTO;
            }

            return Json(lRetorno);
        }
    }
}