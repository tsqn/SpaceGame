namespace Models
{
    using System;

    using UnityEngine;

    [CreateAssetMenu(fileName = "Ship Stats Multipliers", menuName = Constants.GAME_NAME + "/Ship Stats Multipliers")]
    [Serializable]
    public class UnitAttributesMultipliers : UnitBaseAttributes
    {
        public float WeaponDamage;
    }
}