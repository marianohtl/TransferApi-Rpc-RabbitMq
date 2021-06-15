using System;
using System.Collections.Generic;
using System.Text;

namespace Transfer.Rpc
{
    public class ClientTransfer
    {
        public Guid Id { get; set; }
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public int Value { get; set; }
    }
}
