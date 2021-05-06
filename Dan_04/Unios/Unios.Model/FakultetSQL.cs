using System;

namespace Unios.Model
{
    public class FakultetSQL
    {
        public string FakultetID { get; set; }
        public string Naziv { get; set; }

        public FakultetSQL(string fakultetID, string naziv)
        {
            FakultetID = fakultetID;
            Naziv = naziv;
        }
    }
}
