using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RejectChanges();

    }
}
