using System;
using System.Collections.Generic;
using System.Text;
using Transfer.Domain.Entities;

namespace Transfer.Domain.Interfaces
{
    public interface IMessaging
    {
        void SendTransfer(ClientTransfer transfer);

    }
}
