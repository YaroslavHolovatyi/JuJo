using UnityEngine;
using TMPro;
using System.Collections.Generic;




public class FlyingUnicode : MonoBehaviour
{

    public string[] visibleChars = new string[] {
        "@", "#", "$", "%", "&", "*", "(", ")", "!", "?", ":", ";", ".", ",", "+", "-", "=", "<", ">", "/", "\\", "[", "]", "{", "}", "|", "^", "_", "~",
        "©", "®", "™", "§", "¶", "‰", "µ", "°",
        "♥", "★", "♪", "✓", "•", "◉", "☻", "☺", "♠", "♣", "♦", "♤", "♡", "♢"
    };

    public RectTransform area;
    public GameObject symbolPrefab;
    public int maxSymbols = 256;
    public float minSpeed = 10f, maxSpeed = 30f;

    private List<GameObject> activeSymbols = new List<GameObject>();

    void Start()
    {
        Debug.Log("SymbolPrefab: " + symbolPrefab);
        Debug.Log("Area: " + area);
        for (int i = 0; i < maxSymbols; i++)
            SpawnSymbol();
    }

    void SpawnSymbol()
    {

        if (symbolPrefab == null)
        {
            Debug.LogError("symbolPrefab is NULL!");
            return;
        }
        GameObject symbol = Instantiate(symbolPrefab, area);
        Debug.Log("SymbolPrefab інстанційовано: " + symbol);
        var allComponents = symbol.GetComponents<Component>();
        foreach (var comp in allComponents)
            Debug.Log("Prefab компонент: " + comp.GetType());

        TMP_Text txt = symbol.GetComponent<TMP_Text>();
        if (txt == null)
        {
            Debug.LogError("TMP_Text is NULL! Є компоненти: " + symbol.GetComponents<Component>().Length);
            return;
        }

        if (activeSymbols.Count >= maxSymbols)
            return;

        int idx = Random.Range(0, visibleChars.Length);
        txt.text = visibleChars[idx];

        Rect rect = area.rect;
        symbol.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            Random.Range(rect.xMin, rect.xMax),
            Random.Range(rect.yMin, rect.yMax)
        );

        var move = symbol.AddComponent<MoveSymbol>();
        move.speed = Random.Range(minSpeed, maxSpeed) / 100f;
        move.area = area;

        activeSymbols.Add(symbol);
    }


}
