using System.IO;
using UnityEngine;



public static class BeatPatternUtilities
{

    public static BeatPatternData LoadTextFile(TextAsset textFile)
    {
        BeatPatternData data = ScriptableObject.CreateInstance<BeatPatternData>();

        using StringReader reader = new(textFile.text);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            //Debug.Log("Reading " + line);
            line = line.Trim();
            //Debug.Log("---1---");

            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {

                //Debug.Log("---2---");
                continue; // Skip empty lines and comments
            }

            //Debug.Log("---3---");
            string[] strings = line.Trim().Split(",");

            //Debug.Log("---4---");
            BeatPattern pattern = new BeatPattern(strings.Length);
            //Debug.Log("Pattern Length: " + pattern.Length);

            bool errorFlag = false;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (float.TryParse(strings[i].Trim(), out float value))
                {
                    //Debug.Log("BBBB");
                    pattern[i] = value;
                }
                else
                {
                    Debug.LogError("There is non-number value in the text file.");
                    errorFlag = true;
                    break;
                }

            }

            if (errorFlag) continue;

            BeatPatternCategory category = data.categories.Find(x => x.length == pattern.Length);
            if (category == null)
            {
                category = new BeatPatternCategory() { length = pattern.Length };
                data.categories.Add(category);
            }
            category.patterns.Add(pattern);
            //Debug.Log("AAAA");
        }

        return data;
    }

    public static Beverage[] GenerateBeverages(MusicTrack musicTrackInfo)
    {
        if (musicTrackInfo == null) return null;

        var beverages = new Beverage[musicTrackInfo.MeasureCount];
        for (int i = 0; i < musicTrackInfo.MeasureCount; i++)
        {
            if (musicTrackInfo.BeatPatternComplexities[i] == 0)
            {
                beverages[i] = null;
            }
            else
            {
                int beatCount = musicTrackInfo.BeatPatternComplexities[i];
                beverages[i] = musicTrackInfo.AvailableIngredients.RandomBeverage(new Range<int>(1, 3), beatCount);
            }

        }
        return beverages;

    }

    public static SoundCueList[] GenerateCueLists(Beverage[] beverages, MusicTrack musicTrackInfo)
    {
        var soundCueLists = new SoundCueList[musicTrackInfo.MeasureCount];
        for(int i = 0;i < beverages.Length;i++)
        {
            if (beverages[i] == null)
            {
                soundCueLists[i] = null;
                continue;
            }
            soundCueLists[i] = beverages[i].ToCueList(musicTrackInfo.AvailableBeatPatterns.GetRandomPattern(beverages[i].IngredientCount));
        }
        return soundCueLists;
    }
}
