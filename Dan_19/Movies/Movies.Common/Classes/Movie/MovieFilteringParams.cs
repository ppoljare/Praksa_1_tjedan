using Movies.Common.Interfaces;

namespace Movies.Common.Classes.Movie
{
    public class MovieFilteringParams : IFilteringParams
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int YearLowerBound { get; set; }
        public int YearUpperBound { get; set; }

        public string GenerateFilteringString()
        {
            string filterString = "";
            int counter = 0;

            if (Name != null)
            {
                counter++;
                filterString = " WHERE ";
                filterString += "LOWER(Name) LIKE '%" + Name.ToLower() + "%'";
            }

            if (Genre != null)
            {
                filterString += NextKeyword(counter);
                counter++;
                filterString += "LOWER(Genre) LIKE '%" + Genre.ToLower() + "%'";
            }

            if (YearLowerBound != 0)
            {
                filterString += NextKeyword(counter);
                counter++;
                filterString += "YearReleased >= " + YearLowerBound.ToString();
            }

            if (YearUpperBound != 0)
            {
                filterString += NextKeyword(counter);
                filterString += "YearReleased <= " + YearUpperBound.ToString();
            }

            return filterString;
        }

        private string NextKeyword(int counter)
        {
            if (counter == 0)
            {
                return " WHERE ";
            }
            else
            {
                return " AND ";
            }
        }
    }
}
