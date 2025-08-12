using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FoodScript : MonoBehaviour
{

    public Vector2 Bounds;
    public GameObject WallGenerator;
    public WallGenerationScript WallGenerationScript;
    
    public List<Vector2> ListOfEmptySquares;
    public int OddEvenFixerIntX;
    public int OddEvenFixerIntY;
     



    // Start is called before the first frame update
    void Start()
    {

        WallGenerator = GameObject.Find("WallGenerator");
        WallGenerationScript = WallGenerator.GetComponent<WallGenerationScript>();

        Bounds.x = WallGenerationScript.WallLength;
        Bounds.y = WallGenerationScript.WallWidth;

        
        MoveApple();
    }

    // Update is called once per frame

    public void MoveApple()
    {
        OddEvenFixerIntX = (Bounds.x % 2 == 0) ? 1 : 2;
        OddEvenFixerIntY = (Bounds.y % 2 == 0) ? 1 : 2;
        AttemptStochasticPlacement();
        


        





        bool overlap = false;
        
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x == transform.position.x && GameObject.FindGameObjectWithTag("Player").transform.position.y == transform.position.y)
        {
            overlap = true;
        }
        

        if (overlap == false)
        {
            foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Food"))
            {
                if (Obj != gameObject)
                {
                    if (Obj.transform.position.x == transform.position.x && Obj.transform.position.y == transform.position.y)
                    {
                        overlap = true;
                        break;
                    }
                }




            }
        }


        if (overlap == false)
        {
            foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Body"))
            {
                if (Obj.transform.position.x == transform.position.x && Obj.transform.position.y == transform.position.y)
                {
                    overlap = true;
                    break;
                }

            }
        }

        ListOfEmptySquares.Clear();
        if (overlap == true)
        {
            //Debug.Log("Stochastic Placement Failed");
            ListOfEmptySquares.Clear();

            for (int i = 0; i < Bounds.x - 2; i++)
            {
                for (int j = 0; j < Bounds.y - 2; j++)
                {
                    //  Debug.Log(i +","+ j);
                    // WallGenerationScript.IsSnakePosition[0, 0] = false;
                     // Debug.Log(i + "," + j);
                    //Debug.Log("WallGenerationScript.IsSnakePosition[i, j]");

                    if (WallGenerationScript.IsSnakePosition[i, j] == false)
                    {
                        ListOfEmptySquares.Add(new Vector2(i, j));
                    }
                }
            }
            //Debug.Log(ListOfEmptySquares.Count);
            if (ListOfEmptySquares.Count != 0)
            {
                int RanIndex = Random.Range(0, ListOfEmptySquares.Count);
                Vector2 NewFoodPosition = new Vector2(Mathf.FloorToInt((ListOfEmptySquares[RanIndex].x - (Bounds.x) / 2f) + OddEvenFixerIntX), Mathf.FloorToInt((ListOfEmptySquares[RanIndex].y - (Bounds.y) / 2f) + OddEvenFixerIntY));
                transform.position = new Vector3(NewFoodPosition.x, NewFoodPosition.y, transform.position.z);
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Food").Length == 1)
                {
                    GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>().ActivateWeiner();
                }

                Destroy(gameObject);

            }

        }
        //  bool test = WallGenerationScript.IsSnakePosition[0, 0];


        int x = (int)gameObject.transform.position.x + Mathf.FloorToInt(Bounds.x / 2f) - 1;
        int y = (int)gameObject.transform.position.y + Mathf.FloorToInt((Bounds.y / 2f) - 1);


        if (WallGenerationScript.IsSnakePosition != null)
        {
            //Debug.Log(x + " " + y);
            WallGenerationScript.IsSnakePosition[x, y] = true;
        }
        else
        {
            Debug.Log("is Null");
        }



    }
    

    public void AttemptStochasticPlacement()
    {
        Vector2 RandomPos;
        int MinumumX;
        int MinimumY;
        int MaximumX;
        int MaximumY;
        MinumumX = (Mathf.FloorToInt(Bounds.x / 2) * -1 + OddEvenFixerIntX);
        MaximumX = Mathf.FloorToInt(Bounds.x / 2) - OddEvenFixerIntX;
        MinimumY = Mathf.FloorToInt(Bounds.y / 2) * -1 + OddEvenFixerIntY;
        MaximumY = Mathf.FloorToInt(Bounds.y / 2) - OddEvenFixerIntY;
        





        RandomPos.x = Random.Range(MinumumX, MaximumX);
        RandomPos.y = Random.Range(MinimumY, MaximumY);
        //Debug.Log( "" + RandomPos.x + "," + RandomPos.y);
        transform.position = new Vector3(RandomPos.x, RandomPos.y, transform.position.z);

    }
}
