using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tcc.Entity;

namespace Tcc.Controllers
{
    public class ClienteController : AppController
    {
        private ClienteFixoRepository db = new ClienteFixoRepository();

        // GET: Cliente
        public ActionResult Index()
        {
            return View(db.getEmpresa(empresa.empresaid));
        }

        // GET: Cliente/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixoDTO clienteFixoDTO = null;// db.ClienteFixoDTOes.Find(id);
            if (clienteFixoDTO == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixoDTO);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteFixoDTO clienteFixoDTO)
        {
            if (ModelState.IsValid)
            {
                Empresa e = new EmpresaRepository().getUser(usuario.userid);

                if(e == null)
                {
                    ModelState.AddModelError("error", "Deve estar associado a uma empresa");
                    return View(clienteFixoDTO);
                }

                clienteFixoDTO.empresaid = e == null ? 0 : e.empresaid;
                clienteFixoDTO.documento = clienteFixoDTO.documento.removerCaracteresEspeciais();

                IncluirClienteFixo lIncluirClienteFixo = new IncluirClienteFixo();
                if (!lIncluirClienteFixo.incluir(clienteFixoDTO))
                {
                    ModelState.AddModelError("error", "Houve um erro na requisição");
                    return View(clienteFixoDTO);
                }
                    

                return RedirectToAction("Index");
                //return RedirectToAction("Index");
            }

            return View(clienteFixoDTO);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixoDTO clienteFixoDTO = null;//db.ClienteFixoDTOes.Find(id);
            if (clienteFixoDTO == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixoDTO);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clienteid,dataultimopagamento,tipofrequencia,horario,nomecliente,datanascimento,documento,nomeservico,empresaid")] ClienteFixoDTO clienteFixoDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteFixoDTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienteFixoDTO);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixoDTO clienteFixoDTO = db.getIdDTO(id);
            if (clienteFixoDTO == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixoDTO);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExcluirClienteFixo lExcluirClienteFixo = new ExcluirClienteFixo();
            lExcluirClienteFixo.excluir(new ClienteFixoRepository().getId(id));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult searchdocumento(string term)
        {
            return new CustomJsonResult() { Data = new ClienteRepository().Clientes.Where(c => c.documento.StartsWith(term.ToUpper())).Select(a => new { label = a.documento + " - " + a.nome, nascimento = a.datanascimento, name = a.nome, documento = a.documento, id = a.clienteid }) };
        }
    }
}
