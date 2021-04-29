using System;
using System.Collections.Generic;

namespace Core.Common.Models.Search
{
    public class SearchResult<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        public SearchResult<R> RecreateWithType<R>(Func<List<T>, List<R>> convertItems)
        {
            return new SearchResult<R>
            {
                Page = Page,
                Size = Size,
                TotalItems = TotalItems,
                Items = convertItems(Items)
            };
        }
    }
}
