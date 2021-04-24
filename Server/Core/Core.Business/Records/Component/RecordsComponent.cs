using AutoMapper;
using Core.Business.Records.Models;
using Core.Common.Models;
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
        private readonly IMapper _mapper;

        public RecordsComponent(
            IUnitOfWork context,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RecordModel>> Get(Pagination pagination)
        {
            var result = await _context.Records.GetAll();
            return _mapper.Map<List<RecordModel>>(result);
        }

        public async Task<List<RecordModel>> GetWithDependencies(Pagination pagination)
        {
            var result = await _context.Records.GetWithAllDependencies(pagination);
            return _mapper.Map<List<RecordModel>>(result);
        }

        public async Task<RecordModel> GetById(string id)
        {
            var record = await _context.Records.GetById(id);
            return _mapper.Map<RecordModel>(record);
        }

        public async Task Update(RecordModel model)
        {
            var record = _mapper.Map<Record>(model);
            await _context.Records.Update(record);
            await _context.SaveChanges();
        }
    }
}
