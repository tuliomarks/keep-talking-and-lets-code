using EverybodyCodes.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverybodyCodes.Infrastructure.Services.Interfaces
{
    public interface ICameraService
    {
        IEnumerable<Camera> GetAll();
        
        Camera GetById(long filter);

        IEnumerable<Camera> GetByDivisor(long divisor);

        IEnumerable<Camera> GetByName(string filter);

    }
}
