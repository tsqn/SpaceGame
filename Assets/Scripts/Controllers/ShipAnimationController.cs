using System;

using UnityEngine;

namespace Controllers
{
    public class ShipAnimationController : MonoBehaviour
    {
        private Animator _animator;

        public ShipMovementState MovementState;

        public void UpdateAnimation(ShipMovementState state)
        {
            MovementState = state;
            UpdateAnimation();
        }

        private void Idle()
        {
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
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