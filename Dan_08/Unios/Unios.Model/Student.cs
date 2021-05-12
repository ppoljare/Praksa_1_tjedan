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

        public Student(Guid id, string ime, string prezime, string fakultet, int godina)
        {
            StudentID = id;
            Ime = ime;
            Prezime = prezime;
            Fakultet = fakultet;
            Godina = godina;
            Found = true;
        }

        public Student(bool found)
        {
            Found = found;
        }
    }
}
