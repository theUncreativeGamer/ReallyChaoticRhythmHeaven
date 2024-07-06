using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffJumpSetter : MonoBehaviour
{
    [SerializeField] TweenerProperty stuff;
    [SerializeField] Transform targetPosition;
    [SerializeField] float jumpHeight;

    public void TriggerJump()
    {
        stuff.JumpTo(targetPosition.position, jumpHeight, 0.2f, DG.Tweening.Ease.OutCubic);
    }
}
