using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTrackLoader : MonoBehaviour
{
    public static MusicTrackLoader Instance;
    public MusicTrack musicToLoad;

    void Awake()
    {
        // If there's already an instance, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this
        Instance = this;

        // Make this object persist across scene loads
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Call your function here
        SceneChanged();
    }

    private void SceneChanged()
    {
        if (musicToLoad == null) return;
        var list = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ILoadMusicTrack>();
        foreach (var v in list)
        {
            v.LoadMusicTrack(musicToLoad);
        }
    }
}
