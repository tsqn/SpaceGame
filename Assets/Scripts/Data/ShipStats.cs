namespace Data
{
    using System;
    using System.Collections.Generic;

    using UnityEditor;

    using UnityEngine;

    [CreateAssetMenu(fileName = "Ship Stats Data", menuName = Constants.GAME_NAME + "/Ship Stats Data")]
    [Serializable]
    public class ShipStats : ScriptableObject
    {
        private const string ASSET_NAME = "ShipStatsData.asset";

        private static ShipStats _instance;

        public List<BaseShipStats> BaseShipStats;

        public List<ShipStatsMultipliers> ShipStatsMultipliers;

        public static ShipStats Instance
        {
            get
            {
                if (_instance != null && _instance.BaseShipStats != null && _instance.ShipStatsMultipliers != null)
                    return _instance;

                if (_instance != null)
                {
                    if (_instance.BaseShipStats == null)
                        _instance.BaseShipStats = new List<BaseShipStats>();

                    if (_instance.ShipStatsMultipliers == null)
                        _instance.ShipStatsMultipliers = new List<ShipStatsMultipliers>();
                }


                _instance = AssetDatabase.LoadAssetAtPath<ShipStats>(ShipStatsPath);

                if (_instance != null)
                    return _instance;

                _instance = CreateInstance<ShipStats>();

                if (_instance.BaseShipStats == null)
                    _instance.BaseShipStats = new List<BaseShipStats>();

                if (_instance.ShipStatsMultipliers == null)
                    _instance.ShipStatsMultipliers = new List<ShipStatsMultipliers>();

                AssetDatabase.CreateAsset(_instance, ShipStatsPath);

                return _instance;
            }
        }

        private static string ShipStatsPath => $"{Utils.ShipStatsDataPath}/{ASSET_NAME}";
    }
}