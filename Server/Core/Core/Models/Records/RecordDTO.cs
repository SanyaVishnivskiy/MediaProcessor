﻿using Core.Models.Records;

namespace Core.Models
{
    public class RecordDTO
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public RecordFileDTO File { get; set; }
    }
}