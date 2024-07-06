using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenerProperty : MonoBehaviour
{
    [SerializeField] bool returnWhenFinished = false;
    Tween currentTween;

    public void TweenTo(Vector3 position, float time, Ease ease)
    {
        currentTween.Kill();
        currentTween = transform.DOMove(position, time);
        currentTween.SetEase(ease);
        if (returnWhenFinished) currentTween.OnComplete(Return);
    }

    public void JumpTo(Vector3 position, float height, float time, Ease ease)
    {
        currentTween.Kill();
        currentTween = transform.DOJump(position, height, 1, time);
        currentTween.SetEase(ease);
        if (returnWhenFinished) currentTween.OnComplete(Return);
    }

    private void Return()
    {
        transform.localPosition = Vector3.zero;
    }
}
