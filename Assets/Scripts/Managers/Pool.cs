namespace Managers
{
    using System;

    using UnityEngine;

    /// <summary>
    /// Пул объектов.
    /// </summary>
    [Serializable]
    public class Pool
    {
        /// <summary>
        /// Префаб.
        /// </summary>
        public GameObject Prefab;

        /// <summary>
        /// Колличество объектов в пуле.
        /// </summary>
        public int Size;
    }
}