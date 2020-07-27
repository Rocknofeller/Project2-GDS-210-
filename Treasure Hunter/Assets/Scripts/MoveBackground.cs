using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    private Transform centerBackground;
   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= centerBackground.position.y + 5.76f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y + 5.76f);
        }
        else if (transform.position.y <= centerBackground.position.y - 5.76f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y - 5.76f);
        }
    }
}
