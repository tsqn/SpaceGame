namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// Содержит коллекцию юнитов.
    /// </summary>
    [CreateAssetMenu(fileName = "UnitsCollection", menuName = Constants.GAME_NAME + "/Units Collection")]
    [Serializable]
    public class UnitsCollection : ScriptableObject
    {
        /// <summary>
        /// Коллекция юнитов.
        /// </summary>
        public List<Object> Units;

        /// <summary>
        /// Возвращает объект юнита по строковому идентификатору.
        /// </summary>
        /// <param name="sid">Строковый идентификатор.</param>
        public GameObject GetUnitPrefabBySid(string sid)
        {
            return Units.OfType<GameObject>().First(unit => unit.GetComponent<Unit>().Sid == sid);
        }
    }
}