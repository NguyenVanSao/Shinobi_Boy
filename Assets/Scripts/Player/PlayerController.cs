using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController> 
{ 
    Rigidbody2D _rb;
    Collider2D _colli;
    Collider2D _colliCrouched;

    [Header("Config")]
    [SerializeField] float _speed;
    [SerializeField] float _atkpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _rayLength;
    [SerializeField] bool _isGround;
    [SerializeField] bool _doubleJump;
    [SerializeField] float _timeCountDown;

    [SerializeField] AudioSource runSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] Animator _animControl;

    

    [Header("Watching")]
    [SerializeField] Vector2 movement;
    [SerializeField] float _anglePlatform;

    public playerState currentSTATE => PLAY_STATE;
    [SerializeField] playerState PLAY_STATE = playerState.Idle;

    public atkState AtkState = atkState.Attack_Idle;
    public getHitState GetHitState = getHitState.GetHit;
    public enum playerState
    {
        Idle = 1,
        Run = 2,
        Jump = 3,
        Land = 4,
        Attack = 5,
        Crouched = 6,
        Death = 7,  
        GetHit = 8,
    }

    public enum atkState
    {
        Attack_Idle = 0,
        Attack_Air = 1,
        Attack_Crouched = 2,
    }

    public enum getHitState
    {
        GetHit = 0,
        GetHit_Crouched = 1,
        GetHit_Air = 2
    }

    [SerializeField] List<PhysicsMaterial2D> _friction = new List<PhysicsMaterial2D>();

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _colli = this.GetComponent<CapsuleCollider2D>();
        _colliCrouched = this.GetComponent<BoxCollider2D>();
        transform.localScale = new Vector2(1, 1);
    }



    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "DeadBoy")
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            PLAY_STATE = playerState.Death;
            if (runSound.clip.name == "Run")
            {
                runSound.clip = gameOverSound;
                runSound.Play();
            }
            _timeCountDown -= Time.deltaTime;
            if(_timeCountDown < 0)
            {
                SceneManager.LoadScene(4);
            }
            return;
        }
        checkGround();
        updateState();
        Moving();

        //Check double jump
        if (_isGround && !Input.GetKey(KeyCode.Space))
        {
            _doubleJump = false;
        }

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

        if(PLAY_STATE == playerState.Attack || PLAY_STATE == playerState.Crouched)
        {
            movement.x = 0;
        }

        if(dir != 0)
        {
            if(!runSound.isPlaying)
            {
                runSound.Play();
            }
        }
        else
        {
            runSound.Stop();
        }
        _rb.velocity = movement;

        if (_isGround && !Input.GetKey(KeyCode.Space))
        {
            _doubleJump = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (checkSlope())
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

    void Jump()
    {
        if (_isGround || _doubleJump)
        {
            SoundManager.instant.PlaySound(Constant.Jump);
            _isGround = false;
            _rb.AddForce(new Vector2(0, _jumpForce));
            _doubleJump = !_doubleJump;
        }
    }

    bool checkSlope()
    {
        if (!_isGround)
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
        RaycastHit2D[] hits = new RaycastHit2D[20];
        if(_colli.enabled)
            _colli.Cast(Vector2.down, hits, _rayLength);
        else
            _colliCrouched.Cast(Vector2.down, hits, _rayLength);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            this.transform.SetParent(hit.collider.transform);
            _isGround = true;
            return;
        }
        this.transform.SetParent(null);
        _isGround = false;

    }
    void updateState()
    {
        if(PLAY_STATE == playerState.Death)
        {
            Debug.LogError("Death");
            return;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _colli.enabled = true;
            _colliCrouched.enabled = false;
            PLAY_STATE = playerState.Idle;
        }

        if (PLAY_STATE == playerState.Attack)
        {
            return;
        }

        if (PLAY_STATE == playerState.Crouched)
        {
            return;
        }

        if (PLAY_STATE == playerState.GetHit)
        {
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (_isGround && PLAY_STATE != playerState.Crouched)
            {
                _colli.enabled = false;
                _colliCrouched.enabled = true;

                PLAY_STATE = playerState.Crouched;
                return;
            }
        }
        
        if (_isGround)
        {
            if (_rb.velocity.x > 0.2 || _rb.velocity.x < -0.2)
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
   

    public void AttackStateChange()
    {
        if(_isGround)
        {
            if(_colli.enabled)
            {
                if (PLAY_STATE != playerState.Attack)
                {
                    PLAY_STATE = playerState.Attack;
                    AtkState = atkState.Attack_Idle;
                }
            }
            else
            {
                if (PLAY_STATE != playerState.Attack)
                {
                    PLAY_STATE = playerState.Attack;
                    AtkState = atkState.Attack_Crouched;
                }
            }
        }
        else
        {
            if (PLAY_STATE != playerState.Attack)
            {
                PLAY_STATE = playerState.Attack;
                AtkState = atkState.Attack_Air;
            }
        }
    }

    public void ChangeAnimGetHit()
    {
        if(PLAY_STATE != playerState.GetHit)
        {
            PLAY_STATE = playerState.GetHit;
            if(_isGround)
            {
                if(_colli.enabled)
                {
                    GetHitState = getHitState.GetHit;
                }
                else
                {
                    GetHitState = getHitState.GetHit_Crouched;
                }
            }
            else
            {
                GetHitState = getHitState.GetHit_Air;
            }
        }
    }

    public void ReturnIDle()
    {
        PLAY_STATE = playerState.Idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "NextLevel")
        {
            GameManager.instant.CheckScore();
        }
    }
}
