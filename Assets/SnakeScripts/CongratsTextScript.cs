using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CongratsTextScript : MonoBehaviour
{
    public GameLogicScript GameLogicScript;
    public TextMeshPro Text;
    
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshPro>();
        GameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();

    }


    void OnEnable()
    {
        Text = GetComponent<TextMeshPro>();
        GameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();

        GameLogicScript.Score *= 1.1f;

        Text.SetText("Congratulations, Winner Is YOU! \n Score: " +string.Format("{0:N0}", GameLogicScript.Score) + "\n HighScore: " + string.Format("{0:N0}", GameLogicScript.HighScore));
        if (GameLogicScript.Score > GameLogicScript.HighScore)
        {
            PlayerPrefs.SetFloat("HIghScore", GameLogicScript.HighScore);
        }
        GameLogicScript.ActiveCongratsMenu = true;

    }
    void Update()
    {
        Text.SetText("Congratulations, Winner Is YOU! \n Score: " + string.Format("{0:N0}", GameLogicScript.Score) + "\n HighScore: " + string.Format("{0:N0}", GameLogicScript.HighScore));

    }
    public void InformGameLogicOfMenuClosing()
    {
        GameLogicScript.ActiveCongratsMenu = false;
        
    }
    private void OnDisable()
    {
        GameLogicScript.Restart();
    }
}
