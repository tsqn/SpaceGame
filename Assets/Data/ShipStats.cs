namespace Data
{
    using System.Collections.Generic;

    using UnityEngine;

//    [CreateAssetMenu(fileName = "ShipStatsData", menuName = "SpaceGame/ShipStatsData")]
    public class ShipStats
    {
        [SerializeField]
        public List<BaseShipStats> BaseShipStats;

        [SerializeField]
        public List<ShipStatsMultipliers> ShipStatsMultipliers;
    }
}