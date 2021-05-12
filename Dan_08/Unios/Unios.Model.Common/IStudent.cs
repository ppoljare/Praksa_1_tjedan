using System;

namespace Unios.Model.Common
{
    public interface IStudent
    {
        Guid StudentID { get; set; }
        Guid FakultetID { get; set; }
        string Ime { get; set; }
        string Prezime { get; set; }
        string Fakultet { get; set; }
        int Godina { get; set; }
        bool Found { get; set; }
    }
}
