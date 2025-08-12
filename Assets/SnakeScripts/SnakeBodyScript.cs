using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyScript : MonoBehaviour
{
    public Vector2 Bounds;
    public GameObject WallGenerator;
    public WallGenerationScript WallGenerationScript;
    
    public GameObject Head;
    public HeadScript HeadScript;
    public int length;
    public int StartingLength;
    public SpriteRenderer SelfSprite;
    public int InitialColorRangeOffset;
    public Vector2 Direction;
    public Vector2 OldDirection;
    public Sprite TurningTexture;
    public Sprite TurningTextureDark;
    public Sprite StraightTexture;
    public Sprite TailTexture;
    // Start is called before the first frame update
    void Start()
    {
        SelfSprite = GetComponent<SpriteRenderer>();
        WallGenerator = GameObject.Find("WallGenerator");
        Bounds.x = WallGenerator.GetComponent<WallGenerationScript>().WallLength;
        Bounds.y = WallGenerator.GetComponent<WallGenerationScript>().WallWidth;
        
        Head = GameObject.Find("SnakeHead");
        length = Head.GetComponent<HeadScript>().MasterLength + 1;
        Direction = Head.GetComponent<HeadScript>().Direction;
        OldDirection = Head.GetComponent<HeadScript>().OldDirection;
        StartingLength = length;


        if (Head.GetComponent<HeadScript>().HasMoved == true)
        {

            if (OldDirection != Direction)
            {
                if ((Direction == Vector2.left && OldDirection == Vector2.up))
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);

                    SelfSprite.sprite = TurningTexture;

                }
                if (Direction == Vector2.down && OldDirection == Vector2.right)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    SelfSprite.sprite = TurningTextureDark;
                }



                if ((Direction == Vector2.right && OldDirection == Vector2.up))
                {
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                    SelfSprite.sprite = TurningTextureDark;

                }
                if (Direction == Vector2.down && OldDirection == Vector2.left)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                    SelfSprite.sprite = TurningTexture;
                }



                if ((Direction == Vector2.left && OldDirection == Vector2.down))
                {
                    transform.localEulerAngles = new Vector3(0, 0, -90);
                    SelfSprite.sprite = TurningTextureDark;

                }
                if (Direction == Vector2.up && OldDirection == Vector2.right)
                {
                    transform.localEulerAngles = new Vector3(0, 0, -90);
                    SelfSprite.sprite = TurningTexture;
                }



                if ((Direction == Vector2.right && OldDirection == Vector2.down))
                {
                    transform.localEulerAngles = new Vector3(0, 0, 180);
                    SelfSprite.sprite = TurningTexture;


                }
                if (Direction == Vector2.up && OldDirection == Vector2.left)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 180);
                    SelfSprite.sprite = TurningTextureDark;

                }





            }
            else
            {
                if (Direction.y != 0)
                {
                    transform.localEulerAngles = new Vector3(0, 0, -90);

                }


                if (Direction.y == -1)
                {
                    SelfSprite.flipY = true;
                }

                if (Direction.x == 1)
                {
                    SelfSprite.flipY = true;
                }



            }
        }
        else
        {

            SelfSprite.flipY = false;
            SelfSprite.sprite = TailTexture;

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
        }


        byte newred;
        byte newgreen;
        byte newblue;

        newred = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length), 2) * 80 + 20);
        newblue = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length), 2) * 80 + 20);
        newgreen = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length), 2) * 160 + 95);






        SelfSprite.color = new Color32(newred, newgreen, newblue, 255);









        Head.GetComponent<HeadScript>().HasMoved = true;

    }

    // Update is called once per frame
    void Update()
    {
      


        byte newred;
        byte newgreen;
        byte newblue;

        newred = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length),2) * 80 + 20);
        newblue = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length),2) * 80 + 20);
        newgreen = (byte)(Mathf.Pow(Mathf.InverseLerp(-InitialColorRangeOffset, StartingLength, length),2) * 160 + 95);






        SelfSprite.color = new Color32(newred, newgreen, newblue, 255);






        if (Head.GetComponent<HeadScript>().Move)
        {
            if (length <= 0)
            {
                if (transform.position.x > Mathf.Round(-Bounds.x / 2) && transform.position.y > Mathf.Round(-Bounds.y / 2) && transform.position.x <= Mathf.Round(Bounds.x / 2) - 1 && transform.position.y <= Mathf.Round(Bounds.y / 2) - 1)
                {
                    bool up;
                    bool down;
                    bool right;
                    bool left;

                    float upfloat;
                    float downfloat;
                    float rightfloat;
                    float leftfloat;

                    leftfloat = Bounds.x / 2;
                    downfloat = (-Bounds.y / 2);
                    rightfloat = (Bounds.x / 2);
                    upfloat = (Bounds.y / 2);

                    left = (transform.position.x > (-Bounds.x / 2));
                    down = (transform.position.y > (-Bounds.y / 2));
                    right = (transform.position.x <= (Bounds.x / 2));
                    up = transform.position.y <= (Bounds.y / 2);

                //    Debug.Log("UP:" + up + ",DOWN:" + down + ",LEFT:" +  left + ",RIGHT:" + right);
                //    Debug.Log("UP:" + upfloat + ",DOWN:" + downfloat + ",LEFT:" + leftfloat + ",RIGHT:" + rightfloat);


                  
                        WallGenerator.GetComponent<WallGenerationScript>().IsSnakePosition[(int)(transform.position.x) + (int)Mathf.FloorToInt(Bounds.x / 2f) - 1, (int)transform.position.y + (int)Mathf.FloorToInt((Bounds.y / 2f) - 1)] = false;

                    



                }





                Destroy(gameObject);

            }
            length--;
            if (length == 0)
            {
                SelfSprite.flipY = false;
                SelfSprite.sprite = TailTexture;

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
            }

        }
       
    }
}
