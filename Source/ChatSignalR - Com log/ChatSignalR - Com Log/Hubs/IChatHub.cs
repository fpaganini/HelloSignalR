using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Hubs
{
    public interface IChatHub
    {
        void enviaMensagem(string usuario, string mensagem);
        void novoUsuario(string usuario);
    }
}
