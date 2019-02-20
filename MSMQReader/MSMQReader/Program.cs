using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Configuration;

namespace MSMQReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["MsmqPath"];
            var lstMessage = ReadQue(path);
        }

        private static List<string> ReadQue(string path)
        {
            MessageQueue messageQueue = null;
            var listMessage = new List<string>();
            try
            {
                using (messageQueue = new MessageQueue(path))
                {
                    var messages = messageQueue.GetAllMessages();
                    foreach (var message in messages)
                    {
                        message.Formatter = new XmlMessageFormatter(new Type[] { typeof(Model) });
                        var msg = (Model)message.Body;
                    }
                }
                return listMessage;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                messageQueue.Dispose();
            }
        }
    }
}
