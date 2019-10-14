using System.Collections.Generic;
using System.Linq;

namespace Tcc
{
    public static class MessageList
    {
        public static List<Message> messagelist { get { return new List<Message>(); } set { messagelist = value; } }

        public static void addMessage(string prMessage, Message.kdType? type = null, string prCurrentMethod = null)
        {
            messagelist.Add(new Message(prMessage, type, prCurrentMethod));
        }

        public static bool withoutError()
        {
            if (messagelist.Count > 0)
            {
                foreach (Message item in messagelist)
                {
                    if (item.messageType == Message.kdType.Error)
                        return false;
                }
            }
            return true;
        }
        public static bool withError()
        {
            if (messagelist.Count > 0)
            {
                foreach (Message item in messagelist)
                {
                    if (item.messageType == Message.kdType.Error)
                        return true;
                }
            }
            return false;
        }

        public static void clearList()
        {
            messagelist = new List<Message>();
        }

        public static void clearErro()
        {
            messagelist = messagelist.Where(x => x.messageType != Message.kdType.Error).ToList();
        }
    }
}
