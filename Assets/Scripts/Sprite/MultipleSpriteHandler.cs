using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpriteHandler : MonoBehaviour
{
    [SerializeField]
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    public void GetSpritesInChildren()
    {
        sprites.Clear();
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprites.Add(sprite);
        }
    }

    public void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }
}
