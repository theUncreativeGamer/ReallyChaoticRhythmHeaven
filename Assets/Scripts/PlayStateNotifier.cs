using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public static class PlayStateNotifier
{

    static PlayStateNotifier()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    static void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.EnteredEditMode)
        {
            Debug.Log("Entered Edit mode.");
            var allObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<IOnEnterEditMode>();
            foreach (var obj in allObjects)
            {
                obj.OnEnterEditMode();
            }
        }
    }
}
#endif