using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatSignalR.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public void EnviaMensagem(string usuario, string mensagem)
        {
            //Enviando para todos os usuarios conectados, a mesma mensagem recebida do cliente
            Clients.All.enviaMensagem(usuario,mensagem);
        }

        public void novoUsuario(string usuario) {
            Clients.Others.novoUsuario(usuario);
        }
    }
}