using UnityEngine;
using Newtonsoft.Json;
using Zork;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
    }
}
