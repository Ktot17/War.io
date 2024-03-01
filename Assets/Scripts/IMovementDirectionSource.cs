using UnityEngine;

namespace War.io
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get;  }
    }
}