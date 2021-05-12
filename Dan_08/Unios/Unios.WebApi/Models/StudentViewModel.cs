using System;

namespace Unios.WebApi.Models
{
    public class StudentViewModel
    {
        public Guid StudentID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Fakultet { get; set; }
        public int Godina { get; set; }
    }
}