namespace Data
{
    using System;

    using UnityEngine;

    [CreateAssetMenu(fileName = "Base Ship Stats", menuName = Constants.GAME_NAME + "/Base Ship Stats")]
    [Serializable]
    public class BaseShipStats : ScriptableObject
    {
        public string Type;
        public float Health;
        public float Mobility;
        public float MoveSpeed;
        public float ShootSpeed;

        public override string ToString()
        {
            return Type;
        }
    }
}