using System;

namespace Unios.Common
{
    public class PaginationParams : IPaginationParams
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        private int TotalItems { get; set; }

        public void SetTotalItems(int totalItems)
        {
            TotalItems = totalItems;
        }

        public bool IsValid()
        {
            if (ItemsPerPage == 0)
            {
                ItemsPerPage = 10;
            }

            if (Page == 0)
            {
                Page = 1;
            }

            return ItemsPerPage > 0 && Page > 0;
        }

        public int PageStart()
        {
            return ItemsPerPage * (Page - 1);
        }

        public bool IsFirstPage()
        {
            return Page == 1;
        }

        public bool IsLastPage()
        {
            return ItemsPerPage * Page >= TotalItems;
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
    }
}
