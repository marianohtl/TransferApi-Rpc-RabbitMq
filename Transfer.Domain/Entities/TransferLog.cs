using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transfer.Domain.Entities
{
    public class TransferLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This data is required.")]
        public string AccountOrigin { get; set; }

        [Required(ErrorMessage = "This data is required.")]
        public string AccountDestination { get; set; }

        [Required(ErrorMessage = "This data is required.")]
        public int Value { get; set; }

        [Required(ErrorMessage = "This data is required.")]
        public DateTime LogDate { get; set; }
    }
}
