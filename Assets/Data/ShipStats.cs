namespace Data
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    [CreateAssetMenu(fileName = "ShipStatsData", menuName = "SpaceGame/ShipStatsData")]
    [Serializable]
    public class ShipStats : ScriptableObject
    {
        public List<BaseShipStats> BaseShipStats;

        public List<ShipStatsMultipliers> ShipStatsMultipliers;
    }
}