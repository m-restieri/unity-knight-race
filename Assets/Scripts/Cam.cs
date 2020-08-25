using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera cam;

    public Color color1;

    public Color color2;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float pos = 0;
        if (transform.position.x <= 100)
        {
            pos = transform.position.x / 100;
            cam.backgroundColor = Color.Lerp(color1, color2, pos);
        }
        else if (transform.position.x > 100 && transform.position.x <= 1000)
        {
            pos = transform.position.x / 1000;
            cam.backgroundColor = Color.Lerp(color2, color1, pos);
        }
        else if (transform.position.x > 1000 && transform.position.x <= 10000)
        {
            pos = transform.position.x / 10000;
            cam.backgroundColor = Color.Lerp(color1, color2, pos);
        }
        else
        {
            pos = 1;
            cam.backgroundColor = Color.Lerp(color2, color1, pos);
        }
     

    }
}
