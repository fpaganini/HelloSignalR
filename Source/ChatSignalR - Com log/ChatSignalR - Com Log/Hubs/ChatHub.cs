using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ChatSignalR.DAL;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace ChatSignalR.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        ChatContext db = new ChatContext();
        public void EnviaMensagem(string usuario, string mensagem)
        {
            
            //Enviando para todos os usuarios conectados, a mesma mensagem recebida do cliente
            Clients.All.enviaMensagem(usuario,mensagem);
            Task task = Task.Run(() => SaveDataTask(usuario, mensagem, this.Context));
        }

        private void SaveDataTask(string usuario, string mensagem, HubCallerContext context)
        {
            var logMensagem = new Models.LogMenssagem();
            logMensagem.ConnectionId = this.Context.ConnectionId;
            logMensagem.data = DateTime.Now;
            logMensagem.Menssagem = mensagem;
            logMensagem.NickName = usuario;

            db.LogMenssagems.Add(logMensagem);
            db.SaveChanges();
            
        }

        public void novoUsuario(string usuario) {
            Clients.Others.novoUsuario(usuario);
        }
    }
}