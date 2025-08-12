using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerationScript : MonoBehaviour
{
    public GameObject Head;
    public GameObject Wall;
    public float WallLength = 10f;
    public float WallWidth = 10f;
    public int MaxReasonableApples;
    public bool[,] IsSnakePosition;

    // Start is called before the first frame update
    void Start()
    {
        Head = GameObject.FindGameObjectWithTag("Player");
        if ((PlayerPrefs.GetFloat("WallWidth")) != 0)
        {
            WallWidth = PlayerPrefs.GetFloat("WallWidth");
        }

        if (PlayerPrefs.GetFloat("WallLength") != 0)
        {
            WallLength = PlayerPrefs.GetFloat("WallLength");
        }

        IsSnakePosition = new bool[(int)WallLength, (int)WallWidth];

        for (int i = 0; i < WallLength - 1; i++)
        {
            for (int j = 0; j < WallWidth - 1; j++)
            {
                IsSnakePosition[i, j] = false;
            }
        }
        
       

        MaxReasonableApples = Mathf.RoundToInt(Mathf.Sqrt(WallLength * WallWidth) * 2);
        
        GenerateNewWall();

        





    }

    // Update is called once per frame

    public void GenerateNewWall()
    {
        //   FoodScript.GenerateArray();

        PlayerPrefs.SetFloat("WallWidth", WallWidth);
        PlayerPrefs.SetFloat("WallLength", WallLength);


        IsSnakePosition = new bool[(int)WallLength -2, (int)WallWidth -2];


        for (int i = 0; i < WallLength - 2; i++)
        {
            for (int j = 0; j < WallWidth - 2; j++)
            {
                IsSnakePosition[i, j] = false;
            }
        }

        int x = (int)Head.transform.position.x + (int)Mathf.RoundToInt(WallLength / 2f) - 1;
        int y = (int)Head.transform.position.y + (int)Mathf.RoundToInt(WallWidth / 2f) - 1;

        IsSnakePosition[x, y] = true;

        MaxReasonableApples = Mathf.RoundToInt(Mathf.Sqrt(WallLength * WallWidth) * 2);

        foreach (GameObject Wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(Wall);
        }

        for (int i = 0; i < WallLength; i++)
        {
            Instantiate(Wall, new Vector3(transform.position.x + i - Mathf.FloorToInt(WallLength / 2f), transform.position.y - Mathf.FloorToInt(WallWidth / 2f)), new Quaternion(0, 0, 0, 0));
            Instantiate(Wall, new Vector3(transform.position.x + i - Mathf.FloorToInt(WallLength / 2f), transform.position.y + WallWidth - Mathf.FloorToInt(WallWidth / 2f) -1f), new Quaternion(0, 0, 0, 0));

        }


        for (int i = 0; i < WallWidth ; i++)
        {
            Instantiate(Wall, new Vector3(transform.position.x - Mathf.FloorToInt(WallLength / 2f), transform.position.y + i - Mathf.FloorToInt(WallWidth / 2f)), new Quaternion(0, 0, 0, 0));
            Instantiate(Wall, new Vector3(transform.position.x + WallLength - Mathf.FloorToInt(WallLength / 2f) -1 , transform.position.y + i - Mathf.FloorToInt(WallWidth / 2f)), new Quaternion(0, 0, 0, 0));

        }
    }



}
