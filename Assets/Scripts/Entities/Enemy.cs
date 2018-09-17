namespace Entities
{
    using System.Collections;

    public class Enemy : Unit
    {
        private void Start()
        {
            StartCoroutine(EndlessShooting());
        }

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
    }
}