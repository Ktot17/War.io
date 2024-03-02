using UnityEngine;

namespace War.io.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get;  }
    }
}