using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HelloSignal
{
    public class HelloSignalHub : Hub
    {
        public void hello(string mensagem)
        {
            Clients.All.hello("Mensagem enviada do servidor");
        }
    }
}