namespace Data.Models
{
    public class ApiLog
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Путь запроса
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Набор параметров запроса
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// Тело запроса
        /// </summary>
        public string? Payload { get; set; }

        /// <summary>
        /// Имя используемого метода
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Данные из тела ответа
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Статус ответа
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Дата и время запроса
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
