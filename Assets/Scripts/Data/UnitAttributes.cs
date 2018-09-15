namespace Data
{
    using System;
    using System.Collections.Generic;

    using UnityEditor;

    using UnityEngine;

    [CreateAssetMenu(fileName = "Ship Stats Data", menuName = Constants.GAME_NAME + "/Ship Stats Data")]
    [Serializable]
    public class UnitAttributes : ScriptableObject
    {
        private const string ASSET_NAME = "ShipStatsData.asset";
        private static UnitAttributes _instance;
        public List<UnitAttributesMultipliers> ShipAttributesMultipliers;
        public List<UnitBaseAttributes> ShipBaseAttributes;

        public static UnitAttributes Instance
        {
            get
            {
                if (_instance != null && _instance.ShipBaseAttributes != null &&
                    _instance.ShipAttributesMultipliers != null)
                    return _instance;

                if (_instance != null)
                {
                    if (_instance.ShipBaseAttributes == null)
                        _instance.ShipBaseAttributes = new List<UnitBaseAttributes>();

                    if (_instance.ShipAttributesMultipliers == null)
                        _instance.ShipAttributesMultipliers = new List<UnitAttributesMultipliers>();
                }

                _instance = Resources.Load<UnitAttributes>(ShipStatsPath);

#if UNITY_EDITOR

                if (_instance != null)
                    return _instance;

                _instance = CreateInstance<UnitAttributes>();

                if (_instance.ShipBaseAttributes == null)
                    _instance.ShipBaseAttributes = new List<UnitBaseAttributes>();

                if (_instance.ShipAttributesMultipliers == null)
                    _instance.ShipAttributesMultipliers = new List<UnitAttributesMultipliers>();

                AssetDatabase.CreateAsset(_instance, ShipStatsPath);
#endif

                return _instance;
            }
        }

        private static string ShipStatsPath => $"{Utils.ShipStatsDataPath}/{ASSET_NAME}";
    }
}