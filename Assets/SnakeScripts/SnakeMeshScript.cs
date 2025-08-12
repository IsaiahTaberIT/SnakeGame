using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshFilter))]

public class SnakeMeshScript : MonoBehaviour
{
    public List<Vector3> ListOffSnakePositions;



    Vector3[] Verticies;
    int[] Triangles;
    Vector2[] Uvs;
    Color[] Colors;
    public Gradient MyGradient;



    public Transform SelfTransform;
    private Mesh MyMesh;
    public float Speed = 1f;
    public float T = 0f;
    public int MinimumNoiseScale;
    public int Octaves;
    public float RunningOctaveStrengthTotal;


    public float Perlin = 100;

    public float Exponents = 100;

    public float Height = 256;
    public float HeightScaleRate = 2f;
    public float NoiseScaleRate = 2f;
    public float ExponentialAdditiveScaleRate = 0;
    public float ExponentialHeightScale = 0;
    public GameObject Head;
    public HeadScript HeadScript;
    

    public Vector3 Pos;
    public Vector2Int GridSize = new Vector2Int(20, 20);


    private Vector3 DeltaPos = Vector3.zero;


    
  


    // Start is called before the first frame update
    void Start()
    {

        
        SelfTransform = GetComponent<Transform>();
        Pos = SelfTransform.position;
        Pos = new Vector3(Pos.x / (GridSize.x), 0, Pos.z / (GridSize.y));

        MyMesh = new Mesh();


       
        //GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;


        if (ListOffSnakePositions.Count > 10)
        {
            GetComponent<MeshCollider>().sharedMesh = MyMesh;
            GetComponent<MeshFilter>().mesh = MyMesh;
        }



    }

    void Update()
    {

        Pos = SelfTransform.position;
        Pos = new Vector3(Pos.x / (GridSize.x), 0, Pos.z / (GridSize.y));


      









        if (ListOffSnakePositions.Count > 0)
        {
            for (int i = 0 ; i < HeadScript.MasterLength + 3; i++)
            {
                if (ListOffSnakePositions.Count > HeadScript.MasterLength *2f)
                {
                    ListOffSnakePositions.RemoveAt(i);
                }
            }


            Verticies = new Vector3[ListOffSnakePositions.Count * 4];
            int j = 0;
            foreach (Vector3 SnakePosition in ListOffSnakePositions)
            {
                float SizeReduction = Mathf.InverseLerp(-10, HeadScript.MasterLength, Mathf.RoundToInt(j/4));
                SizeReduction = 1;

                Vector3 Zpos = new Vector3(0, 0, 0.4f);

                Verticies[j] = SnakePosition + new Vector3(-0.5f,-0.5f,0) * SizeReduction +  Zpos;
                Verticies[j + 1] = SnakePosition + new Vector3(-0.5f, 0.5f, 0) * SizeReduction + Zpos;
                Verticies[j + 2] = SnakePosition + new Vector3(0.5f, -0.5f, 0) * SizeReduction + Zpos;
                Verticies[j + 3] = SnakePosition + new Vector3(0.5f, 0.5f, 0) * SizeReduction + Zpos;
             //   Verticies[j + 4] 
             //   Verticies[j + 5]
                j += 4;

            }
            CreateShape();
            UpdateMesh();
            GetComponent<MeshCollider>().sharedMesh = MyMesh;
            GetComponent<MeshFilter>().mesh = MyMesh;
        }

    }


    public void CreateShape()
    {
        
       


        
     



      //  for (int i = 0, Y = 0; Y <= GridSize.y; Y++)
      //  {
        //    for (int X = 0; X <= GridSize.x; X++)
         //   {

                

             //       Verticies[i] = new Vector3(X, Y, 1);



            //        i++;
                
        //    }
      //  }




        Triangles = new int[ListOffSnakePositions.Count * 6];

//
      //  int Vert = 0;
        int Tris = 0;


        for (int Vert = 0; Vert < ListOffSnakePositions.Count * 4 ; Vert += 4)
        {
           

            Triangles[Tris] = Vert;
            Triangles[Tris + 1] = Vert + 1;
            Triangles[Tris + 2] = Vert + 2;
            Triangles[Tris + 3] = Vert + 3;
            Triangles[Tris + 4] = Vert + 2;
            Triangles[Tris + 5] = Vert + 1;
            
            Tris += 6;
            
        }
        /*
        for (int Vert = 2; Vert < ListOffSnakePositions.Count * 4 -2; Vert += 4)
        {
            //13
            //02

            //31
            //20


            Triangles[Tris] = Vert + 0;
            Triangles[Tris + 1] = Vert + 3;
            Triangles[Tris + 2] = Vert + 2;
            Triangles[Tris + 3] = Vert + 3;
            Triangles[Tris + 4] = Vert + 0;
            Triangles[Tris + 5] = Vert + 1;

            Tris += 6;

        }

        */


        //   for (int Z = 0; Z < GridSize.y; Z++)
        //   {
        //  for (int X = 0; X < GridSize.x; X++)
        //  {
        //      Triangles[Tris + 0] = Vert + 0;
        //      Triangles[Tris + 1] = Vert + GridSize.x + 1;
        //     Triangles[Tris + 2] = Vert + 1;
        //     Triangles[Tris + 3] = Vert + 1;
        //     Triangles[Tris + 4] = Vert + GridSize.x + 1;
        //     Triangles[Tris + 5] = Vert + GridSize.x + 2;

        //       Vert++;
        //       Tris += 6;
        //   }

        //     Vert++;
        // }

        //  Colors = new Color[Verticies.Length];

        // for (int i = 0, Z = 0; Z <= GridSize.y; Z++)
        // {
        //     for (int X = 0; X <= GridSize.x; X++)
        //     {




        //        Colors[i] = Color.red; 
        //         i++;
        //      }
        //  }



        /*

        Uvs = new Vector2[Verticies.Length];

        for (int i = 0, Z = 0; Z <= GridSize.y; Z++)
        {
            for (int X = 0; X <= GridSize.x; X++)
            {
                Uvs[i] = new Vector2((float)X / GridSize.x, (float)Z / GridSize.y);

                i++;
            }
        }

        */












        /*
        Verticies = new Vector3[]
        {
            new Vector3 (0,0,0),
            new Vector3 (0,0,1),
            new Vector3 (1,0,0),
            new Vector3 (1,0,1)
        };

        Triangles = new int[]
        {
            0 , 1 , 2,
            1 , 3 , 2
        };
        */
    }

    public void UpdateMesh()
    {



        MyMesh.Clear();

        MyMesh.vertices = Verticies;
        MyMesh.triangles = Triangles;

        MyMesh.RecalculateNormals();
        //MyMesh.uv = Uvs;
        MyMesh.colors = Colors;
    }
    private void OnDrawGizmos()
    {
        if (Verticies == null)
        {
            return;
        }
        for (int i = 0; i < Verticies.Length; i++)
        {
            //   Gizmos.DrawSphere(Verticies[i], 0.1f);

        }
    }
    public void AddPosition()
    {

        ListOffSnakePositions.Add(Head.transform.position);
       
    }



    // Update is called once per frame

}


