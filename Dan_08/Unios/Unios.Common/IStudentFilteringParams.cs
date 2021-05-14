using System;

namespace Unios.Common
{
    public interface IStudentFilteringParams
    {
        string Ime { get; set; }
        string Prezime { get; set; }
        string Fakultet { get; set; }
        string Godina { get; set; }
    }
}
