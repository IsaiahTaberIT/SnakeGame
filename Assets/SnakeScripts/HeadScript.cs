using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public float WaitFloor = 0.05f;
    public float WaitReductionFactor = 0.95f;
    public GameObject Logic;
    public GameObject WallGenerator;
    public WallGenerationScript WallGenerationScript;
    public GameLogicScript GameLogicScript;
    public Vector2 Bounds;
    public GameObject Food;
    public Vector2 Direction;
    public Vector2 QueuedDirection;
    public GameObject SnakeBody;
    public GameObject Self;
    public float InitialWait;
    public float Wait = 0.25f;
    public float Timer = 0;
    public bool GameOver = false;
    public int MasterLength = 10;
    public bool Move;
    public Vector2 OldDirection = Vector2.zero;
    public List<Vector2> DirectionQueue;
    public bool HasMoved = false;
    public SnakeMeshScript SnakeMeshScript;


    // Start is called before the first frame update
    void Start()
    {
       // SnakeMeshScript = GameObject.Find("MeshGenerator").GetComponent<SnakeMeshScript>();




        Logic = GameObject.FindGameObjectWithTag("Logic");
        InitialWait = Wait;
        WallGenerator = GameObject.Find("WallGenerator");
        GameLogicScript = Logic.GetComponent<GameLogicScript>();
        Bounds.x = WallGenerator.GetComponent<WallGenerationScript>().WallLength;
        Bounds.y = WallGenerator.GetComponent<WallGenerationScript>().WallWidth;
        WallGenerationScript = WallGenerator.GetComponent<WallGenerationScript>();

        if (PlayerPrefs.GetFloat("SavedWait") > 0)
        {
            Wait = PlayerPrefs.GetFloat("SavedWait");
        }

       // SnakeMeshScript.AddPosition();

    }

    // Update is called once per frame
    void Update()
    {

        if (Direction == Vector2.up)
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);
        }
        else if (Direction == Vector2.down)
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);

        }
        else if (Direction == Vector2.left)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);

        }
        else if (Direction == Vector2.right)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);

        }



        Bounds.x = WallGenerationScript.WallLength;
        Bounds.y = WallGenerationScript.WallWidth;


        Timer += Time.deltaTime;
       
        Move = false;

        

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (DirectionQueue.Count < 2)
            {
                if (DirectionQueue.Count >= 1)
                {
                    if (DirectionQueue[0].y == 0)
                    {
                        DirectionQueue.Add(Vector2.up);
                    }
                }
                else if (Direction.y == 0)
                {
                    DirectionQueue.Add(Vector2.up);
                }
            }



        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (DirectionQueue.Count < 2)
            {
                if (DirectionQueue.Count >= 1)
                {
                    if (DirectionQueue[0].y == 0)
                    {
                        DirectionQueue.Add(Vector2.down);
                    }
                }
                else if (Direction.y == 0)
                {
                    DirectionQueue.Add(Vector2.down);
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            if (DirectionQueue.Count < 2)
            {
                if (DirectionQueue.Count >= 1)
                {
                    if (DirectionQueue[0].x == 0)
                    {
                        DirectionQueue.Add(Vector2.left);
                    }
                }
                else if (Direction.x == 0)
                {
                    DirectionQueue.Add(Vector2.left);
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (DirectionQueue.Count < 2)
            {
                if (DirectionQueue.Count >= 1)
                {
                    if (DirectionQueue[0].x == 0)
                    {
                        DirectionQueue.Add(Vector2.right);
                    }
                }
                else if (Direction.x == 0)
                {
                    DirectionQueue.Add(Vector2.right);
                }
            }


        }

        // if (DirectionQueue.Count == 2)
        // {
        //     Timer = Wait;


        // }


        if (Timer >= Wait && GameOver == false && GameLogicScript.AnyActiveMenu == false)
        {

            OldDirection = Direction;

            if (HasMoved == true)
            {
                GameLogicScript.RequireNewGame = true;

            }
            Vector3 OldPosition = transform.position;

            if (DirectionQueue.Count != 0)
            {
               
                Direction = DirectionQueue[0];
                DirectionQueue.RemoveAt(0);
            }

            

            
            



            if (Direction != Vector2.zero)
            {
                if (transform.position.x > Mathf.FloorToInt(-Bounds.x / 2) && transform.position.y > Mathf.FloorToInt(-Bounds.y / 2)  -1 && transform.position.x <= Mathf.FloorToInt(Bounds.x / 2) - 1 && transform.position.y <= Mathf.FloorToInt(Bounds.y / 2) - 1)
                {
                    WallGenerationScript.IsSnakePosition[(int)transform.position.x + Mathf.FloorToInt(Bounds.x / 2) - 1, (int)transform.position.y + Mathf.FloorToInt(Bounds.y / 2) - 1] = true;
                }


                Instantiate(SnakeBody, transform.position, new Quaternion(0, 0, 0, 0));
                transform.position += new Vector3(Direction.x, Direction.y, 0);
                //SnakeMeshScript.AddPosition();


                Move = true;
                // FoodScript
               


            }
            Timer = 0;
            if (QueuedDirection != OldDirection && QueuedDirection != Vector2.zero)
            {
                Direction = QueuedDirection;

            }
        }

        if (GameLogicScript.ActiveMenu == true)
        {
            DirectionQueue.Clear();
        }

    }
    private void FixedUpdate()
    {
        
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.CompareTag("Body") || collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("GameOver");
            GameOver = true;
        }
        if (collision.gameObject.CompareTag("Food"))
        {
            if (GameLogicScript.SpeedUpMode && Wait > WaitFloor)
            {
                
                Wait *= WaitReductionFactor;
                if (Wait < WaitFloor)
                {
                    Wait = WaitFloor;
                }
            }
            ExpandBody(); 

            collision.gameObject.GetComponent<FoodScript>().MoveApple();


            if (GameLogicScript.AnyActiveMenu == false && HasMoved == true)
            {
                GameLogicScript.ScoreFunction();
                GameLogicScript.CountApplesEaten();
                MasterLength++;

            }

        }
    }
    public void ExpandBody()
    {
        GameObject[] SnakePartList = GameObject.FindGameObjectsWithTag("Body");
        foreach (GameObject G in SnakePartList)
        {
            G.GetComponent<SnakeBodyScript>().length += 1;
        }


    }





}
