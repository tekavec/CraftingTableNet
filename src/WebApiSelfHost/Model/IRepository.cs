using System;

namespace WebApiSelfHost.Model
{
    public interface IRepository<T>
    {
        void Create(Guid id, T item);

        // other members
    }
}