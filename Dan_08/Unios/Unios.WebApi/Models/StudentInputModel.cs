using System;

namespace Unios.WebApi.Models
{
    public class StudentInputModel
    {
        public Guid FakultetID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
    }
}