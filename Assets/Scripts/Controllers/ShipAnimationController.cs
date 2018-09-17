namespace Controllers
{
    using System;

    using UnityEngine;

    public class ShipAnimationController : MonoBehaviour
    {
        public ShipMovementState MovementState;

        public void UpdateAnimation(ShipMovementState state)
        {
            MovementState = state;
            UpdateAnimation();
        }

        private void Idle()
        {
        }

        private void StrifeLeft()
        {
        }

        private void StrifeRight()
        {
        }

        private void Update()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            switch (MovementState)
            {
                case ShipMovementState.StrifeLeft:
                    StrifeLeft();
                    break;
                case ShipMovementState.StrifeRight:
                    StrifeRight();
                    break;
                case ShipMovementState.Idle:
                    Idle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}