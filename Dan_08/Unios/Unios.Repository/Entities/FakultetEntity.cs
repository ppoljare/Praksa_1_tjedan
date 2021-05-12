using System;

namespace Unios.Model
{
    public class FakultetEntity
    {
        public Guid FakultetID { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }

        public FakultetEntity(Guid fakultetID, string naziv, string vrsta)
        {
            FakultetID = fakultetID;
            Naziv = naziv;
            Vrsta = vrsta;
        }
    }
}
