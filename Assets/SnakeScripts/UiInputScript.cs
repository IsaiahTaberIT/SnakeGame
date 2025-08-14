using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UiInputScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Output;
    public TMP_InputField InputValue;
    public GameObject Logic;
    public GameLogicScript GameLogicScript;
    public GameObject FoodManager;
    public FoodManagmentScript FoodManagmentScript;
    public WallGenerationScript WallGenerationScript;
    public Vector2 Bounds;
    public const int MinumumSize = 5;

    void Start()
    {
       

        WallGenerationScript = GameObject.Find("WallGenerator").GetComponent<WallGenerationScript>();


        Bounds.x = WallGenerationScript.WallLength;
        Bounds.y = WallGenerationScript.WallWidth;



        FoodManager = GameObject.Find("FoodManager");
        FoodManagmentScript = FoodManager.GetComponent<FoodManagmentScript>();

        Logic = GameObject.FindGameObjectWithTag("Logic");
        GameLogicScript = Logic.GetComponent<GameLogicScript>();

        if(gameObject.name == "AppleCount" && string.IsNullOrEmpty(PlayerPrefs.GetString("Apples")) == false)
        {
            InputValue.text = PlayerPrefs.GetString("Apples");


        }
        if (gameObject.name == "WallLength")
        {
            InputValue.text = "" + PlayerPrefs.GetFloat("WallLength");
        }

       


        if (gameObject.name == "WallHeight")
        {
            InputValue.text = "" + PlayerPrefs.GetFloat("WallWidth");

        }



        





    }
    public void SaveAppleCount()
    {
        PlayerPrefs.SetString("Apples", InputValue.text);
    }

    public void RepairInvalidSetting()
    {
        if (string.IsNullOrEmpty(InputValue.text) == true)
        {

            InputValue.text = "1";
            Output.text = InputValue.text;
            
        }
    }
    public void RepairInvalidDimention()
    {
        int number;
        if (string.IsNullOrEmpty(InputValue.text) == true)
        {

            InputValue.text = "" + MinumumSize;
            Output.text = InputValue.text;

        }
        // This makes sure the value is even so that it doesnt break everythng if its odd because i dont feel like fixing that bug

        if (int.TryParse(InputValue.text, out number))
        {

            //if ((number % 2) == 1)
          //  {
          //      InputValue.text = "" + (number + 1);
          //  }

            if (number < MinumumSize)
            {
                InputValue.text = "" + MinumumSize;
            }


        }
        else
        {
            InputValue.text = "" + MinumumSize;

            WallGenerationScript.WallLength = MinumumSize;
        }
    }



    public void OutputApples()
    {
        int LocalMaxReasonableApples = WallGenerationScript.MaxReasonableApples;
        int number;


        if (int.TryParse(InputValue.text, out number))
        {

            if (string.IsNullOrEmpty(InputValue.text) == false)
            {
                if (number > LocalMaxReasonableApples)
                {
                    InputValue.text = "" + LocalMaxReasonableApples;
                    number = LocalMaxReasonableApples;
                }


                FoodManagmentScript.FoodCount = number;
            }

            


        }
        else
        {
            FoodManagmentScript.FoodCount = 1;
        }

        Output.text = InputValue.text;

    }





    void Update()
    {
        if (GameLogicScript.RequireNewGame && !GameLogicScript.HeadScript.GameOver)
        {
            gameObject.GetComponent<TMP_InputField>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<TMP_InputField>().interactable = true;
        }
    }
    public void OutputText()
    {
        Output.text = InputValue.text;
    }
    public void UpdateWallLength()
    {
        int number;



        if (string.IsNullOrEmpty(InputValue.text) == false && int.TryParse(InputValue.text, out number))
        {
            if (number >= 4)
            {
                WallGenerationScript.WallLength = int.Parse(InputValue.text);
            }
            else
            {
                WallGenerationScript.WallLength = MinumumSize;
            }
        }
        else
        {
            WallGenerationScript.WallLength = MinumumSize;
        }
    }

    public void UpdateWallWidth()
    {
        int number;



        if (string.IsNullOrEmpty(InputValue.text) == false && int.TryParse(InputValue.text, out number))
        {
            if (number >= 6)
            {
                WallGenerationScript.WallWidth = number;
            }
            else
            {
                WallGenerationScript.WallWidth = MinumumSize;
            }
        }
        else
        {
            WallGenerationScript.WallWidth = MinumumSize;
        }
    }
   
}
