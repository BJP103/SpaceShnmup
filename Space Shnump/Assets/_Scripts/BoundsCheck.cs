using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoundsCheck;

public class BoundsCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0,
        offRight =1,
        offLeft =2,
        offUp = 4,
        offDown = 8
    }
    /// <summary>
    /// Keeps a game object on screen
    /// Note that this only works for an orthographic main camera
    /// </summary>
    
    public enum eType { center, inset, outset}

    [Header("Inscribed")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    //public bool isOnScreen = true;
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
        if(boundsType == eType.inset) checkRadius = -radius;
        if(boundsType == eType.outset) checkRadius = radius;
        Vector3 pos = transform.position;
        //isOnScreen = true;
        screenLocs = eScreenLocs.onScreen;

        if (pos.x > camWidth + checkRadius){
            pos.x = camWidth + checkRadius;
            //isOnScreen = false;
            screenLocs |= eScreenLocs.offRight;

        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            //isOnScreen = false;
            screenLocs |= eScreenLocs.offLeft;

        }
        if (pos.y > camWidth + checkRadius)
        {
            pos.y = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offUp;
            //isOnScreen = false;
        }
        if (pos.y < -camWidth - checkRadius)
        {
            pos.y = -camWidth - checkRadius;
            //isOnScreen = false;
            screenLocs |= eScreenLocs.offDown;
        }
        /*
        if (pos.x < -camWidth){ 
            pos.x = -camWidth;
        }
        if (pos.y > camHeight){ 
            pos.y = camHeight;
        }
        if (pos.y < -camHeight){ 
            pos.y = -camHeight;
        }*/
        if (keepOnScreen && !isOnScreen) {
            transform.position = pos;
            screenLocs |= eScreenLocs.onScreen;
            //isOnScreen = true;
        }
        
    }
    public bool isOnScreen
    {
        get { return (screenLocs == eScreenLocs.onScreen); }
    }
    public bool Locls(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return((screenLocs & checkLoc)== checkLoc);
    }
}
