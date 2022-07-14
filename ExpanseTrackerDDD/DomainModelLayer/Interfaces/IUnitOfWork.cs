using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<> - wszystkei repozytoria, poczytać

        void Commit();
        void RejectChanges();

    }
}
