using AutoMapper;
using Data.Context;
using Data.Models;
using FinBeatTest;
using Microsoft.EntityFrameworkCore;
using Services.Mapping;

namespace Services.Services
{
    public class CodeValueService: ICodeValueService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CodeValueService(IRepository repository, ServiceMapping mapper)
        {
            _repository = repository;
            _mapper = mapper.Mapper;
        }

        ///<inheritdoc/>
        public async Task <CodeValueModel[]> GetByFilterAsync(CodeValueFilter filter)
        {
            DataRecord[] dataList;

            var query = _repository.GetAll();

            if (filter != null)
            {
                query = query.Where(r =>
                    (!filter.Code.HasValue || r.Code == filter.Code.Value) &&
                    (!filter.Id.HasValue || r.Id == filter.Id.Value) &&
                    (string.IsNullOrEmpty(filter.Value) || r.Value == filter.Value)
                );
            }

            dataList = await query.ToArrayAsync();

            return dataList.Select(_mapper.Map<DataRecord, CodeValueModel>).ToArray();
        }

        ///<inheritdoc/>
        public async Task CreateDataAsync(CodeValueModel[] codeValueList)
        {
            var records = codeValueList
                .Select(c => new DataRecord
                {
                    Code = c.Code,
                    Value = c.Value
                })
                .OrderBy(c => c.Code)
                .ToList();

            await _repository.Clear();
            await _repository.CreateMany(records);
        }

    }
}
