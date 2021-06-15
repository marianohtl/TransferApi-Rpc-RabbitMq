using System;
using System.Collections.Generic;
using System.Text;

namespace Transfer.Rpc
{
    public class AccountTransfer
    {
        public string AccountNumber { get; set; }
        public int  Value { get; set; }
        public string Type { get; set; }

    }
}
