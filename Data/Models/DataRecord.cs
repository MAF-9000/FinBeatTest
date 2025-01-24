namespace Data.Models
{
    public class DataRecord
    {
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Id { get; set; } 
        /// <summary>
        /// Код
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string? Value { get; set; }
    }
}

