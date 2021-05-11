using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository;
using Unios.Service.Common;

namespace Unios.Service
{
    public class FakultetService : IFakultetService
    {
        protected FakultetRepository Repository { get; private set; }

        public FakultetService()
        {
            Repository = new FakultetRepository();
        }

        public async Task<IFakultetEntity> AddAsync(FakultetInput input)
        {
            IFakultetEntity fakultet = new FakultetEntity(input.Naziv);
            return await Repository.AddAsync(fakultet);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return -204;
            }
            return await Repository.DeleteAsync(id);
        }

        public async Task<List<IFakultetEntity>> GetAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<IFakultet> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<IFakultetEntity> UpdateAsync(Guid id, FakultetInput input)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return new FakultetEntity(id, null);
            }

            IFakultetEntity fakultet = new FakultetEntity(id, input.Naziv);
            return await Repository.UpdateAsync(fakultet);
        }
    }
}
