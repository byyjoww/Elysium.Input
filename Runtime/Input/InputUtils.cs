using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtils
{
    public static Vector3 AdjustInputWithCameraDirection(Vector2 _input, Vector3 _ground, Camera _cam, bool _normalize = true)
    {
        Vector3 flattenedCameraForward = _cam.transform.forward - Vector3.Project(_cam.transform.forward, _ground);
        Vector3 flattenedCameraRight = _cam.transform.right - Vector3.Project(_cam.transform.right, _ground);

        Vector3 adjustedMovementForward = flattenedCameraRight.normalized * _input.x;
        Vector3 adjustedMovementRight = flattenedCameraForward.normalized * _input.y;

        Vector3 adjustedMovement = adjustedMovementForward + adjustedMovementRight;
        Vector3 finalMovementVector = adjustedMovement - Vector3.Project(adjustedMovement, _ground);

        if (_normalize) { finalMovementVector = finalMovementVector.normalized; }

        return finalMovementVector;
    }
}
