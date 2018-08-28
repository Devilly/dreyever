using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAlongWithinBounds : CinemachineExtension
{
    internal Bounds bounds;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            float minY = bounds.min.y + Camera.main.orthographicSize;
            float maxY = bounds.max.y - Camera.main.orthographicSize;

            var pos = state.RawPosition;
            if (pos.y < minY)
            {
                pos.y = minY;
            }
            
            if (pos.y > maxY)
            {
                pos.y = maxY;
            }
            
            state.RawPosition = pos;
        }
    }
}