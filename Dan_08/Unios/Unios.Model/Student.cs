using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class Student : IStudent
    {
        public Guid StudentID { get; set; }
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Fakultet { get; set; }
        public int Godina { get; set; }
        public bool Found { get; set; }
    }
}
