using System;

namespace Unios.Repository.Entities
{
    public class StudentEntity
    {
        public Guid StudentID { get; set; }
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
        public string Fakultet { get; set; }

        public StudentEntity() { }

        public StudentEntity(Guid id, string ime, string prezime, string fakultet, int godina)
        {
            StudentID = id;
            Ime = ime;
            Prezime = prezime;
            Fakultet = fakultet;
            Godina = godina;
        }
    }
}
