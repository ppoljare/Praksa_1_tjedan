using System;

namespace Unios.Common
{
    public class StudentSortingParams : IStudentSortingParams
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public bool IsValid()
        {
            if (SortBy == null)
            {
                SortBy = "Prezime";
            }

            if (SortOrder == null)
            {
                SortOrder = "asc";
            }

            switch (SortOrder)
            {
                case "asc":
                    break;
                case "desc":
                    break;
                default:
                    return false;
            }

            switch (SortBy)
            {
                case "Ime":
                    break;
                case "Prezime":
                    break;
                case "Fakultet":
                    SortBy = "Naziv";
                    break;
                case "Godina":
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
