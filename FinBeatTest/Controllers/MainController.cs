using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace FinBeatTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MainController : Controller
    {

        private readonly ICodeValueService _codeValueService;
        private readonly ILogger<MainController> _logger;
        private readonly IMapper _queryMapping;

        public MainController(ICodeValueService iCodeValueService, QueryMapping queryMapping, ILogger<MainController> logger)
        {
            _codeValueService = iCodeValueService;
            _queryMapping = queryMapping.Mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получение записей
        /// </summary>
        /// <param name="id">Порядковый номер</param>
        /// <param name="code">код</param>
        /// <param name="value">значение</param>
        [HttpGet]
        public async Task<IActionResult> Get(int? id, int? code, string? value)
        {
            var filter = new CodeValueFilter() { Id = id, Code = code, Value = value };
            return Ok( await _codeValueService.GetByFilterAsync(filter));
        }

        /// <summary>
        /// Создание записей
        /// </summary>
        /// <param name="data">список пар код - значение</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> data) 
        {
            if (data != null)
            {
                try
                {
                    var codeValueList = data.Select(x=>_queryMapping.Map<CodeValueModel>(x)).ToArray();
                    await _codeValueService.CreateDataAsync(codeValueList);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving data.");
                    return StatusCode(500, "Internal server error.");
                }
            }

            return Ok();
        }

    }
}
