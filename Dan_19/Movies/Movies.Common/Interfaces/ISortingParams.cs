namespace Movies.Common.Interfaces
{
    public interface ISortingParams
    {
        string SortBy { get; set; }
        string SortOrder { get; set; }

        bool IsValid();
        string GenerateSortingString();
    }
}
