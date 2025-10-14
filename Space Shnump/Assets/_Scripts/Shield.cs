using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotationsPerSecound = 0.1f;
    [Header("Dynamic")]
    public int levelShown = 0;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        int currlevel = Mathf.FloorToInt(Hero.S.shiedldLevel);
        if (levelShown != currlevel) { 
            levelShown = currlevel;
            mat.mainTextureOffset = new Vector2(0f * levelShown, 0);
        }
        float rZ = -(rotationsPerSecound * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0,0,rZ);

    }
}
