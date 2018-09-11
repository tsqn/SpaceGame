namespace Data
{
    using System;

    using UnityEngine;

    [CreateAssetMenu(fileName = "Ship Stats Multipliers", menuName = Constants.GAME_NAME + "/Ship Stats Multipliers")]
    [Serializable]
    public class ShipStatsMultipliers : BaseShipStats
    {
        public float WeaponDamage;
    }
}