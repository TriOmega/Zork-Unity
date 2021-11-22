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

    void Start()
    {
        
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
        game.Player.LocationChanged += OnPlayerLocationChanged;
        game.Player.MovesChanged += OnPlayerMovesChanged;
        game.Player.ScoreChanged += OnPlayerScoreChanged;

        game.Start(InputService, OutputService);
        //CurrentLocationText.text = game.Player.Location.Name;

        game.Player.Move(Directions.North);
        game.Commands["REWARD"].Action(game);
        game.Player.Move(Directions.South);
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
        CurrentLocationText.text = newLocation.Name;
    }
}
