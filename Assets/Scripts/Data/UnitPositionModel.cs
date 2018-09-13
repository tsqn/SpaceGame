namespace Data
{
    using System;

    using UnityEngine;

    using Object = UnityEngine.Object;

    [Serializable]
    public class UnitPositionModel
    {
        /// <summary>
        /// Префаб юнита.
        /// </summary>
        [SerializeField]
        public Object GameObject;

        /// <summary>
        /// Положение юнита.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Вращение юнита.
        /// </summary>
        public Quaternion Rotation;
    }
}