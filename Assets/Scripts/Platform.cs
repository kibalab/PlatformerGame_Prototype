using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public SpriteRenderer[] Sprites;

    private bool m_isPreview = false;

    public float duration = 5.0f;

    public bool IsPreview
    {
        set
        {
            if (value)
            {
                gameObject.layer = 0;
                var coliders = GetComponentsInChildren<BoxCollider2D>();
                SetColor(Color.gray);

                foreach (var colider in coliders)
                {
                    
                    colider.gameObject.layer = 0;
                    
                    colider.isTrigger = true;
                }
            }
            else
            {
                gameObject.layer = 6;
                var coliders = GetComponentsInChildren<BoxCollider2D>();
                SetColor(Color.white);

                foreach (var colider in coliders)
                {
                    
                    colider.gameObject.layer = 6;
                    
                    colider.isTrigger = false;
                }
            }

            m_isPreview = value;
        }

        get => m_isPreview;
    }

    private void Update()
    {
        if (!m_isPreview) duration -= Time.deltaTime;

        SetColor(Color.white * duration / 5 + new Color(0,0,0,1 ));
        
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color)
    {
        var coliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var sprite in Sprites)
        {
            sprite.color = color;
        }
    }
}
