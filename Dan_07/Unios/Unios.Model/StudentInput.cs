using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class StudentInput : IStudentInput
    {
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
