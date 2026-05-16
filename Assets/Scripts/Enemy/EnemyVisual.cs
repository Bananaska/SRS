using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Sprite _sprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    }
    public void ChangeSprite(Sprite spriteToSet)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = spriteToSet;
        }
    }
}
