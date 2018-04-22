using ChatSignalR.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatSignalR.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Historico()
        {
            ChatContext db = new ChatContext();
                var model = (from d in db.LogMenssagems where d.data >= DateTime.Today && d.privado == false orderby d.data select d).ToList();
            return View(model);
        }
    }
}