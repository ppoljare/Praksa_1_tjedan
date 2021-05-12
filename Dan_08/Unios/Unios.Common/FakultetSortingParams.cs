using System;

namespace Unios.Common
{
    public class FakultetSortingParams
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public bool IsValid()
        {
            if (IsNull())
            {
                return true;
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

        public bool IsNull()
        {
            return SortBy == null && SortOrder == null;
        }
    }
}
