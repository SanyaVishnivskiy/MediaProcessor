using Core.Business.Records.Models;
using Core.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Records.Component
{
    public interface IRecordsComponent
    {
        Task<List<RecordModel>> Get(Pagination pagination);
        Task<List<RecordModel>> GetWithDependencies(Pagination pagination);
        Task<RecordModel> GetById(string id);
        Task Add(RecordModel model);
        Task AddDefault(RecordModel model);
        Task Update(RecordModel model);
        Task Delete();
    }
}
