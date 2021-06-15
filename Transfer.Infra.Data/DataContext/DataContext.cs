using Transfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Transfer.Infra.Data
{
    public class DataContext : DbContext, IDisposable
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        {
        }
        public virtual DbSet<TransferStatus> TransferStatus { get; set; }
        public virtual DbSet<TransferLog> TransferLog { get; set; }

    }
}
