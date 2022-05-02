using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum LemmingState
    {
        IDLE,
        STOP,
        FALLING,
        WALK_LEFT,
        WALK_RIGHT
    }

public class LemmingStateMachine : MonoBehaviour
{
    [Header("Ground Sensor")]
    [SerializeField] Transform _groundSensor;
    [SerializeField] Vector2 _groundSensorSize;
    [SerializeField] LayerMask _groundSensorLayer;
    private bool _isFalling = true;

   


    [Header("Movement")]
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;
    private LemmingState _currentState;
    private bool _walkingRight = false;
    private SpriteRenderer sprite;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        sprite = GetComponent<SpriteRenderer>();
        TransitionToState(LemmingState.IDLE);
    }
    private void Update()
    {
        GroundDector();
        StateUpdate();

    }


    public void StateEnter()
    {
        switch (_currentState)
        {
            case LemmingState.IDLE:
                sprite.color = Color.blue;

                break;
            case LemmingState.STOP:
                sprite.color = Color.gray;
                rb.velocity = new Vector2(0, 0);
                break;
            case LemmingState.FALLING:
                sprite.color = Color.red;
                break;
            case LemmingState.WALK_LEFT:
                sprite.color = Color.cyan;
                _walkingRight = false;
                rb.velocity = new Vector2(0, 0);


                break;
            case LemmingState.WALK_RIGHT:
                sprite.color = Color.green;
                _walkingRight = true;
                rb.velocity = new Vector2(0, 0);


                break;
            default:
                break;
        }
    }
    public void StateUpdate()
    {
        switch (_currentState)
        {
            case LemmingState.IDLE:
                if (_isFalling)
                {
                    TransitionToState(LemmingState.FALLING);
                }
                break;
            case LemmingState.STOP:
                break;
            case LemmingState.FALLING:
                if (!_isFalling)
                {
                    RandomizeWalkDirection();  
                }
                break;
            case LemmingState.WALK_LEFT:
                if (_isFalling) { TransitionToState(LemmingState.FALLING); }
                Walking(_walkingRight);
                

                break;
            case LemmingState.WALK_RIGHT:
                if (_isFalling) { TransitionToState(LemmingState.FALLING); }
                Walking(_walkingRight);
                

                break;
            default:
                break;
        }
    }
    public void StateExit()
    {

    }

    public void TransitionToState(LemmingState newState)
    {
        StateExit();
        _currentState = newState;
        StateEnter();
    }
    void GroundDector()
    {
        Collider2D groundCheck = Physics2D.OverlapBox(_groundSensor.position, _groundSensorSize, 0, _groundSensorLayer);
        _isFalling = groundCheck == null;
    }

    
    void Walking(bool walkingDirection)
    {

        float direction;
        if (walkingDirection)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        rb.AddForce(speed * Time.deltaTime * Vector2.right * direction);
    }

    void RandomizeWalkDirection()
    {
        float coinToss = Random.Range(-10, 10);
        if (coinToss > 0)
        {
            TransitionToState(LemmingState.WALK_LEFT);
        }
        else
        {
            TransitionToState(LemmingState.WALK_RIGHT);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Lemming") && _currentState != LemmingState.STOP))
        {
            GoTheOppositeDirection();
        }
    }


    void GoTheOppositeDirection()
    {
        if (_currentState == LemmingState.WALK_LEFT)
        {
            TransitionToState(LemmingState.WALK_RIGHT);
        }
        else
        {
            TransitionToState(LemmingState.WALK_LEFT);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_groundSensor.position, _groundSensorSize);
    }
}

