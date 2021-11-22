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

    public void Write(object value)
    {
        throw new System.NotImplementedException();
    }

    public void WriteLine(object value)
    {
        var newLine = GameObject.Instantiate(TextLinePrefab);
        newLine.transform.SetParent(ScrollViewContent.transform, false);
    }
}
