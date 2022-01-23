using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider2D))]
public class PlayerControl : MonoBehaviour//, IInteractable
{
    //Component<Rigidbody2D>();
    Rigidbody2D RB;
    public GameObject Manager;
    GameManager GM;

    public GameObject Normal;
    public GameObject Inverted;
    public GameObject GroundPoint;

    public float MaxSpeed = 8;
    public float JumpHeight = 5;
    public bool OnGround = true;

    public List<GameObject> Interactables;

    void SpeedCheck()
    {
        RB.velocity = new Vector2 (Mathf.Clamp(RB.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(RB.velocity.y   , -MaxSpeed, MaxSpeed));
        
    }

    void LeftRight()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            RB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MaxSpeed, RB.velocity.y);
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                Normal.transform.eulerAngles = new Vector3(0, 0, 0);
                Inverted.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                Normal.transform.eulerAngles = new Vector3(0, 180, 0);
                Inverted.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }
        SpeedCheck();
    }
    void CheckGround()
    {
        int TempLayer;
        if (Physics2D.OverlapPoint(GroundPoint.transform.position))
        {
            TempLayer = Physics2D.OverlapPoint(GroundPoint.transform.position).gameObject.layer;
            if (TempLayer == 8 || TempLayer == 9)
            {
                OnGround = true;
            }
        }
        else
        {
            OnGround = false;
        }

    }
    void Jump()
    {
        if (Input.GetAxisRaw("Jump") != 0 && OnGround)
        {
            //RB.AddRelativeForce(new Vector2(0, Input.GetAxisRaw("Jump") * JumpHeight));
            RB.velocity += new Vector2(0, Input.GetAxisRaw("Jump") * JumpHeight);
        }
        else
        {
            //RB.velocity = new Vector2(RB.velocity.x, 0);
        }
        
        SpeedCheck();
    }

    void Invert()             //Inversion
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GM.Inverted)
            {
                GM.Inverted = false;
                Inverted.SetActive(false);
                Normal.SetActive(true);
            }
            else
            {
                GM.Inverted = true;
                Inverted.SetActive(true);
                Normal.SetActive(false);
            }
            GM.InvertWorlds();

        }
    }
    void Interact()
    {
        //Physics2D.OverlapCollider(gameObject, null, Interactables  );
        if (Physics2D.OverlapPoint(gameObject.transform.position, 10))
        {
            //enable text or sth indicator
            if (Input.GetKeyDown(KeyCode.F))
            {
                //interact

            }

        }
    }

    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        GM = Manager.GetComponent<GameManager>();
        
    }

    void Update()
    {
        Interact();
        LeftRight();
        CheckGround();
        Jump();
        Invert();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)
        {

            case "Collectible":
                //GameManager add 1 score
                break;

            case "Enemy":

                break;




        }

        Destroy(collision.gameObject);

    }


}
