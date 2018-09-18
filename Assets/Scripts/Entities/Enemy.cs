namespace Entities
{
    using System.Collections;

    public class Enemy : Unit
    {
        /// <summary>
        /// Бесконечная стрельба.
        /// </summary>
        private IEnumerator EndlessShooting()
        {
            while (true)
            {
                Shoot();
                yield return null;
            }
        }

        private void Start()
        {
            StartCoroutine(EndlessShooting());
        }
    }
}