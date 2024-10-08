﻿using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Common.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Records.Component
{
    public interface IRecordsComponent
    {
        Task<List<RecordModel>> Get(Pagination pagination);
        Task<SearchResult<RecordModel>> GetWithDependencies(RecordSearchContext context);
        Task<RecordModel> GetById(string id);
        Task Add(RecordModel model);
        Task AddDefault(RecordModel model);
        Task Update(RecordModel model);
        Task Delete(string id);
    }
}
