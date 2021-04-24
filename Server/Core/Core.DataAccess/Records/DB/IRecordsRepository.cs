﻿using Core.Common.Models;
using Core.DataAccess.Base.Database;
using Core.DataAccess.Records.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.DB
{
    public interface IRecordsRepository : IRepository<Record>
    {
        Task<List<Record>> GetWithAllDependencies(Pagination pagination);
    }
}
