using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rules : MonoBehaviour
{
    Text rules;
    // Start is called before the first frame update
    void Start()
    {
        rules = GetComponent<Text>();
        rules.text = "Swipe Up to Jump" + "\n" + "Swipe Down to Slash Red Enemies for 100 more Points";
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
