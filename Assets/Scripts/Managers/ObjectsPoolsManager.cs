namespace Managers
{
    using System.Collections.Generic;

    using Entities;

    using UnityEngine;

    /// <summary>
    /// Менеджер пулов объектов.
    /// </summary>
    public class ObjectsPoolsManager : MonoBehaviour
    {
        /// <summary>
        /// Инстанс менеджера (синглтон).
        /// </summary>
        public static ObjectsPoolsManager Instance;

        /// <summary>
        /// Словарь пулов объектов.
        /// </summary>
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        /// <summary>
        /// Список пулов объектов. 
        /// </summary>
        public List<Pool> Pools;

        /// <summary>
        /// Спавнит объект из пула.
        /// </summary>
        /// <param name="sid">Строковый идентификатор.</param>
        /// <param name="position">Позиция.</param>
        /// <param name="rotation">Вращение.</param>
        public GameObject SpawnFromPool(string sid, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(sid))
            {
                Debug.LogWarning($"Poll with tag: {sid} doesn't exist");
                return null;
            }

            var objectToSpawn = _poolDictionary[sid].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;

            objectToSpawn.transform.rotation = rotation;

            objectToSpawn.GetComponentInParent<IPoolable>()?.OnObjectSpawn();

            _poolDictionary[sid].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        /// <summary>
        /// Спавнит объект из пула.
        /// </summary>
        /// <param name="sid">Строковый идентификатор.</param>
        /// <param name="position">Позиция.</param>
        public GameObject SpawnFromPool(string sid, Vector3 position)
        {
            if (!_poolDictionary.ContainsKey(sid))
            {
                Debug.LogWarning($"Poll with tag: {sid} doesn't exist");
                return null;
            }

            var objectToSpawn = _poolDictionary[sid].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;

            objectToSpawn.GetComponentInParent<IPoolable>()?.OnObjectSpawn();

            _poolDictionary[sid].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        /// <summary>
        /// Спавнит партикл из пула и проигрывает его.
        /// </summary>
        /// <param name="sid">Строковый индетификатор.</param>
        /// <param name="position">Позиция</param>
        public void SpawnParticleFromPool(string sid, Vector3 position)
        {
            var particle = Instance.SpawnFromPool("RocketExplosionVFX", position).GetComponent<ParticleSystem>();
            particle.Play(true);
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            if (Application.isPlaying)
                DontDestroyOnLoad(gameObject);

            if (_poolDictionary == null)
                InitializePolls();
        }

        /// <summary>
        /// Инициализация пулов объектов.
        /// </summary>
        private void InitializePolls()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in Pools)
            {
                var objectPool = new Queue<GameObject>();

                for (var i = 0; i < pool.Size; i++)
                {
                    var instance = Instantiate(pool.Prefab);
                    instance.SetActive(false);
                    objectPool.Enqueue(instance);
                }

                _poolDictionary.Add(pool.Prefab.GetComponent<Entity>().Sid, objectPool);
            }
        }
    }
}