using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ChatSignalR.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("ChatContext")
        {

        }

        public DbSet<Models.LogMenssagem> LogMenssagems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}