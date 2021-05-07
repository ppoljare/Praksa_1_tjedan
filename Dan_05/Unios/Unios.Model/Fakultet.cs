using System;
using System.Collections.Generic;
using Unios.Model.Common;

namespace Unios.Model
{
    public class Fakultet : IFakultet
    {
        public Guid FakultetID { get; set; }
        public string Naziv { get; set; }
        public List<Student> Studenti { get; set; }

        public Fakultet(Guid fakultetID, string naziv)
        {
            FakultetID = fakultetID;
            Naziv = naziv;
            Studenti = new List<Student>();
        }

        public void Add(Student student)
        {
            Studenti.Add(student);
        }
    }
}
