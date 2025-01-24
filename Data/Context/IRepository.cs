using Data.Models;

namespace Data.Context
{
    public interface IRepository
    {
        /// <summary>
        /// Получение всех записей
        /// </summary>
        IQueryable<DataRecord> GetAll();

        /// <summary>
        /// Создание записей
        /// </summary>
        /// <param name="records"></param>
        Task CreateMany(IEnumerable<DataRecord> records);

        /// <summary>
        /// Очистка записей
        /// </summary>
        Task Clear();
    }
}
