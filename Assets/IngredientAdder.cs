using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IHasIngredient))]
public class IngredientAdder : MonoBehaviour, IInteractable
{
    [SerializeField] private List<AudioClip> noises;
    [SerializeField] private Transform shakerPosition;
    [SerializeField] private Animation animationEffect;
    [SerializeField] private UnityEvent otherEffects;
    private IHasIngredient _ingredient;
    private AudioSource _audioSource;
    private void Awake()
    {
        _ingredient = GetComponent<IHasIngredient>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        if(GameManager.Instance.AddIngredientToCurrentBeverage(_ingredient.GetIngredient()))
        {
            _audioSource.clip = noises.GetRandom();
            _audioSource.Play();
            if( animationEffect!= null)
            {
                animationEffect.Stop();
                animationEffect.Play();
            }
            
            otherEffects.Invoke();

            var shaker = GameManager.Instance.GetCurrentShaker();
            if (shaker != null) shaker.TweenTo(shakerPosition.position, 0.1f, DG.Tweening.Ease.OutCubic);
        }
    }
}
