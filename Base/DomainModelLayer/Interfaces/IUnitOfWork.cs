using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DomainModelLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RejectChanges();

    }
}
