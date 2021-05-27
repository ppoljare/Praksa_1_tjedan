namespace Movies.Common.Interfaces
{
    public interface IPaginationParams
    {
        int ItemsPerPage { get; set; }
        int Page { get; set; }
        int TotalItems { get; set; }

        bool IsValid();
        string GeneratePaginationString(string position);
    }
}
