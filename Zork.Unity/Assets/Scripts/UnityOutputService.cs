using UnityEngine;
using TMPro;
using Zork;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI NewLinePrefab;
    
    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private RectTransform ScrollViewContent;

    public void Write(string value) => WriteLine(value);

    public void WriteLine(string textOutput)
    {
        var newLine = GameObject.Instantiate(TextLinePrefab);
        newLine.text = textOutput;
        newLine.transform.SetParent(ScrollViewContent.transform, false);
    }
}
