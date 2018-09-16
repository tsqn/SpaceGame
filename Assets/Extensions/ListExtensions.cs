using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Методы расширения для списка.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Перемещение элемента в на одну позицию в конец коллекции.
        /// </summary>
        /// <param name="list">Коллекция.</param>
        /// <param name="item">Элемент коллекции.</param>
        public static void MoveDown<T>(this List<T> list, T item)
        {
            var oldIndex = list.IndexOf(item);

            if (oldIndex == list.Count - 1)
                return;

            var newIndex = oldIndex + 1;
            Move(list, item, oldIndex, newIndex);
        }

        /// <summary>
        /// Перемещение элемента в на одну позицию в начало коллекции.
        /// </summary>
        /// <param name="list">Коллекция.</param>
        /// <param name="item">Элемент коллекции.</param>
        public static void MoveUp<T>(this List<T> list, T item)
        {
            var oldIndex = list.IndexOf(item);

            if (oldIndex == 0)
                return;

            var newIndex = oldIndex - 1;
            Move(list, item, oldIndex, newIndex);
        }

        /// <summary>
        /// Перемещение элекнта коллекции.
        /// </summary>
        /// <param name="list">Коллекция.</param>
        /// <param name="item">Элемент коллекции.</param>
        /// <param name="oldIndex">Старый идекс.</param>
        /// <param name="newIndex">Новый индекс.</param>
        private static void Move<T>(List<T> list, T item, int oldIndex, int newIndex)
        {
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }
    }
}