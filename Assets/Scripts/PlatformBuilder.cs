using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformBuilder : MonoBehaviour
{
    public int SelectIndex = 0;
    public GameObject[] Platforms;

    public Platform PreviewPlatform;

    private void Start()
    {
        PreviewPlatform = Instantiate(Platforms[SelectIndex]).GetComponent<Platform>();
        PreviewPlatform.IsPreview = true;
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos = new Vector2(
            Mathf.Round(pos.x * 4) / 4,
            Mathf.Round(pos.y * 4) / 4
            );

        PreviewPlatform.transform.position = (Vector3)pos;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var newPlatform = Instantiate(Platforms[SelectIndex]).GetComponent<Platform>();
            newPlatform.IsPreview = false;
            newPlatform.transform.position = (Vector3)pos;
        }
        
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.NameToLayer("Platform"));

        foreach (var hit in hits)
        {
            if(hit.collider != null && !hit.collider.gameObject.Equals(PreviewPlatform.gameObject))
            {
                Transform objectHit = hit.transform;

                var platform = objectHit.GetComponent<Platform>();
            
                Debug.Log(objectHit.gameObject.name);
                if (platform)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Destroy(platform.gameObject);
                    }
                }
            }
        }
        
        
    }
}
