using System;
using System.Collections.Generic;
using System.Text;
using Transfer.Domain.Entities;
using Transfer.Domain.ViewModel;

namespace Transfer.Domain.Interfaces
{
    public interface ITransferService
    {
        IList<TransferLog> GetLogs();
        TransferStatus GetByTransactionId(Guid transactionId);

        ClientTransferViewModel Post(ClientTransfer transfer);


    }
}
