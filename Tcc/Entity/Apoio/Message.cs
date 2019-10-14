using System.Collections.Generic;
using System.ComponentModel;

namespace Tcc
{
    public class Message
    {
        public enum kdType
        {
            [Description("Sucesso")]
            Success,
            [Description("Erro")]
            Error,
            [Description("Info")]
            Info
        }
        public Message()
        {

        }
        public Message(string prMessage, kdType? type = null, string prCurrentMethod = null)
        {
            message = prMessage;
            messageType = type;
            currentMethod = prCurrentMethod;
        }

        public string message { get; set; }

        public string currentMethod { get; set; }
        public kdType? messageType { get; set; }

        public override string ToString()
        {
            return message;
        }
    }
        
}