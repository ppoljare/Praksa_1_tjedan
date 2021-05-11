using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class Student : IStudent
    {
        public Guid StudentID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Fakultet { get; set; }

        public Student(Guid id, string ime, string prezime, string fakultet)
        {
            StudentID = id;
            Ime = ime;
            Prezime = prezime;
            Fakultet = fakultet;
        }
    }
}
