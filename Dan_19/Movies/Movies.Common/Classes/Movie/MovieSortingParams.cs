using Movies.Common.Interfaces;

namespace Movies.Common.Classes.Movie
{
    public class MovieSortingParams : ISortingParams
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public bool IsValid()
        {
            SetDefaults();

            switch (SortOrder.ToLower())
            {
                case "asc":
                    break;
                case "desc":
                    break;
                default:
                    return false;
            }

            switch (SortBy.ToLower())
            {
                case "name":
                    SortBy = "Name";
                    break;
                case "genre":
                    SortBy = "Genre";
                    break;
                case "year":
                    SortBy = "YearReleased";
                    break;
                case "yearreleased":
                    SortBy = "YearReleased";
                    break;
                default:
                    return false;
            }

            return true;
        }

        public string GenerateSortingString()
        {
            return " ORDER BY " + SortBy + " " + SortOrder.ToUpper();
        }

        private void SetDefaults()
        {
            if (SortBy == null)
            {
                SortBy = "Name";
            }
            if (SortOrder == null)
            {
                SortOrder = "asc";
            }
        }
    }
}
