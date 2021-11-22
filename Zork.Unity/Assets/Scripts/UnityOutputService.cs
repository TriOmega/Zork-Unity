using System.Collections.Generic;
using System.Linq;
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

    private int EntriesLimit = 50;

    public UnityOutputService() => mEntries = new List<GameObject>();

    public void Write(string value) => WriteLine(value);

    public void WriteLine(string textOutput)
    {
        if (mEntries.Count > EntriesLimit)
        {
            Destroy(mEntries.First());
            mEntries.Remove(mEntries.First());
        }
        var newLine = GameObject.Instantiate(TextLinePrefab);
        newLine.text = textOutput;
        newLine.transform.SetParent(ScrollViewContent.transform, false);
        mEntries.Add(newLine.gameObject);
    }

    private readonly List<GameObject> mEntries;
}
