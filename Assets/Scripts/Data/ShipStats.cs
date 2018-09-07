namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using UnityEditor;

    using UnityEngine;

    [CreateAssetMenu(fileName = "ShipStatsData", menuName = "SpaceGame/ShipStatsData")]
    [Serializable]
    public class ShipStats : ScriptableObject
    {
        private const string ASSET_NAME = "ShipStatsData.asset";

        private static ShipStats _instance;

        [SerializeField]
        public List<BaseShipStats> BaseShipStats;

        [SerializeField]
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


                var path = Path.Combine(Utils.PackageRoot, ASSET_NAME);
                _instance = AssetDatabase.LoadAssetAtPath<ShipStats>(path);

                if (_instance != null)
                    return _instance;

                _instance = CreateInstance<ShipStats>();

                if (_instance.BaseShipStats == null)
                    _instance.BaseShipStats = new List<BaseShipStats>();

                if (_instance.ShipStatsMultipliers == null)
                    _instance.ShipStatsMultipliers = new List<ShipStatsMultipliers>();

                AssetDatabase.CreateAsset(_instance, path);

                return _instance;
            }
        }
    }
}