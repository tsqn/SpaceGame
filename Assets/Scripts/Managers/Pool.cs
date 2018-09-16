using System;

using UnityEngine;

namespace Managers
{
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