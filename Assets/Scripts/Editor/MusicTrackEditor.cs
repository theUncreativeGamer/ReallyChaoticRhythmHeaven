using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(MusicTrack))]
public class MusicTrackEditor : Editor
{
    private SerializedProperty textAssetProperty;

    private void OnEnable()
    {
        // Initialize serialized property for TextAsset
        textAssetProperty = serializedObject.FindProperty("beatPatternTextAsset");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        MusicTrack track = (MusicTrack)target;

        GUILayout.Space(10);

        // Display a field for assigning TextAsset
        EditorGUILayout.PropertyField(textAssetProperty, new GUIContent("Beat Pattern Text Asset"));

        GUILayout.Space(5);

        // Button to convert assigned TextAsset to array
        if (GUILayout.Button("Convert Text Asset to Array"))
        {
            if (textAssetProperty.objectReferenceValue != null)
            {
                TextAsset textAsset = (TextAsset)textAssetProperty.objectReferenceValue;
                int[] complexities = ReadComplexitiesFromTextAsset(textAsset);
                if (complexities != null)
                {
                    track.SetBeatPatternComplexities(complexities);
                    EditorUtility.SetDirty(track);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                else
                {
                    Debug.LogError("Failed to read complexities from the assigned TextAsset.");
                }
            }
            else
            {
                Debug.LogError("Please assign a TextAsset containing beat pattern complexities.");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    // Method to read complexities from TextAsset
    private int[] ReadComplexitiesFromTextAsset(TextAsset textAsset)
    {
        try
        {
            string[] nums = textAsset.text.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            int[] complexities = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                if (!int.TryParse(nums[i], out complexities[i]))
                {
                    Debug.LogErrorFormat("Failed to parse complexity value at line {0}", i + 1);
                    return null;
                }
            }

            return complexities;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading text asset: " + e.Message);
            return null;
        }
    }
}
