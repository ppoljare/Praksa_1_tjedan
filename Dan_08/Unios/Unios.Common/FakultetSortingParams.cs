using System;

namespace Unios.Common
{
    public class FakultetSortingParams : IFakultetSortingParams
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public bool IsValid()
        {
            if (SortBy == null)
            {
                SortBy = "Naziv";
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
                case "Naziv":
                    break;
                case "Vrsta":
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
