using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class StudentEntity : IStudent
    {
        public Guid StudentID { get; set; }
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
