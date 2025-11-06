using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color DefaultColor { get; private set; }

    [SerializeField] private Color[] _colors;
    [SerializeField] private SpriteRenderer _fillSpriteRender;

    public void SetDefaultColor(Color color)
    {
        DefaultColor = color;
        SetColor(color);
    }

    public void SetColor(Color color)
    {
        _fillSpriteRender.color = color;
    }
    public void SetRandomColor()
    {
        int randomNum = Random.Range(0, _colors.Length);
        DefaultColor = _colors[randomNum];
        _fillSpriteRender.color = DefaultColor;
    }
}
