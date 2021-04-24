using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Files
{
    public class FileDTO
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
