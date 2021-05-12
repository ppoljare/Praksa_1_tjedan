using System;
using System.Collections.Generic;
using Unios.Model.Common;

namespace Unios.Model
{
    public class Fakultet : IFakultet
    {
        public Guid FakultetID { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public List<IStudent> Studenti { get; set; }
        public bool Found { get; set; }

        public Fakultet(bool found)
        {
            Found = found;
        }
    }
}
