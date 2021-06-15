using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer.Domain.Entities;
using Transfer.Domain.Interfaces;
using Transfer.Domain.ViewModel;

namespace Transfer.Domain.Services
{
    public class TransferService : ITransferService
    {

        private readonly ITransferRepository _transferRepository;
        private readonly IMessaging _client;
        private TransferLog _log;
        private TransferStatus _status;
        private ClientTransferViewModel _clientTransferViewModel;
        public TransferService(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }


        public IList<TransferLog> GetLogs()
        {
            try
            {
                return _transferRepository.Get().ToList();
            }
            catch
            {
                return new List<TransferLog>();
            }
        }



        public TransferStatus GetByTransactionId(Guid transactionId)
        {
            try
            {
                return _transferRepository.GetById(transactionId);
            }
            catch
            {
                return null;
            }
        }


        public ClientTransferViewModel Post(ClientTransfer transfer)
        {
            try
            {   
                
                if(transfer != null)
                {

                    _client.SendTransfer(transfer);
                    
                    _log = new TransferLog() { LogDate = DateTime.Now, AccountDestination = transfer.AccountDestination, AccountOrigin = transfer.AccountOrigin, Value = transfer.Value };
                    _transferRepository.SaveTransferLog(_log);

                    _status = new TransferStatus { TransactionId = new Guid(), Status = "In Queue" };
                    _clientTransferViewModel.TransactionId = _status.TransactionId;
                    _transferRepository.SaveStatus(_status);
                }

                return _clientTransferViewModel;
            }
            catch
            {
                return null;
            }
        }

      




    }
}
