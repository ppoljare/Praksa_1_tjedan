using System;

namespace Unios.Model.Common
{
    public interface IStudentInput
    {
        Guid FakultetID { get; set; }
        string Ime { get; set; }
        string Prezime { get; set; }
    }
}
