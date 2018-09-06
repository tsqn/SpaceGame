namespace Data
{
    using UnityEngine;

    public class BaseShipStats : ScriptableObject
    {
        public float Health;
        public float Mobility;
        public float MoveSpeed;
        public float ShootSpeed;
        public string Type;

        public override string ToString()
        {
            return Type;
        }
    }
}