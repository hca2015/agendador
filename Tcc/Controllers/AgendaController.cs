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
    public class AgendaController : AppController
    {
        public ActionResult Agenda()
        {
            return View();
        }
          

    }
}