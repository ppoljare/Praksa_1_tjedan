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
    }
}
