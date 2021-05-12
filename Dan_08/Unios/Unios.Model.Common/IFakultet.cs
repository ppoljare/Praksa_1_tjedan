using System;
using System.Collections.Generic;

namespace Unios.Model.Common
{
    public interface IFakultet
    {
        Guid FakultetID { get; set; }
        string Naziv { get; set; }
        string Vrsta { get; set; }
        List<IStudent> Studenti { get; set; }
        bool Found { get; set; }
    }
}
