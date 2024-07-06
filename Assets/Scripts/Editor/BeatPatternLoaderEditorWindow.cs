using UnityEditor;
using UnityEngine;

public class BeatPatternLoaderEditorWindow : EditorWindow
{
    public TextAsset textFile;
    public string outputPath = "Assets/New Pattern Data.asset";

    [MenuItem("Window/Beat Pattern Loader")]
    public static void ShowWindow()
    {
        GetWindow<BeatPatternLoaderEditorWindow>("Beat Pattern Loader");
    }

    void OnGUI()
    {
        GUILayout.Label("Load Beat Patterns from Text File", EditorStyles.boldLabel);

        textFile = (TextAsset)EditorGUILayout.ObjectField("Text File", textFile, typeof(TextAsset), false);
        outputPath = EditorGUILayout.TextField("Output Path", outputPath);

        if (GUILayout.Button("Load Beat Patterns"))
        {
            if (textFile != null)
            {
                BeatPatternData beatPatternData = BeatPatternUtilities.LoadTextFile(textFile);

                /*
                // Combine Application.dataPath with outputPath
                string assetPath = Path.Combine(Application.dataPath, outputPath);

                // Ensure the directory exists before saving the asset
                string directoryPath = Path.GetDirectoryName(assetPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                */

                // Save the asset
                AssetDatabase.CreateAsset(beatPatternData, "Assets/NewPatternData.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Success", "Beat patterns loaded successfully!", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign both the text file and the beat pattern data asset.", "OK");
            }
        }
    }


}
