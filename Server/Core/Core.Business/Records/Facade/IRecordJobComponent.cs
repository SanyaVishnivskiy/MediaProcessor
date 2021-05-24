using Core.Business.Records.Models;
using System.Threading.Tasks;

namespace Core.Business.Records.Facade
{
    public interface IRecordJobComponent
    {
        Task SubmitPreviewGeneration(RecordModel model);
    }
}
