using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentiFakulteti.Models
{
    public class Fakultet
    {
        public string FakultetID { get; set; }
        public string Naziv { get; set; }
        public List<Student> Studenti { get; set; }

        public Fakultet()
        {
            Studenti = new List<Student>();
        }

        public void Append(Student student)
        {
            Studenti.Add(student);
        }
    }
}