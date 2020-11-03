using EverybodyCodes.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverybodyCodes.Infrastructure.Extension.Interfaces
{
    public interface IDataContext
    {
        IEnumerable<Camera> Cameras { get; }

    }
}
