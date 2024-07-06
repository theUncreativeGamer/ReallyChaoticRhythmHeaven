using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    [SerializeField] private bool isPlaying;
    public bool IsPlaying { get => isPlaying; private set => isPlaying = value; }

    [SerializeField] private MusicTrack musicTrackInfo;
    [SerializeField] private TMPro.TextMeshProUGUI scoreDisplay;
    [SerializeField] private GoofyRapperAnimationController animationController;
    [SerializeField] private GameObject shakerPrefab;
    [SerializeField] private Transform shakerStartPosition;
    [SerializeField] private Transform shakerEndPosition;

    [Space(5)]
    public Beverage currentBeverage;
    [SerializeField] private IconListDisplay baseLiquidsDisplay;
    [SerializeField] private IconListDisplay syrupsDisplay;
    [SerializeField] private IconListDisplay sidesDisplay;
    [SerializeField] private int score = 0;

    private GameObject[] shakerInstances;
    private int currentShakerIndex;
    public TweenerProperty GetCurrentShaker()
    {
        if (currentShakerIndex >= 0)
            return shakerInstances[currentShakerIndex].GetComponent<TweenerProperty>();
        else
            return null;
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreDisplay.text = score.ToString();
        }
    }

    private Beverage[] beverages;
    private SoundCueList[] soundCueLists;
    private AudioSource audioSource;

    public bool StartPlaying()
    {
        if (IsPlaying) return false;
        IsPlaying = true;
        shakerInstances = new GameObject[2];
        shakerInstances[0] = Instantiate(shakerPrefab, shakerEndPosition);
        shakerInstances[1] = Instantiate(shakerPrefab, shakerEndPosition);
        currentShakerIndex = -1;
        GetComponent<BeatKeeper>().StartPlayingMusic();
        return true;
    }

    public void LoadMusicTrack(MusicTrack track)
    {
        musicTrackInfo = track;
        beverages = BeatPatternUtilities.GenerateBeverages(track);
        soundCueLists = BeatPatternUtilities.GenerateCueLists(beverages, track);
    }

    public bool AddIngredientToCurrentBeverage(Ingredient ingredient)
    {
        if (!IsPlaying)
        {
            audioSource.clip = ingredient.GetCue();
            audioSource.Play();
            animationController.TriggerCue();
            return false;
        }

        if (currentBeverage == null) { return false; }

        bool result = currentBeverage.Add(ingredient);
        if (result)
        {
            UpdateIngredientDisplays();
            return true;
        }

        return false;
    }

    private void UpdateIngredientDisplays()
    {
        baseLiquidsDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.baseLiquids);
        syrupsDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.syrups);
        sidesDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.sideIngredients);
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BroadcastMessage("LoadMusicTrack", musicTrackInfo);
    }

    public void StartMeasureEvent(int measure)
    {
        CompareBeverage(measure - 1);
        if (measure >= musicTrackInfo.MeasureCount)
        {
            return;
        }

        var stuff = GetCurrentShaker();
        if (stuff != null) stuff.TweenTo(shakerEndPosition.position, 0.1f, DG.Tweening.Ease.OutCubic);
        if (soundCueLists[measure] == null)
        {
            currentShakerIndex = -1;
        }
        else
        {
            currentShakerIndex = currentShakerIndex == 0 ? 1 : 0;
            GetCurrentShaker().TweenTo(shakerStartPosition.position, 0.1f, DG.Tweening.Ease.OutCubic);
        }

        currentBeverage = ScriptableObject.CreateInstance<Beverage>();
        UpdateIngredientDisplays();
        SendCues(soundCueLists[measure]);
    }

    private void SendCues(SoundCueList cueList)
    {
        if (cueList == null) return;
        foreach (var cue in cueList.CueList)
        {
            StartCoroutine(PlayCue(cue.ingredient.GetCue(), cue.cueTime * (60f / musicTrackInfo.Bpm)));
        }
    }

    private IEnumerator PlayCue(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        animationController.TriggerCue();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void CompareBeverage(int measure)
    {
        if (measure >= beverages.Length || measure < 0 || beverages[measure] == null)
        {
            return;
        }

        if (currentBeverage == beverages[measure])
        {
            Score += ScoreFunction(beverages[measure].IngredientCount);
        }
        else
        {
            Score -= ScoreFunction(beverages[measure].IngredientCount);
        }
        scoreDisplay.text = Score.ToString();
    }

    private int ScoreFunction(int num)
    {
        return num * 10;
    }
}
