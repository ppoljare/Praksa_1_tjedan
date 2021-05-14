namespace Unios.Common
{
    public interface IStudentSortingParams
    {
        string SortBy { get; set; }
        string SortOrder { get; set; }

        bool IsValid();
    }
}