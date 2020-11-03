using EverybodyCodes.Infrastructure.Domain;
using EverybodyCodes.Infrastructure.Extension.Interfaces;
using EverybodyCodes.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverybodyCodes.Infrastructure.Repositories
{
    public class CameraRepository : ICameraRepository
    {
        private IDataContext _context;

        public CameraRepository(IDataContext context)
        {
            this._context = context;
        }

        public IEnumerable<Camera> GetAll()
        {
            // desirable to clone this list
            return _context.Cameras;
        }

        public Camera GetById(long filter)
        {
            return _context.Cameras.FirstOrDefault(x => x.Id == filter);
        }

        public IEnumerable<Camera> GetByName(string filter)
        {
            return _context.Cameras.Where(x => x.Name.Contains(filter)).ToList();
        }
    }
}
