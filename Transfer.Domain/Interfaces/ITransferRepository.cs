using System;
using System.Collections.Generic;
using System.Text;
using Transfer.Domain.Entities;

namespace Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> Get();
        TransferStatus GetById(Guid transactionId);

        TransferStatus SaveStatus(TransferStatus transfer);

        TransferLog SaveTransferLog(TransferLog transferLog);





    }
}
