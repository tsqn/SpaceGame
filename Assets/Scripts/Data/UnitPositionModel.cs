namespace Data
{
    using Entities;

    using UnityEngine;

    [System.Serializable]
    public class UnitPositionModel : ScriptableObject
    {
        public string ObjectId;

        public int PosX;
        public int PosY;
        public int PosZ;
        
        public int RotX;
        public int RotY;
        public int RotZ;
    }
}