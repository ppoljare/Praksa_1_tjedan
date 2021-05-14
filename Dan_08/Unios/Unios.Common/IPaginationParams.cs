namespace Unios.Common
{
    public interface IPaginationParams
    {
        int ItemsPerPage { get; set; }
        int Page { get; set; }

        void SetTotalItems(int totalItems);
        bool IsValid();
        bool IsFirstPage();
        bool IsLastPage();
        int PageStart();
        string GeneratePaginationString(string position);
    }
}