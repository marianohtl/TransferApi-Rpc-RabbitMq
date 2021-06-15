using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transfer.Domain
{
    public class RabbitMqConfiguration { 
            public string Hostname { get; set; }
            public string SendQueue { get; set; }
            public string ReplyQueue { get; set; }

    }
}
