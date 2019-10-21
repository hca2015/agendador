using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tcc.Entity;

namespace Tcc.Apoio
{
    public class GenClass
    {
        public GenClass()
        {

        }
        [NotMapped]
        [JsonIgnore]
        protected string getUserId
        {
            get
            {
                //Microsoft.Owin.IOwinContext a = System.Web.HttpContext.Current.GetOwinContext();
                //ApplicationUserManager b = a.GetUserManager<ApplicationUserManager>();
                //ApplicationUser c = b.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        [NotMapped]
        [JsonIgnore]
        protected User currentUser
        {
            get
            {
                return new UsersRepository().GetUserId(HttpContext.Current.User.Identity.GetUserId());
            }
        }

        [NotMapped]
        public virtual List<Message> Messages
        {
            get
            {
                if (_Messages == null)
                    _Messages = new List<Message>();

                return _Messages;
            }
            set
            {
                _Messages = value;
            }
        }
        private List<Message> _Messages;

        public void addMessage(string prMessage, Message.kdType? type = null, string prCurrentMethod = null)
        {
            Messages.Add(new Message(prMessage, type, prCurrentMethod));
        }

        public void add(List<Message> prMessageList)
        {
            Messages.AddRange(prMessageList);
        }

        public virtual bool withoutError()
        {
            if (Messages.Count > 0)
            {
                foreach (Message item in Messages)
                {
                    if (item.messageType == Message.kdType.Error)
                        return false;
                }
            }
            return true;
        }
        public virtual bool withError()
        {
            if (Messages.Count > 0)
            {
                foreach (Message item in Messages)
                {
                    if (item.messageType == Message.kdType.Error)
                        return true;
                }
            }
            return false;
        }


        public virtual bool withoutError(Message prMessage)
        {
            Messages.Add(prMessage);

            return withoutError();
        }

        public virtual bool withErrororWarn()
        {
            if (Messages.Count > 0)
            {
                foreach (Message item in Messages)
                {
                    if (item.messageType == Message.kdType.Error || item.messageType == Message.kdType.Info)
                        return true;
                }
            }
            return false;
        }

        public virtual void clearList()
        {
            Messages = new List<Message>();
        }

        public virtual void clearErro()
        {
            Messages = Messages.Where(x => x.messageType != Message.kdType.Error).ToList();
        }

        public virtual void addErro(string prErro)
        {
            Messages.Add(new Message(prErro, Message.kdType.Error));
        }

        public virtual Message newError(string prErro)
        {
            return new Message(prErro, Message.kdType.Error);
        }
    }
}