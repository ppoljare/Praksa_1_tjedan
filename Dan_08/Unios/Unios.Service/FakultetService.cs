using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Common;
using Unios.Model.Common;
using Unios.Repository.Common;
using Unios.Service.Common;

namespace Unios.Service
{
    public class FakultetService : IFakultetService
    {
        protected IFakultetRepository Repository { get; private set; }

        public FakultetService(IFakultetRepository repository)
        {
            Repository = repository;
        }

        public async Task<IFakultet> AddAsync(IFakultet fakultet)
        {
            if (await Repository.GetAsync(fakultet.FakultetID) == null)
            {
                return await Repository.AddAsync(fakultet);
            }
            return null;
        }

        public async Task<int> CountAsync(FakultetFilteringParams filteringParams)
        {
            return await Repository.CountAsync(filteringParams);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (await Repository.GetAsync(id) == null)
            {
                return -204;
            }
            return await Repository.DeleteAsync(id);
        }

        public async Task<List<IFakultet>> FindAsync(
            FakultetFilteringParams filteringParams,
            FakultetSortingParams sortingParams,
            PaginationParams paginationParams
        )
        {
            return await Repository.FindAsync(filteringParams, sortingParams, paginationParams);
        }

        public async Task<IFakultet> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<IFakultet> UpdateAsync(IFakultet fakultet)
        {
            if (await Repository.GetAsync(fakultet.FakultetID) == null)
            {
                fakultet.Found = false;
                return fakultet;
            }

            return await Repository.UpdateAsync(fakultet);
        }
    }
}
