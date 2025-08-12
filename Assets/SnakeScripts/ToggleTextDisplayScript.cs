using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class ToggleTextDisplayScript : MonoBehaviour
{
    public bool IsOn;
    public TextMeshPro Text;
    // Start is called before the first frame update
 
   
    // Start is called before the first frame update
    void Start()
    {
        string text;
        Text = GetComponent<TextMeshPro>();
        IsOn = (PlayerPrefs.GetInt("SpeedUpMode") == 1) ? true : false;

        text = (IsOn) ? "On" : "Off" ;
        Text.text = "SpeedUp \n Mode: " + text;











    }

    // Update is called once per frame

    public void SwitchOnOff()
    {
        string text;
        int intbool = IsOn ? 1 : 0;
        if (IsOn)
        {
            IsOn = false;
            text = "Off";
        }
        else
        {
            IsOn = true;
            text = "On";
        }


        PlayerPrefs.GetInt("SpeedUpMode", intbool);

        Text.text = "SpeedUp \n Mode: " + text;




    }
}
