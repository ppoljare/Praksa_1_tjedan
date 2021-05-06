using System;

namespace Unios.Model
{
    public class Student
    {
        public string StudentID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Fakultet { get; set; }

        public Student(string id, string ime, string prezime, string fakultet)
        {
            StudentID = id;
            Ime = ime;
            Prezime = prezime;
            Fakultet = fakultet;
        }
    }
}
