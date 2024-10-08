﻿using AutoMapper;
using Core.Business.Auth;
using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Common.Models.Search;
using Core.DataAccess.Base.Database;
using Core.DataAccess.Records.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Records.Component
{
    public class RecordsComponent : IRecordsComponent
    {
        private readonly IUnitOfWork _context;
        private readonly ICurrentUser _user;
        private readonly IMapper _mapper;

        public RecordsComponent(
            IUnitOfWork context,
            ICurrentUser user,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task Add(RecordModel model)
        {
            throw new NotImplementedException();
        }

        public async Task AddDefault(RecordModel model)
        {
            FillDefaultFields(model);

            await _context.Records.Add(_mapper.Map<Record>(model));
            await _context.SaveChanges();
        }

        private void FillDefaultFields(RecordModel model)
        {
            SetIdIfNull(model);

            var utcNow = DateTime.UtcNow;
            model.CreatedOn = utcNow;
            model.ModifiedOn = utcNow;

            model.CreatedBy = _user.EmployeeId;
            model.ModifiedBy = _user.EmployeeId;
        }

        private void SetIdIfNull(RecordModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrEmpty(model.File.Id))
            {
                model.File.Id = Guid.NewGuid().ToString();
            }
        }

        public async Task Delete(string id)
        {
            await _context.Records.Delete(id);
            await _context.SaveChanges();
        }

        public async Task<List<RecordModel>> Get(Pagination pagination)
        {
            var result = await _context.Records.GetAll();
            return _mapper.Map<List<RecordModel>>(result);
        }

        public async Task<SearchResult<RecordModel>> GetWithDependencies(RecordSearchContext context)
        {
            var result = await _context.Records.GetWithAllDependencies(context);
            return result.RecreateWithType(x => _mapper.Map<List<RecordModel>>(x));
        }

        public async Task<RecordModel> GetById(string id)
        {
            var record = await _context.Records.GetByIdAsNoTracking(id);
            return _mapper.Map<RecordModel>(record);
        }

        public async Task Update(RecordModel model)
        {
            try
            {
                var record = await _context.Records.GetById(model.Id);
                if (HasPreviewChanged(record, model))
                {
                    await _context.Records.DeletePreview(record.Id);
                }

                model.ModifiedBy = _user.EmployeeId;
                model.ModifiedOn = DateTime.UtcNow;

                var newRecord = _mapper.Map<RecordModel, Record>(model, record);
                await _context.Records.Update(newRecord);
                await _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        private bool HasPreviewChanged(Record record, RecordModel model)
        {
            return record.Preview?.Id != null
                && model.Preview?.Id != null
                && record.Preview.Id != model.Preview?.Id;
        }
    }
}
