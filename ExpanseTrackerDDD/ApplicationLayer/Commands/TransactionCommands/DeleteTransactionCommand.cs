using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands
{
    public class DeleteTransactionCommand
    {
        public Guid FirstTransactionId;
        [AllowNull]
        public Guid SecondTransactionId;
    }
}
