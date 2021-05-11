using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class FakultetEntity : IFakultetEntity
    {
        public Guid FakultetID { get; set; }
        public string Naziv { get; set; }

        public FakultetEntity(Guid fakultetID, string naziv)
        {
            FakultetID = fakultetID;
            Naziv = naziv;
        }

        public FakultetEntity(string naziv)
        {
            FakultetID = Guid.NewGuid();
            Naziv = naziv;
        }
    }
}
