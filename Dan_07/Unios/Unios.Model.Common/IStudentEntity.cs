using System;

namespace Unios.Model.Common
{
    public interface IStudentEntity
    {
        Guid StudentID { get; set; }
        Guid FakultetID { get; set; }
        string Ime { get; set; }
        string Prezime { get; set; }
    }
}
