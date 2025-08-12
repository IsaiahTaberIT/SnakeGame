using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{
    public GameObject Logic;
    public GameLogicScript GameLogicScript;



    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic");
        GameLogicScript = Logic.GetComponent<GameLogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogicScript.RequireNewGame)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;

        }
    }
}
