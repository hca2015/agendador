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
    public class ClienteFixoController : AppController
    {
        private ClienteFixoRepository db = new ClienteFixoRepository();

        // GET: ClienteFixoes
        public ActionResult Index()
        {
            return View(db.ClienteFixos.ToList());
        }

        // GET: ClienteFixoes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixo clienteFixo = db.ClienteFixos.Find(id);
            if (clienteFixo == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixo);
        }

        // GET: ClienteFixoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteFixoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clienteid,dataultimopagamento,tipofrequencia")] ClienteFixo clienteFixo)
        {
            if (ModelState.IsValid)
            {
                db.ClienteFixos.Add(clienteFixo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clienteFixo);
        }

        // GET: ClienteFixoes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixo clienteFixo = db.ClienteFixos.Find(id);
            if (clienteFixo == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixo);
        }

        // POST: ClienteFixoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clienteid,dataultimopagamento,tipofrequencia")] ClienteFixo clienteFixo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteFixo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienteFixo);
        }

        // GET: ClienteFixoes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteFixo clienteFixo = db.ClienteFixos.Find(id);
            if (clienteFixo == null)
            {
                return HttpNotFound();
            }
            return View(clienteFixo);
        }

        // POST: ClienteFixoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ClienteFixo clienteFixo = db.ClienteFixos.Find(id);
            db.ClienteFixos.Remove(clienteFixo);
            db.SaveChanges();
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
    }
}
