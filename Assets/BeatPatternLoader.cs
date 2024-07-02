using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatPatternData", menuName = "ScriptableObjects/BeatPatternData", order = 1)]
public class BeatPatternData : ScriptableObject
{
    public List<BeatPatternCategory> categories;
}

[System.Serializable]
public class BeatPatternCategory
{
    public string categoryName;
    public List<BeatPattern> patterns;
}

[System.Serializable]
public class BeatPattern
{
    public string patternName;
    public string pattern;
}

public class BeatPatternLoader : MonoBehaviour
{
    public TextAsset textFile;
    public BeatPatternData beatPatternData;

    void Start()
    {
        ParseTextFile();
    }

    void ParseTextFile()
    {
        beatPatternData.categories = new List<BeatPatternCategory>();

        using StringReader reader = new(textFile.text);
        string line;
        BeatPatternCategory currentCategory = null;

        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();

            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {
                continue; // Skip empty lines and comments
            }

            if (line.Contains("-beat patterns"))
            {
                // New category
                currentCategory = new BeatPatternCategory
                {
                    categoryName = line,
                    patterns = new List<BeatPattern>()
                };
                beatPatternData.categories.Add(currentCategory);
            }
            else if (currentCategory != null && line.Contains(":"))
            {
                // New pattern
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    BeatPattern pattern = new BeatPattern
                    {
                        patternName = parts[0].Trim(),
                        pattern = parts[1].Trim()
                    };
                    currentCategory.patterns.Add(pattern);
                }
            }
        }
    }
}
