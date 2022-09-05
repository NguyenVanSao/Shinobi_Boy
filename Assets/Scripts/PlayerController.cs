using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _colli;

    [Header("Config")]
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _rayLength;
    [SerializeField] bool isGround;
    [SerializeField] Animator _animControl;
    

    [Header("Watching")]
    [SerializeField] Vector2 movement;
    [SerializeField] float _anglePlatform;

    public playerState currentSTATE => PLAY_STATE;
    [SerializeField] playerState PLAY_STATE = playerState.Idle;
    
    public enum playerState
    {
        Idle = 1,
        Run = 2,
        Jump = 3,
        Land = 4,
    }

    [SerializeField] List<PhysicsMaterial2D> _friction = new List<PhysicsMaterial2D>();



    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _colli = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
        updateState();
        updateAnim();

    }

    private void FixedUpdate()
    {
        Moving();

    }


    void Moving()
    {
        float dir = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;

        Vector3 tmp = this.transform.localScale;
        if(dir>0)
        {
            tmp.x = 1;
        }
        else if(dir<0)
        {
            tmp.x = -1;
        }
        this.transform.localScale = tmp;


        movement.x = Mathf.Cos((_anglePlatform) * Mathf.Deg2Rad) * dir;

        if(checkSlope())
        {
            movement.y = Mathf.Sin((_anglePlatform) * Mathf.Deg2Rad) * dir;
        }
        else
        {
            movement.y = _rb.velocity.y;
        }

        _rb.velocity = movement;

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Debug.LogError("call here");
            isGround = false;
            _rb.AddForce(new Vector2(0, _jumpForce));
        }

        if(checkSlope())
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                _rb.sharedMaterial = _friction[1];
            }
            else
            {
                _rb.sharedMaterial = _friction[0];
            }
            return;
        }

        _rb.sharedMaterial = _friction[1];

    }



    bool checkSlope()
    {
        if (!isGround)
        {
            _anglePlatform = 0;
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, Mathf.Infinity);
        if (hit.collider == null)
        {
            _anglePlatform = 0;
            return false;
        }

        _anglePlatform = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90;

        if (_anglePlatform < 0.1)
        {
            _anglePlatform = 0; 
            return false;
        }

        return true;
    }

    void checkGround()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        _colli.Cast(Vector2.down, hits, _rayLength);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }

            isGround = true;
            return;
        }

        isGround = false;

    }

    void updateState()
    {
        if (isGround)
        {
            if (_rb.velocity.x != 0)
                PLAY_STATE = playerState.Run;
            else
                PLAY_STATE = playerState.Idle;
        } 
        else
        {
            if (_rb.velocity.y > 0)
                PLAY_STATE = playerState.Jump;
            else if (_rb.velocity.y < 0)
                PLAY_STATE = playerState.Land;
        }
        
    }

    void updateAnim()
    {
        for(int i=0; i<=(int)playerState.Land; i++)
        {
            playerState tmp = (playerState)i;
            if (tmp != PLAY_STATE)
                _animControl.SetBool(tmp.ToString(), false);
            else
                _animControl.SetBool(tmp.ToString(), true);
                
        }
    }


}