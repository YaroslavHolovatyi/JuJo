using UnityEngine;

public class MoveSymbol : MonoBehaviour
{
    public RectTransform area;
    public float speed;
    Vector2 direction;

    void Start()
    {
        // ¬ипадковий напр€мок руху
        float angle = Random.Range(0, 2 * Mathf.PI);
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void Update()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += direction * speed;

        // якщо вийшов за меж≥ Ц повернути на ≥нший б≥к
        Rect r = area.rect;
        Vector2 pos = rectTransform.anchoredPosition;
        if (pos.x < r.xMin) pos.x = r.xMax;
        if (pos.x > r.xMax) pos.x = r.xMin;
        if (pos.y < r.yMin) pos.y = r.yMax;
        if (pos.y > r.yMax) pos.y = r.yMin;
        rectTransform.anchoredPosition = pos;
    }
}
