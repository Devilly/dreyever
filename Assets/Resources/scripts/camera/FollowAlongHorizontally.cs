using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAlongHorizontally : CinemachineExtension
{
    internal float yPosition = 2;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.y = yPosition;
            state.RawPosition = pos;
        }
    }
}