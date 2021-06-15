using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer.Domain.Entities;
using Transfer.Domain.Interfaces;

namespace Transfer.Infra.Data.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DataContext _context;

        public TransferRepository(DataContext context)
        {
            this._context = context;
        }

 
        public IEnumerable<TransferLog> Get()
        {
            return _context.TransferLog;
        }


        public TransferStatus GetById(Guid id)
        {
            return _context.TransferStatus.Where(x => x.TransactionId == id).FirstOrDefault();
        }

        public TransferStatus SaveStatus(TransferStatus transfer)
        {
            var state = transfer.TransactionId == null ? EntityState.Added : EntityState.Modified;
            _context.Entry(transfer).State = state;
            _context.Add(transfer);
            _context.SaveChanges();
            return transfer;
        }

        public TransferLog SaveTransferLog(TransferLog transferLog)
        {
            var state = transferLog.Id == null ? EntityState.Added : EntityState.Modified;
            _context.Entry(transferLog).State = state;
            _context.Add(transferLog);
            _context.SaveChanges();
            return transferLog;
        }

    }
}
