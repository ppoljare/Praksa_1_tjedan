namespace Unios.Common
{
    public interface IFakultetSortingParams
    {
        string SortBy { get; set; }
        string SortOrder { get; set; }

        bool IsValid();
    }
}