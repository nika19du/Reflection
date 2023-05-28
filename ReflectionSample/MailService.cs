using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public class MailService
    {
        public void SendMail(string address,string subject)
        {
            Console.WriteLine($"Sending a warning mail to {address} with subject {subject}.");
        }
    }
}
