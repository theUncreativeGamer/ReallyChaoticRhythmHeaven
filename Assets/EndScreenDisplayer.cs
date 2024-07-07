using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject[] stuffsToDisplay;
    [SerializeField] private float displayInterval;
    [Space(1)]
    [SerializeField] private GameObject scoreDisplayObject;
    [SerializeField] private Transform scoreDisplayPosition;
    [SerializeField] private TMPro.TextMeshProUGUI rankResponseDisplay;
    [SerializeField] private TMPro.TextMeshProUGUI rankDisplay;
    [SerializeField] private float startMovingDelay;
    [SerializeField] private float moveTime;
    [SerializeField] private string[] rankResponses;
    [SerializeField] public char rank;
    private float timer = 0;
    private int index = 0;

    private void Start()
    {
        StartCoroutine(MoveScoreDisplay());
        rankDisplay.text = rank.ToString();
        rankResponseDisplay.text = rankResponses[rank - 'A'];
    }

    private IEnumerator MoveScoreDisplay()
    {
        yield return new WaitForSeconds(startMovingDelay);
        // Create individual tweens
        Tweener positionTween = scoreDisplayObject.transform.DOMove(scoreDisplayPosition.position, moveTime);
        Tweener rotationTween = scoreDisplayObject.transform.DORotate(scoreDisplayPosition.rotation.eulerAngles, moveTime);
        Tweener scaleTween = scoreDisplayObject.transform.DOScale(scoreDisplayPosition.localScale, moveTime);

        // Combine tweens into a sequence
        Sequence sequence = DOTween.Sequence();
        sequence.Append(positionTween);
        sequence.Join(rotationTween);
        sequence.Join(scaleTween);
        sequence.SetEase(Ease.Linear);
    }

    private void Update()
    {
        if (index >= stuffsToDisplay.Length) return;
        timer += Time.deltaTime;
        while (timer > displayInterval)
        {
            timer -= displayInterval;
            stuffsToDisplay[index].SetActive(true);
            index++;
        }
    }
}
