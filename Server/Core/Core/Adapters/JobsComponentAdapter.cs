using Core.Business.Records.Facade;
using Core.Business.Records.Models;
using FileProcessor.Actions.Base;
using FileProcessor.Actions.Preview;
using Newtonsoft.Json.Linq;
using Scheduler;
using Scheduler.Component;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Adapters
{
    public class JobsComponentAdapter : IRecordJobComponent
    {
        private readonly IJobsComponent _component;

        public JobsComponentAdapter(IJobsComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public Task SubmitPreviewGeneration(RecordModel model)
        {
            var previewAction = CreatePreviewAction(model);
            var data = JObject.FromObject(previewAction);

            return _component.Create(new JobData
            {
                Id = Guid.NewGuid().ToString(),
                Type = JobType.Actions,
                Data = data
            });
        }

        private ActionsRequest CreatePreviewAction(RecordModel model)
        {
            return new ActionsRequest
            {
                RecordId = model.Id,
                Actions = new List<IAction>
                {
                    new GeneratePreviewAction
                    {
                        OutputPath = CreateFileNamePreview(model),
                        RecordId = model.Id
                    }
                }
            };
        }

        private string CreateFileNamePreview(RecordModel model)
        {
            return Path.GetFileNameWithoutExtension(model.File.RelativePath) + "_preview.jpg";
        }
    }
}
