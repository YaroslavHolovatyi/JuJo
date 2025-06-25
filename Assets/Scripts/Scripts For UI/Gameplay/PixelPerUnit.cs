using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AdjustPixelsPerUnit : MonoBehaviour
{
    public float targetPPU = 100f;

    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr.sprite != null)
        {
            var tex = sr.sprite.texture;
            var rect = sr.sprite.rect;
            sr.sprite = Sprite.Create(tex, new Rect(rect.x, rect.y, rect.width, rect.height),
                                      new Vector2(0.5f, 0.5f), targetPPU);
        }
    }
}
