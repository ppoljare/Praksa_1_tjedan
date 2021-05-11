using System;

namespace Unios.Model.Common
{
    public interface IFakultetEntity
    {
        Guid FakultetID { get; set; }
        string Naziv { get; set; }
    }
}
