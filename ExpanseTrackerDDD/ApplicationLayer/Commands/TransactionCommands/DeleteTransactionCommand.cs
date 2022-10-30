using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands
{
    public class DeleteTransactionCommand
    {
        public Guid FirstTransactionId { get; set; }
        [AllowNull]
        public Guid SecondTransactionId { get; set; }
    }
}
