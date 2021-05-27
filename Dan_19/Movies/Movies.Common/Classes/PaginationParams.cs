using Movies.Common.Interfaces;

namespace Movies.Common.Classes
{
    public class PaginationParams : IPaginationParams
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }

        public bool IsValid()
        {
            SetDefaults();
            return ItemsPerPage > 0 && Page > 0;
        }

        public string GeneratePaginationString(string position)
        {
            string paginationString = "";

            switch (position)
            {
                case "start":
                    if (IsFirstPage())
                    {
                        paginationString += " TOP " + ItemsPerPage;
                    }
                    break;

                case "end":
                    if (IsFirstPage())
                    {
                        break;
                    }
                    paginationString += " OFFSET " + PageStart().ToString() + " ROWS";

                    if (!IsLastPage())
                    {
                        paginationString += " FETCH NEXT " + ItemsPerPage.ToString() + " ROWS ONLY";
                    }
                    break;

                default:
                    break;
            }

            return paginationString;
        }

        private void SetDefaults()
        {
            if (ItemsPerPage == 0)
            {
                ItemsPerPage = 10;
            }
            if (Page == 0)
            {
                Page = 1;
            }
        }

        private bool IsFirstPage()
        {
            return Page == 1;
        }

        private bool IsLastPage()
        {
            return ItemsPerPage * Page >= TotalItems;
        }

        private int PageStart()
        {
            return ItemsPerPage * (Page - 1);
        }
    }
}
