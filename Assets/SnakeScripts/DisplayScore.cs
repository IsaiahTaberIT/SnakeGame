using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using TMPro;
public class DisplayScore : MonoBehaviour
{
    public TextMeshPro Text;
    public GameLogicScript GameLogicScript;
    // Start is called before the first frame update
    void Start()
    {
        GameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
        Text = gameObject.GetComponent<TextMeshPro>();


        if (gameObject.name == ("HighScore"))
        {
            Text.SetText("HighScore: " + string.Format("{0:N0}", GameLogicScript.HighScore));
                    
        }

        else if (gameObject.name == ("Score"))
        {
            Text.SetText("Score: " + string.Format("{0:N0}", GameLogicScript.Score));

        }
        else if (gameObject.name == ("ApplesEaten"))
        {
            Text.text = "Apples Eaten: " + GameLogicScript.ApplesEaten;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == ("HighScore"))
        {
            Text.SetText("HighScore: " + string.Format("{0:N0}", GameLogicScript.HighScore));

        }
        else if (gameObject.name == ("Score"))
        {
            Text.SetText("Score: " + string.Format("{0:N0}", GameLogicScript.Score));

        }
        else if (gameObject.name == ("ApplesEaten"))
        {
            Text.text = "Apples Eaten: " + GameLogicScript.ApplesEaten;

        }
    }
}
