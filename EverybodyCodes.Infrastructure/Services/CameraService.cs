using EverybodyCodes.Infrastructure.Domain;
using EverybodyCodes.Infrastructure.Repositories.Interfaces;
using EverybodyCodes.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverybodyCodes.Infrastructure.Services
{
    public class CameraService : ICameraService
    {
        private ICameraRepository _repository;

        public CameraService(ICameraRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<Camera> GetAll()
        {
            return this._repository.GetAll();
        }

        public IEnumerable<Camera> GetByDivisor(long divisor)
        {
            return this._repository.GetAll().Where(x => x.Id % divisor == 0).ToList();
        }

        public Camera GetById(long filter)
        {
            return this._repository.GetById(filter);
        }

        public IEnumerable<Camera> GetByName(string filter)
        {
            return this._repository.GetByName(filter);
        }
    }
}
