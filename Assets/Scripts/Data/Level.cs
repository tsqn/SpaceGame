namespace Data
{
    using System;

    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// Модель уровня.
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = Constants.GAME_NAME + "/Level")]
    [Serializable]
    public class Level : ScriptableObject
    {
        /// <summary>
        /// Фон.
        /// </summary>
        public Object Background;

        /// <summary>
        /// Название уровня.
        /// </summary>
        public string Name;

        /// <summary>
        /// Расположение юнитов.
        /// </summary>
        public LevelUnitPositions UnitPositions;
    }
}