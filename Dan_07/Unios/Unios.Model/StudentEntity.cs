using System;
using Unios.Model.Common;

namespace Unios.Model
{
    public class StudentEntity : IStudentEntity
    {
        public Guid StudentID { get; set; }
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public StudentEntity(Guid studentID, string ime, string prezime, Guid fakultetID)
        {
            StudentID = studentID;
            FakultetID = fakultetID;
            Ime = ime;
            Prezime = prezime;
        }

        public StudentEntity(string ime, string prezime, Guid fakultetID)
        {
            StudentID = Guid.NewGuid();
            FakultetID = fakultetID;
            Ime = ime;
            Prezime = prezime;
        }
    }
}
