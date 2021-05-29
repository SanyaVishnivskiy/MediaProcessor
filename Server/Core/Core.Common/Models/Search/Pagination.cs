using System;
using System.Linq.Expressions;

namespace Core.Common.Models
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 2;
        public string SortBy { get; set; } = "";
        /// <summary>
        /// Can have values 'asc' or 'desc'
        /// </summary>
        public string SortOrder { get; set; } = "asc";

        public bool IsDescendingSortOrder => SortOrder == "desc";
    }

    public class FuncPaginationWithKey<T, TKey>
    {
        public FuncPaginationWithKey(Pagination pagination, Expression<Func<T, TKey>> keySelector)
        {
            Page = pagination.Page;
            Size = pagination.Size;
            SortBy = keySelector;
            SortOrder = pagination.SortOrder;
        }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 2;
        public Expression<Func<T, TKey>> SortBy { get; set; }
        /// <summary>
        /// Can have values 'asc' or 'desc'
        /// </summary>
        public string SortOrder { get; set; } = "asc";

        public bool IsDescendingSortOrder => SortOrder == "desc";
    }

    public class FuncPagination<T> : FuncPaginationWithKey<T, object>
    {
        public FuncPagination(Pagination pagination, Expression<Func<T, object>> keySelector) : base(pagination, keySelector)
        {
        }
    }
}
