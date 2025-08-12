using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameLogicScript : MonoBehaviour
{
    public object[] ArrayOfArrays = new object[1];
    public bool CameraTrackingOn = false;
    public GameObject WinnerMenu;
    public bool RequireNewGame = false;
    public bool ActiveMenu = true;
    public bool SpeedUpMode = false;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public bool ActiveSettingsMenu = false;
    public bool ActiveCongratsMenu = false;
    public bool AnyActiveMenu = true;
    public HeadScript HeadScript;
    public WallGenerationScript WallGenerationScript;
    public FoodManagmentScript FoodManagmentScript;
    public Vector2 Bounds;
    public GameObject MainCamera;
    public GameObject Canvas;
    public float Score = 0;
    public float HighScore = 0;
    public float ApplesEaten = 0;
    public GameObject Head;
    // Start is called before the first frame update
    void Start()
    {
        ArrayOfArrays[0] = new int[1, 1, 1];
        HighScore = PlayerPrefs.GetFloat("HIghScore");

        FoodManagmentScript = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManagmentScript>();
        Head = GameObject.FindGameObjectWithTag("Player");
        HeadScript = Head.GetComponent<HeadScript>();
        WallGenerationScript = GameObject.Find("WallGenerator").GetComponent<WallGenerationScript>();

        Bounds.x = WallGenerationScript.WallLength;
        Bounds.y = WallGenerationScript.WallWidth;


        Application.targetFrameRate = 60;

        SpeedUpMode = (PlayerPrefs.GetInt("SpeedUpMode") == 1) ? true : false;


        RefreshGameplayScale();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveMenu == true || ActiveSettingsMenu == true || ActiveCongratsMenu)
        {
            AnyActiveMenu = true;
        }
        else
        {
            AnyActiveMenu = false;
        }


        //this code is for enableing and disabling the close range camera tracking of the snake
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (AnyActiveMenu == false)
            {
                CameraTrackingOn = CameraTrackingOn ? false : true;
                
            }
            if (CameraTrackingOn == false)
            {
                RefreshGameplayScale();
            }
        }

        if (AnyActiveMenu == true)
        {
            if (CameraTrackingOn)
            {
                CameraTrackingOn = false;
                RefreshGameplayScale();

            }
            

        }
        if (CameraTrackingOn)
        {
          //  Canvas.GetComponent<RectTransform>().localScale = new Vector3(1,1, Canvas.GetComponent<RectTransform>().localScale.z * 2)/2;
            MainCamera.GetComponent<Camera>().orthographicSize = 20;
            float NewX;
            float NewY;

            NewX = Mathf.Lerp(MainCamera.transform.position.x, Head.transform.position.x, Time.deltaTime * Mathf.Sqrt(Mathf.Abs(MainCamera.transform.position.x - Head.transform.position.x) ));
            NewY = Mathf.Lerp(MainCamera.transform.position.y, Head.transform.position.y, Time.deltaTime * Mathf.Sqrt(Mathf.Abs(MainCamera.transform.position.y - Head.transform.position.y) ));
            
            
            MainCamera.transform.position = new Vector3(NewX, NewY, MainCamera.transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (AnyActiveMenu == false)
            {
                Restart();

            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (ActiveSettingsMenu)
            {
                ToggleSettingsMenu();

            }
            else
            {
                ToggleMenu();

            }


        }











    }
    public void Restart()
    {
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RequireNewGame = false;
    }

    public void Quit()
    {
        PlayerPrefs.Save();
#if UNITY_EDITOR

        EditorApplication.isPlaying = false;

#endif

        Application.Quit();

    }

    public void ToggleSpeedup()
    {
        if (SpeedUpMode == false)
        {
            SpeedUpMode = true;
        }
        else
        {
            SpeedUpMode = false;
        }

        PlayerPrefs.SetInt("SpeedUpMode", (SpeedUpMode ? 1 : 0));


    }
    public void ToggleMenu()
    {
        SettingsMenu.SetActive(false);
        if (ActiveMenu)
        {
            ActiveMenu = false;
            MainMenu.SetActive(false);

        }
        else
        {
            ActiveMenu = true;
            MainMenu.SetActive(true);

        }
    }

    public void ToggleSettingsMenu()
    {
        ToggleMenu();

        if (ActiveSettingsMenu)
        {
            ActiveSettingsMenu = false;
            SettingsMenu.SetActive(false);

        }
        else
        {

            ActiveSettingsMenu = true;
            SettingsMenu.SetActive(true);

        }
    }

    public void RefreshGameplayScale()
    {
        Bounds.x = WallGenerationScript.WallLength;
        Bounds.y = WallGenerationScript.WallWidth;
        float OffsetX = (Bounds.x % 2 == 0) ? -0.5f : 0;
        float OffsetY = (Bounds.y % 2 == 0) ? -0.5f : 0;

        MainCamera.transform.localPosition = new Vector3(OffsetX, OffsetY, MainCamera.transform.localPosition.z);
        Canvas.transform.localPosition = new Vector3(OffsetX, OffsetY, Canvas.transform.localPosition.z);
        if (CameraTrackingOn == false)
        {
            MainCamera.GetComponent<Camera>().orthographicSize = Mathf.Clamp(Mathf.Max(Bounds.x / 1.75f + 1f, Bounds.y * 1.2f + 1f), 0, float.PositiveInfinity) / 1.5f;

        }

        Canvas.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Clamp(Mathf.Max(Bounds.x / 1.75f + 1f, Bounds.y *1.2f + 1f) , 0, float.MaxValue), Mathf.Clamp(Mathf.Max(Bounds.x /1.75f + 1f, Bounds.y * 1.2f + 1f) , 0, float.MaxValue), 0) / 60;
        Canvas.transform.localScale = new Vector3(Canvas.transform.localScale.x, Canvas.transform.localScale.y, 1);



    }

    public void ScoreFunction()
    {

        Score += 1 + (50 * Mathf.Sqrt(0.5f / HeadScript.Wait) / (FoodManagmentScript.FoodCount + 2)) * ((HeadScript.MasterLength / (Bounds.x * Bounds.y + 25)));

        if (HighScore < Score)
        {
            HighScore = Score;
        }

         PlayerPrefs.SetFloat("HIghScore", HighScore);
    }
    public void CountApplesEaten()
    {
        ApplesEaten++;
    }



    public void ActivateWeiner()
    {
        WinnerMenu.SetActive(true);
    }

    public void DeActivateWeiner()
    {
        WinnerMenu.SetActive(false);
    }

}
