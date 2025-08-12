using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class CaseTextDisplayScript : MonoBehaviour
{
    public HeadScript HeadScript;
    public int SpeedSetting;
    public TextMeshPro Text;
    // Start is called before the first frame update


    // Start is called before the first frame update
    void Start()
    {
        

        HeadScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeadScript>();
        Text = GetComponent<TextMeshPro>();



        SpeedSetting = PlayerPrefs.GetInt("SpeedSetting") - 1;

       // SpeedSetting = SpeedSetting == 0 ? 2 : SpeedSetting;

        if (SpeedSetting < 0)
        {
            Debug.Log("SetTo2");

            SpeedSetting = 2;
        }

        Debug.Log("start");
            SwitchSpeed();
    }

    // Update is called once per frame
    


    public void SwitchSpeed()
    {
        SpeedSetting++;




        if (SpeedSetting == 6)
        {
            SpeedSetting = 1;
        }

        PlayerPrefs.SetInt("SpeedSetting", SpeedSetting);

        string OutputString = "OutOfRange";

        switch (SpeedSetting)
        {
            case 1:
                OutputString = "Slow";
                HeadScript.Wait = 0.5f;
                PlayerPrefs.SetFloat("SavedWait", 0.5f);
                break;
            case 2:
                OutputString = "Normal";
                HeadScript.Wait = 0.25f;
                PlayerPrefs.SetFloat("SavedWait", 0.25f);
                break;
            case 3:
                OutputString = "Fast";
                HeadScript.Wait = 0.15f;
                PlayerPrefs.SetFloat("SavedWait", 0.15f);
                break;
            case 4:
                OutputString = "Very Fast";
                HeadScript.Wait = 0.08f;
                PlayerPrefs.SetFloat("SavedWait", 0.08f);
                break;
            case 5:
                OutputString = "Insane";
                HeadScript.Wait = 0.018f;
                PlayerPrefs.SetFloat("SavedWait", 0.018f);
                break;
           
        }


        Text.text = "Speed: \n" + OutputString;

    }
}



