using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum ListType
    {
        Upcomming,
        Planned,
        Saved
    }

    public class ListOfTransactions
    {
        public Guid AccountId { get; protected set; }
        public ListType type { get; protected set; }
        //public 


    }
}
