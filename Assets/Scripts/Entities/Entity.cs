namespace Entities
{
    using UnityEngine;

    public class Entity : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// Строковый идетификатор Сущности.
        /// </summary>
        public string Sid;
        
        public virtual void OnObjectSpawn()
        {
        }
    }
}