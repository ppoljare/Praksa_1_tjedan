using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentiFakulteti.Models
{
    public class FakultetSQL
    {
        public string FakultetID { get; set; }
        public string Naziv { get; set; }

        public FakultetSQL(string fakultetID, string naziv)
        {
            FakultetID = fakultetID;
            Naziv = naziv;
        }
    }
}