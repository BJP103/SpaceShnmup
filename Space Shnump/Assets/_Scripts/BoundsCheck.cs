using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    /// <summary>
    /// Keeps a game object on screen
    /// Note that this only works for an orthographic main camera
    /// </summary>
    
    public enum eType { center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;

    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        float checkRadius =  0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if(boundsType == eType.outset) checkRadius = -radius;
        Vector3 pos = transform.position;

        if(pos.x > camWidth + checkRadius){
            pos.x = camWidth + checkRadius;
        }
        if (pos.x < -camWidth){ 
            pos.x = -camWidth;
        }
        if (pos.y > camHeight){ 
            pos.y = camHeight;
        }
        if (pos.y < -camHeight){ 
            pos.y = -camHeight;
        }
        transform.position = pos;
    }
}
