using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    //Membervariables
    public GameObject pickUp;
    private int pickUpCount; // number of collected pickUps
    public GameObject piece; // body of the snake
    public GameObject lastPiece;
    public bool showHighscore;
    public bool gameover = false;
    private bool isMoving = false;
    public int speed = 1;
    public bool lookUp = false;
    public bool lookDown = false;

    //Methods
    // Use this for initialization
    void Start()
    {

        showHighscore = false;
        int i = 0;
        while (i < 25)
        {
            placePickUps();
            i++;
        }
        pickUpCount = 0;
        lastPiece = gameObject;
        Debug.Log(lastPiece.name);
        Debug.Log(piece.name);
        //if (PlayerPrefs.GetInt("pickUpCount") < 1)
        //{
        //    PlayerPrefs.SetInt("pickUpCount", 1);
        //}

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Application.LoadLevel(Application.loadedLevel);
        if (isMoving)
        {
            
            if (Input.GetKeyDown(KeyCode.UpArrow) && lookDown)
            {
                transform.Rotate(new Vector3(-90, 0, 0));
                lookDown = false;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow) && lookUp)
            {

                transform.Rotate(new Vector3(-90, 0, 0));
                transform.Rotate(new Vector3(0, 0, 180));
                lookUp = false;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                transform.Rotate(new Vector3(-90, 0, 0));
                lookUp = true;
            };

            if (Input.GetKeyDown(KeyCode.DownArrow) && lookUp)
            {
                transform.Rotate(new Vector3(90, 0, 0));
                lookUp = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && lookDown)
            {

                transform.Rotate(new Vector3(90, 0, 0));
                transform.Rotate(new Vector3(0, 0, 180));
                lookUp = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.Rotate(new Vector3(90, 0, 0));
                lookDown = true;
            };

            if (Input.GetKeyDown(KeyCode.LeftArrow) && lookUp)
            {
                transform.Rotate(new Vector3(90, 0, 0));
                transform.Rotate(new Vector3(0, -90, 0));
                lookUp = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && lookDown)
            {
                transform.Rotate(new Vector3(-90, 0, 0));
                transform.Rotate(new Vector3(0, -90, 0));
                lookDown = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, -90, 0));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && lookUp)
            {
                transform.Rotate(new Vector3(90, 0, 0));
                transform.Rotate(new Vector3(0, 90, 0));
                lookUp = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && lookDown)
            {
                transform.Rotate(new Vector3(-90, 0, 0));
                transform.Rotate(new Vector3(0, 90, 0));
                lookDown = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }
            
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
        }
        if (isMoving)
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

        }
        //if ((Time.time % 10) < 0.01)
        //{
        //    placePickUps();
        //}
    }


    void placePickUps()
    {
        Vector3 position = new Vector3(Random.Range(-29, 29), Random.Range(-29, 29), Random.Range(-29, 29));
        GameObject PickUp = Instantiate(pickUp, position, Quaternion.identity) as GameObject;
        PickUp.name = "pickUp";
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "pickUp")
        {
            Debug.Log("Collision with pick up detected");
            Destroy(col.gameObject);
            pickUpCount++;
            addPiece();
            //placePickUps();


        }
        if (col.gameObject.name == "Piece" || col.gameObject.name == "Wall")
        {

            Debug.Log("Collision with wall or piece detected");
            //Application.LoadLevel(Application.loadedLevel);
            gameover = true;
            isMoving = false;
        }
    }
    void addPiece()
    {
        Vector3 pos = transform.position - (GetComponent<Rigidbody>().velocity * 500);
        GameObject newPiece = (GameObject)Instantiate(piece, pos, Quaternion.identity);
        newPiece.name = "Piece";
        Debug.Log("Last piece is:" + lastPiece);
        newPiece.GetComponent<SmoothFollow>().target = lastPiece.transform;
        lastPiece = newPiece;


    }
    void OnGUI()
    {
        if (!isMoving)
        {

            GUILayout.Label("Press Space to Start");

        }
        if (!showHighscore && GUILayout.Button("Show Highscore"))
        {
            showHighscore = true;
        }
        else if (showHighscore && GUILayout.Button("Hide Highscore"))
        {
            showHighscore = false;
        }

        if (showHighscore)
        {
            GUILayout.Label("Pick Ups: " + PlayerPrefs.GetInt("pickUp"));
        }
        if (gameover)
        {
            GUI.Label(new Rect(200, 200, Screen.width / 2, Screen.height / 2), "Game Over!");

            GUI.Label(new Rect(200, 250, 200, 550), "Press Enter to re-start the game");
        }
    }

}
