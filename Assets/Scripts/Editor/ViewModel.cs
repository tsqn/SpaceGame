namespace Editor
{
    /// <summary>
    /// Вьюмодель отображения.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    public class ViewModel<T> where T : class
    {
        /// <summary>
        /// Действие при отрисовке.
        /// </summary>
        public ActionOnUpdate ActionOnUpdate { get; set; }

        /// <summary>
        /// Раскрыта ли вьюмодель.
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Модель.
        /// </summary>
        public T Model { get; set; }
    }
}