using System;
using System.Collections.Generic;

namespace Unios.WebApi.Models
{
    public class FakultetAndStudentsViewModel
    {
        public Guid FakultetID { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public List<StudentViewModel> Studenti { get; set; }
    }
}