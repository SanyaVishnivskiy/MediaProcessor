﻿using FileProcessor.Actions.Base;
using FileProcessor.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileProcessor.Actions
{
    public interface IActionsProcessorFacade
    {
        Task<List<IAction>> GetActions(string recordId);
        Task Process(ActionsRequest request);
    }

    public class ActionsProcessorFacade : IActionsProcessorFacade
    {
        private readonly IActionsHandlerFactory _factory;
        private readonly IActionsMappings _mappings;
        private readonly ILocalRecordsComponent _filesComponent;

        public ActionsProcessorFacade(
            IActionsHandlerFactory factory,
            IActionsMappings mappings,
            ILocalRecordsComponent component)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
            _filesComponent = component ?? throw new ArgumentNullException(nameof(component));
        }

        public async Task<List<IAction>> GetActions(string recordId)
        {
            var file = await _filesComponent.DownloadLocally(recordId);

            var result = _mappings.GetPossibleActionsByName(file.LocalPath);

            await DeleteLocalFile(file.LocalPath);

            return result;
        }

        public async Task Process(ActionsRequest request)
        {
            if (request?.Actions?.Any() == false)
                return;

            var downloadResult = await _filesComponent.DownloadLocally(request.RecordId);
            UpdateActionPaths(request, downloadResult);

            foreach (var action in request.Actions)
            {
                await ProcessAction(action);
            }

            await DeleteLocalFile(downloadResult.LocalPath);
        }

        private async Task ProcessAction(IAction action)
        {
            var result = await HandleAction(action);
            if (!result.Success)
                return;

            if (result.CreateNewRecord)
            {
                await AddRecord(action, result);
            }

            await DeleteLocalFile(result.ResultFilePath);
        }

        private void UpdateActionPaths(
            ActionsRequest request,
            DownloadingResult downloadResult)
        {
            foreach (var action in request.Actions)
            {
                action.InputPath = downloadResult.LocalPath;
                action.OutputPath = BuildOutputPath(action);
            }
        }

        private string BuildOutputPath(IAction action)
        {
            var directory = Path.GetDirectoryName(action.InputPath);
            var fileName = action.GenerateOuputFileName();

            return Path.Combine(directory, fileName);
        }

        private Task<ActionHandlerResult> HandleAction(IAction action)
        {
            var handler = _factory.Create(action);
            if (handler is null)
            {
                return Task.FromResult(ActionHandlerResult.Failed());
            }

            return handler.Handle(action);
        }

        private Task AddRecord(IAction action, ActionHandlerResult result)
        {
            return _filesComponent.AddRecord(new ActionRecord
            {
                FileName = Path.GetFileName(action.OutputPath),
                LocalFilePath = result.ResultFilePath
            });
        }

        private Task DeleteLocalFile(string localPath)
        {
            return _filesComponent.DeleteLocally(localPath);
        }
    }
}
