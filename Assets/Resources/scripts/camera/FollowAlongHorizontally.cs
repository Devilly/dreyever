using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/follow-only-along-a-certain-axis.544511/#post-3591751
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