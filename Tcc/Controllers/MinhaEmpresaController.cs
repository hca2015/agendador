using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tcc.Entity;

namespace Tcc.Controllers
{
    [Authorize]
    public class MinhaEmpresaController : AppController
    {
        //private const string _dateFormat = "dd-MM-yyyy HH:mm:ss";
        private const string _dateFormat = "yyyy-MM-dd HH:mm:ss";
        public ActionResult MinhaEmpresa(DateTime? prData = null)
        {
            if (!prData.HasValue)
                ViewBag.data = DateTime.Now.ToShortDateString();
            else
                ViewBag.data = prData.Value.ToShortDateString();

            ViewBag.parametrizacao = getParametrizacao();

            var isoConvert = new IsoDateTimeConverter();
            isoConvert.DateTimeFormat = _dateFormat;

            ViewBag.agenda = JsonConvert.SerializeObject(getCreateAgenda(prData), isoConvert);
            return View();
        }

        public string getParametrizacao()
        {
            ParametrizacaoAgenda lRetorno = null;

            ParametrizacaoAgendaRepository lParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();

            if (empresa != null)
                lRetorno = lParametrizacaoAgendaRepository.getEmpresa(empresa.empresaid);

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

                if (!lIncluirParametrizacaoAgenda.incluir(prParametrizacaoAgenda))
                    aContextoExecucao.add(lIncluirParametrizacaoAgenda.Messages);
            }
            else
            {
                AlterarParametrizacaoAgenda lAlterarParametrizacaoAgenda = new AlterarParametrizacaoAgenda();

                if (!lAlterarParametrizacaoAgenda.alterar(prParametrizacaoAgenda))
                    aContextoExecucao.add(lAlterarParametrizacaoAgenda.Messages);
            }

            if (aContextoExecucao.withoutError())
                lRetorno = prParametrizacaoAgenda;

            return aContextoExecucao.withError() ? Json(aContextoExecucao.Messages) : Json(lRetorno);
        }

        public List<AgendaDTO> getCreateAgenda(DateTime? prData = null)
        {
            DateTime lData = prData == null ? DateTime.Now.Date : prData.Value.Date;
            //REMOVER***************************************************************************
            //lData = new DateTime(2019, 10, 23);

            AgendaRepository lAgendaRepository = new AgendaRepository();
            List<AgendaDTO> lRetorno = new List<AgendaDTO>();

            if (empresa != null)
            {
                lRetorno = lAgendaRepository.getAgendaDTO(lData, empresa.empresaid);

                if (lRetorno.Count == 0)
                {
                    CriarAgendaAutomatica lCriarAgendaAutomatica = new CriarAgendaAutomatica(aContextoExecucao);
                    lCriarAgendaAutomatica.criar(lData);
                    lRetorno = lCriarAgendaAutomatica.acoAgendaDTO;
                }

                foreach (var item in lRetorno)
                {
                    if (item.cliente != null && item.cliente.datanascimento.HasValue)
                        item.cliente.datanascimento = item.cliente.datanascimento.Value.Date;
                    if (item.cliente != null && item.cliente.dataultimoservico.HasValue)
                        item.cliente.dataultimoservico = item.cliente.dataultimoservico.Value.Date;
                }
            }

            return lRetorno;
        }

        public JsonResult getCreateAgendaData(string prData)
        {
            DateTime lData;
            List<AgendaDTO> lRetorno = new List<AgendaDTO>();

            if (DateTime.TryParse(prData, out lData) && empresa != null)
            {
                //REMOVER***************************************************************************
                //lData = new DateTime(2019, 10, 23);

                AgendaRepository lAgendaRepository = new AgendaRepository();
                lRetorno = lAgendaRepository.getAgendaDTO(lData, empresa.empresaid);

                if (lRetorno.Count == 0)
                {
                    CriarAgendaAutomatica lCriarAgendaAutomatica = new CriarAgendaAutomatica(aContextoExecucao);
                    lCriarAgendaAutomatica.criar(lData);
                    lRetorno = lCriarAgendaAutomatica.acoAgendaDTO;
                }
            }

            return new CustomJsonResult(true) { Data = lRetorno };
        }

        public JsonResult salvarAgenda(JsonObjectRequestBean prAgenda)
        {
            AgendaDTO lAgenda = null;
            EditarAgendaCompleta lEditarAgendaCompleta = new EditarAgendaCompleta(aContextoExecucao);

            try
            {
                lAgenda = JsonConvert.DeserializeObject<AgendaDTO>(prAgenda.JSONOBJECT);
            }
            catch (Exception e)
            {
                aContextoExecucao.addErro("Erro com as informações inseridas");
            }

            if (aContextoExecucao.withoutError() && lAgenda != null)
            {
                if (!lEditarAgendaCompleta.editar(lAgenda))
                {
                    return Json(aContextoExecucao.Messages);
                }
            }
            else
                return Json(aContextoExecucao.Messages);

            return Json(lEditarAgendaCompleta.aAgendaDTO);
        }


        public JsonResult limparAgenda(int pragendaid)
        {
            LimparAgenda lLimparAgenda = new LimparAgenda(aContextoExecucao);
            if (!lLimparAgenda.Limpar(pragendaid))
                return Json(aContextoExecucao.Messages);
            else
                return new CustomJsonResult() { Data = new AgendaDTO() { agenda = lLimparAgenda.aAgenda, empresa = base.empresa } };
        }
    }
}