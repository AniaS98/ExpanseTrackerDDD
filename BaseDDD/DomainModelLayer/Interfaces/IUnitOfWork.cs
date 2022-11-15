using BaseDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RejectChanges();
    }
}
