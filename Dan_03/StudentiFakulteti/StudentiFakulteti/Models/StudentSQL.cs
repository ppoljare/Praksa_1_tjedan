using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentiFakulteti.Models
{
    public class StudentSQL
    {
        public string StudentID { get; set; }
        public string FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public StudentSQL(string studentID, string fakultetID, string ime, string prezime)
        {
            StudentID = studentID;
            FakultetID = fakultetID;
            Ime = ime;
            Prezime = prezime;
        }
    }
}