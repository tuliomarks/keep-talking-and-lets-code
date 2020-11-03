using System.Collections.Generic;

namespace EverybodyCodes.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(long filter);
    }
}