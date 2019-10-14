using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using Tcc.Entity;
using Tcc.Models;

namespace Tcc.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : AppController
    {
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        // GET: Admin
        public ActionResult Index()
        {            
            //List<ApplicationUser> a = new Contexto().getAllUsers();
            return View();
        }

        public ActionResult AdministrarUsuarios()
        {
            return View(new ApplicationDbContext().Users.ToList());
        }

        public ActionResult PainelAdm()
        {
            return View(new ApplicationDbContext().Users.ToList());
        }

        public ActionResult DeletarUsuario(string id)
        {
            ApplicationDbContext contexto = new ApplicationDbContext();

            contexto.Users.Remove(contexto.Users.Where(x => x.Id == id).FirstOrDefault());

            if (contexto.SaveChanges() > 0)
                return View("PainelAdm", contexto.Users.ToList());
            else
                return View("PainelAdm", contexto.Users.ToList());
        }

        public ActionResult ManageRoles()
        {                        
            return View(roleManager.Roles);
        }

        public ActionResult CreateRole(string name)
        {
            name = name.ToString().ToUpper().Trim();

            IdentityRole role = new IdentityRole(name);

            if(roleManager.RoleExists(name))
                return RedirectToAction("ManageRoles");

            roleManager.Create(role);

            return RedirectToAction("ManageRoles");
        }

        public ActionResult AdicionarRole(IdentityRole model)
        {
            //ViewBag.UserId = id;
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!ModelState.IsValid)
                return View(model);
            else
            {
                //roleManager.CreateAsync(identityRole);

                return RedirectToAction("ManageRoles");
            }
        }

        public ActionResult DeletarRole(string id)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            roleManager.Delete(roleManager.FindById(id));

            return RedirectToAction("ManageRoles");
        }

        public ActionResult ModificarRoles()
        {
            return null;
        }
    }
}