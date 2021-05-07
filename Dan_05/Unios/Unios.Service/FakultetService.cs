using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
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

        public async Task<int> AddAsync(FakultetInput fakultet)
        {
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

        public async Task<List<FakultetEntity>> GetAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<Fakultet> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<int> UpdateAsync(Guid id, FakultetInput fakultet)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return -404;
            }
            return await Repository.UpdateAsync(id, fakultet);
        }
    }
}
