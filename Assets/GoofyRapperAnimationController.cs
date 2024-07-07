using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyRapperAnimationController : MonoBehaviour, ILoadMusicTrack
{
    private Animator m_Animator;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    public void LoadMusicTrack(MusicTrack track)
    {
        m_Animator.SetFloat("Speed", track.Bpm / 60);
    }

    public void StartMeasureEvent(int measure)
    {
        Debug.Log("goofy");
        m_Animator.Play("Idle", 0, 0);
    }

    public void TriggerCue()
    {
        m_Animator.SetBool("Yell", true);
    }
}
