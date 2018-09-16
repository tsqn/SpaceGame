namespace Models
{
    using System.Collections.Generic;

    using UnityEngine;

    public class LevelUnitPositions : ScriptableObject
    {
        public string Name;

        public List<UnitPosition> UnitPositionModels;
    }
}