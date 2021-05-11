using System;

namespace Unios.Model.Common
{
    public interface IFakultet
    {
        Guid FakultetID { get; set; }
        string Naziv { get; set; }
    }
}
