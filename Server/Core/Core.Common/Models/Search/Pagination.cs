namespace Core.Common.Models
{
    public class Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 0;
        public string SortBy { get; set; } = "";
        /// <summary>
        /// Can have values 'asc' or 'desc'
        /// </summary>
        public string SortOrder { get; set; } = "asc";

        public bool IsDescendingSortOrder => SortOrder == "desc";
    }
}
