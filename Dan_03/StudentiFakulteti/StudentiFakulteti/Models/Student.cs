using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentiFakulteti.Models
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