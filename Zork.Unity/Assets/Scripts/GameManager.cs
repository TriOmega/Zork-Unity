using UnityEngine;
using Newtonsoft.Json;
using Zork;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CurrentLocationText;

    [SerializeField]
    private TextMeshProUGUI MovesCounterText;

    [SerializeField]
    private TextMeshProUGUI ScoreCounterText;

    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private UnityOutputService OutputService;

    private Game LoadedGame { get => _loadedGame; set => _loadedGame = value; }

    private Room previousLocation = null;

    void Start()
    {

        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        LoadedGame = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
        LoadedGame.Player.LocationChanged += OnPlayerLocationChanged;
        LoadedGame.Player.MovesChanged += OnPlayerMovesChanged;
        LoadedGame.Player.ScoreChanged += OnPlayerScoreChanged;

        LoadedGame.Start(InputService, OutputService);
        OutputService.WriteLine(string.IsNullOrWhiteSpace(LoadedGame.WelcomeMessage) ? "Welcome to Zork!" : LoadedGame.WelcomeMessage);
        CurrentLocationText.text = LoadedGame.Player.Location.Name;
    }

    private void OnPlayerScoreChanged(object sender, int updatedScore)
    {
        ScoreCounterText.text = updatedScore.ToString();
    }

    private void OnPlayerMovesChanged(object sender, int updatedMoves)
    {
        MovesCounterText.text = updatedMoves.ToString();
    }

    private void OnPlayerLocationChanged(object sender, Room newLocation)
    {
        if (newLocation != previousLocation)
        {
            Game.Look(LoadedGame);
            previousLocation = newLocation;
        }
        CurrentLocationText.text = newLocation.Name;
    }

    private Game _loadedGame;
}
