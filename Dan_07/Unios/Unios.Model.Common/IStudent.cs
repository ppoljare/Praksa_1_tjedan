using System;

namespace Unios.Model.Common
{
    public interface IStudent
    {
        Guid StudentID { get; set; }
        string Ime { get; set; }
        string Prezime { get; set; }
    }
}
