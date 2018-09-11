namespace Editor
{
    using UnityEngine;

    /// <summary>
    /// Вьюмодель отображения.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    public class ViewModel<T> where T : Object
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