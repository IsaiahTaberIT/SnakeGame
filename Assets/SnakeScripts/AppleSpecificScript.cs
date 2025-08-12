using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AppleSpecificScript : MonoBehaviour
{
    public GameObject Self;
    public TMP_InputField InputValue;
    public WallGenerationScript WallGenerationScript;

    void Start()
    {
        WallGenerationScript = GameObject.Find("WallGenerator").GetComponent<WallGenerationScript>();
    }
    public void FixAppleCount()
    {
        int number;
        if (int.TryParse(InputValue.text, out number))
        {
            if (number > WallGenerationScript.MaxReasonableApples)
            {
                InputValue.text = "" + WallGenerationScript.MaxReasonableApples;
            }
        }
    }


   
}
