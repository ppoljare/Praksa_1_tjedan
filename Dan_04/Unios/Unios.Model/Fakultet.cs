using System.Collections.Generic;

namespace Unios.Model
{
    public class Fakultet
    {
        public string FakultetID { get; set; }
        public string Naziv { get; set; }
        public List<Student> Studenti { get; set; }

        public Fakultet(string fakultetID, string naziv)
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
