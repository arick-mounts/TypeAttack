using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{

    public Toggle pause;

    void Start()
    {
        if (GameManager.isMusic)
            pause.isOn = true;
        else
            pause.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
