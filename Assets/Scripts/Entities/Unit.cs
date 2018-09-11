namespace Entities
{
    using Data;

    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        /// <summary>
        /// Строковый идетификатор юнита.
        /// </summary>
        public string Sid;
        
        /// <summary>
        /// Базовые характеристика юнита.
        /// </summary>
        public BaseShipStats BaseShipStats;
        
        /// <summary>
        /// Модификаторы характеристик юнита.
        /// </summary>
        public ShipStatsMultipliers ShipStatsMultipliers;
    }
}