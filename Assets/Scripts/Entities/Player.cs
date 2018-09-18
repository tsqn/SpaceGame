namespace Entities
{
    using UnityEngine;

    public class Player : Unit
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
    }
}