using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transfer.Domain.Entities
{
    public class TransferStatus
    {
        [Key]
        public Guid TransactionId { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "This data is required.")]
        public string Status { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "This data is required.")]
        public string Message { get; set; }

    }
}
