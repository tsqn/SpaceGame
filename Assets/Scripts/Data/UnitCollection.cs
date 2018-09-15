namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Содержит коллекцию юнитов.
    /// </summary>
    [CreateAssetMenu(fileName = "Units list", menuName = Constants.GAME_NAME + "/Units list")]
    [Serializable]
    public class UnitsCollection : ScriptableObject
    {
        private const string ASSET_NAME = "UnitsList.asset";

        private static UnitsCollection _instance;

        /// <summary>
        /// Коллекция юнитов.
        /// </summary>
        public List<Unit> Units;

        
        /// <summary>
        /// Возвращает объект юнита по строковому идентификатору.
        /// </summary>
        /// <param name="sid">Строковый идентификатор.</param>
        public GameObject GetUnitPrefabBySid(string sid)
        {
            return  Units.First(unit => unit.Sid == sid).gameObject;
        }
        
        public static UnitsCollection Instance
        {
            get
            {
                if (_instance != null && _instance.Units != null)
                    return _instance;

                if (_instance != null)
                    if (_instance.Units == null)
                        _instance.Units = new List<Unit>();

                _instance = AssetDatabase.LoadAssetAtPath<UnitsCollection>(UnitsPath);

                if (_instance != null)
                    return _instance;

                _instance = CreateInstance<UnitsCollection>();

                if (_instance.Units == null)
                    _instance.Units = new List<Unit>();

                AssetDatabase.CreateAsset(_instance, UnitsPath);

                return _instance;
            }
        }

        private static string UnitsPath => $"{Utils.ShipStatsDataPath}/{ASSET_NAME}";
    }
}