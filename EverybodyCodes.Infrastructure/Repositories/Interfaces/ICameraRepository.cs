using EverybodyCodes.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverybodyCodes.Infrastructure.Repositories.Interfaces
{
    public interface ICameraRepository: IBaseRepository<Camera>
    {
        IEnumerable<Camera> GetByName(string filter);

    }
}
