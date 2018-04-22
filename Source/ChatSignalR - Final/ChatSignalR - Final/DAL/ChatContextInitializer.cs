using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatSignalR.DAL
{
    public class ChatContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ChatContext>
    {
        protected override void Seed(ChatContext context)
        {
        }
     }
}