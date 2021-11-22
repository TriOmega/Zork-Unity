using System;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using Zork;

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

    public Game LoadedGame { get => _loadedGame; set => _loadedGame = value; }


    void Start()
    {

        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        LoadedGame = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
        LoadedGame.Player.LocationChanged += OnPlayerLocationChanged;
        LoadedGame.Player.MovesChanged += OnPlayerMovesChanged;
        LoadedGame.Player.ScoreChanged += OnPlayerScoreChanged;
        LoadedGame.GameEnded += OnGameEnded;

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
        if (newLocation != _previousLocation)
        {
            Game.Look(LoadedGame);
            _previousLocation = newLocation;
        }
        CurrentLocationText.text = newLocation.Name;
    }

    private void OnGameEnded(object sender, EventArgs e)
    {
        OutputService.WriteLine(string.IsNullOrWhiteSpace(LoadedGame.ExitMessage) ? "Thank you for playing!" : LoadedGame.ExitMessage);
        Invoke("QuitGame", _exitDelay); ;
    }

    private void QuitGame() => Application.Quit();

    private Game _loadedGame;
    private Room _previousLocation = null;
    private float _exitDelay = 5.0f;
}
