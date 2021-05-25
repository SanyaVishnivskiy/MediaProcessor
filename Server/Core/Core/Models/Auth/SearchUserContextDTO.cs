using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Auth
{
    public class SearchUserContextDTO
    {
        private string _search = "";

        [FromQuery]
        public string Search
        {
            get => _search;
            set => _search = value is null
                ? ""
                : value;
        }

        public int PageSize { get; set; } = 10;
    }
}
