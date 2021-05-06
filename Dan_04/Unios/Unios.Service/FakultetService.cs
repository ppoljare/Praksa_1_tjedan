using System.Collections.Generic;
using Unios.Model;
using Unios.Repository;

namespace Unios.Service
{
    public class FakultetService
    {
        protected FakultetRepository Repository { get; private set; }

        public FakultetService()
        {
            Repository = new FakultetRepository();
        }

        public int Add(FakultetSQL fakultet)
        {
            return Repository.Add(fakultet);
        }

        public int Delete(int id)
        {
            return Repository.Delete(id);
        }

        public List<FakultetSQL> Get()
        {
            return Repository.GetAll();
        }

        public Fakultet Get(int id)
        {
            return Repository.Get(id);
        }

        public int Update(int id, FakultetSQL fakultet)
        {
            return Repository.Update(id, fakultet);
        }
    }
}
