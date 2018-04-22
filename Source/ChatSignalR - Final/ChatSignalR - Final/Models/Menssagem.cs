using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatSignalR.Models
{
    public class LogMenssagem
    {
        [Key]
        public int ID { get; set; }
        public string ConnectionId { get; set; }
        public string NickName { get; set; }
        public string Menssagem { get; set; }
        public DateTime data { get; set; }
        public bool privado { get; set; }
        public string destino { get; set; }
    }
}