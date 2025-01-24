using FinBeatTest;

namespace Services.Services
{
    /// <summary>
    /// Сервис для работы с данными
    /// </summary>
    public interface ICodeValueService
    {
        /// <summary>
        /// Получение данных по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        Task<CodeValueModel[]> GetByFilterAsync(CodeValueFilter filter);

        /// <summary>
        /// Создание записей
        /// </summary>
        /// <param name="codeValueList">Список записей</param>
        Task CreateDataAsync(CodeValueModel[] codeValueList);
    }
}
