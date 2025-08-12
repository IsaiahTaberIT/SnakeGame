using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagmentScript : MonoBehaviour
{
    public int FoodCount =1;
    public GameObject Food;
    public GameObject WallGenerator;
    public WallGenerationScript WallGenerationScript;

    // Start is called before the first frame update
    void Start()
    {
        WallGenerator = GameObject.Find("WallGenerator");
        WallGenerationScript = WallGenerator.GetComponent<WallGenerationScript>();

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Apples")) == false)
        {
            FoodCount = int.Parse(PlayerPrefs.GetString("Apples"));
        }



        
        KillAllFood();
        SpawnFood();
    }

    public void KillAllFood()
    {
        WallGenerationScript.GenerateNewWall();
        foreach (GameObject Food in GameObject.FindGameObjectsWithTag("Food"))
        {
            Destroy(Food);
        }
    }

    public void SpawnFood()
    {
        for (int i = 0; i < FoodCount; i++)
        {
            Instantiate(Food, transform);
 
        }
    }
}
