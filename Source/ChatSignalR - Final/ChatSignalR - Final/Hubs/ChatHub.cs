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

            bool privado = false;
            string destino = "";
            //Enviando para todos os usuarios conectados, a mesma mensagem recebida do cliente
            if (mensagem.StartsWith("/"))
            {
                privado = true;
                var comando = mensagem.Split(' ')[0];
                mensagem = mensagem.Replace(comando,"").Trim();
                comando = comando.Replace("/", "").Trim();
                var usuario_solicitado = (from d in db.LogMenssagems where d.NickName.ToUpper() == comando.ToUpper() orderby d.ID select d).ToList().DefaultIfEmpty(null).LastOrDefault();
                if (usuario_solicitado != null)
                {
                    destino = usuario_solicitado.NickName;
                    Clients.Caller.enviaMensagem(usuario + " (privado)",mensagem);
                    Clients.Client(usuario_solicitado.ConnectionId).enviaMensagem(usuario + " (privado)", mensagem);
                }
                
             }
            else
            {
                Clients.All.enviaMensagem(usuario, mensagem);
            }
            
            Task task = Task.Run(() => SaveDataTask(usuario, mensagem, this.Context, privado, destino));
        }

        private void SaveDataTask(string usuario, string mensagem, HubCallerContext context, bool privado, string destino)
        {
            var logMensagem = new Models.LogMenssagem();
            logMensagem.ConnectionId = this.Context.ConnectionId;
            logMensagem.data = DateTime.Now;
            logMensagem.Menssagem = mensagem;
            logMensagem.NickName = usuario;
            logMensagem.privado = privado;
            logMensagem.destino = destino;

            db.LogMenssagems.Add(logMensagem);
            db.SaveChanges();
            
        }

        public void novoUsuario(string usuario) {
            Clients.Others.novoUsuario(usuario);
        }

        public void usuarioDesconectado(string usuario)
        {
            Clients.Others.usuarioDesconectado(usuario);
        }

        public void usuarioReconectado(string usuario)
        {
            Clients.Others.usuarioReconectado(usuario);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var usuario = (from d in db.LogMenssagems where d.ConnectionId == this.Context.ConnectionId select d).ToList().DefaultIfEmpty(null).LastOrDefault();
            if(usuario != null)
                usuarioDesconectado(usuario.NickName); 
            return base.OnDisconnected(stopCalled);
        }


        public override Task OnReconnected()
        {
            var usuario = (from d in db.LogMenssagems where d.ConnectionId == this.Context.ConnectionId select d).ToList().DefaultIfEmpty(null).LastOrDefault();
            if (usuario != null)
                usuarioReconectado(usuario.NickName);
            return base.OnReconnected();
        }

    }
}