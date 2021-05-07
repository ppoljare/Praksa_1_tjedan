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

        public async Task<int> Add(FakultetInput fakultet)
        {
            return await Repository.Add(fakultet);
        }

        public async Task<int> Delete(Guid id)
        {
            if (await Repository.Find(id) == false)
            {
                return -204;
            }
            return await Repository.Delete(id);
        }

        public async Task<List<FakultetEntity>> Get()
        {
            return await Repository.GetAll();
        }

        public async Task<Fakultet> Get(Guid id)
        {
            return await Repository.Get(id);
        }

        public async Task<int> Update(Guid id, FakultetInput fakultet)
        {
            if (await Repository.Find(id) == false)
            {
                return -404;
            }
            return await Repository.Update(id, fakultet);
        }
    }
}
