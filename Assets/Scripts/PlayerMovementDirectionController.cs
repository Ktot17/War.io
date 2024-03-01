using System;
using UnityEngine;

namespace War.io
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            MovementDirection = new Vector3(horizontal, 0 , vertical);
        }
    }
}