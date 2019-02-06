using System.Collections.Generic;

namespace Tcc.Entity
{
    public class Message
    {
        public Message()
        {

        }

        public Message(string prMessage, string prCurrentMethod)
        {
            message = prMessage;
            currentMethod = prCurrentMethod;
        }

        public string message { get; set; }

        public string currentMethod { get; set; }

        public List<Message> messagelist { get; set; }

        public void addMessage(string prMessage, string prCurrentMethod)
        {
            messagelist.Add(new Message(prMessage, prCurrentMethod));
        }
    }
}