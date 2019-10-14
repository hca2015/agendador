using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tcc.Apoio;
using Tcc.Entity;
using Tcc.Models;

namespace Tcc.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        [NotMapped]
        [JsonIgnore]
        protected static ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        [NotMapped]
        [JsonIgnore]
        protected static string membershipId
        {
            get
            {                
                return System.Web.HttpContext.Current.User.Identity.GetUserId();
            }
        }

        [NotMapped]
        [JsonIgnore]
        protected User usuario
        {
            get
            {                
                return (new UsersRepository().GetUserId(membershipId));
            }
        }

        [NotMapped]
        [JsonIgnore]
        private GenClass _ContextoExecucao;
        [NotMapped]
        [JsonIgnore]
        public GenClass aContextoExecucao
        {
            get
            {
                if (_ContextoExecucao == null)
                    _ContextoExecucao = new GenClass();

                return _ContextoExecucao;
            }
            set { _ContextoExecucao = value; }
        }        
    }
}