using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallProperty : MonoBehaviour
{
    public float cellSplitSpeed = 4f;
    public GameObject splitBallFood;

    private Vector2 _position = Vector2.zero;
    private Vector2 _eulerAngles = Vector2.zero;
    private float _speed = 10f;
    private Vector3 _scale = Vector3.one;
    private float _playerMass = 0;
    private RectTransform ballRectTransform;//玩家自身坐标
    private float addMassValue = 2.0f;
   
    public  Vector3 direction;//移动方向
  
   
    private Rigidbody2D rigidbody2D;
    private PlayersManager   playerManager;
    private RectTransform leftUoPoint;
    private RectTransform rightDownPoint;

    private float alltime=.5f;
    private float curtime = 0;

    public Vector2 position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            if (gameObject != null)
            {
                gameObject.transform.position = _position;
            }
        }
    }


    public float playerMass
    {
        get
        {
            return _playerMass;
        }
        set
        {
            _playerMass = value;
        }
    }

    public Vector2 scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = value;
            if (gameObject != null)
            {
                //   gameObject.transform.localScale = _scale;
                _scale = ballRectTransform.localScale;
            }
        }
    }
    public float speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }


    private void Start()
    {
      
        direction = GetComponent<move>().dir;
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        ballRectTransform = this.GetComponent<RectTransform>();
        gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<Cells>().cellSprite;
        _scale = ballRectTransform.localScale;
        playerManager = transform.parent.parent.GetComponent<PlayersManager  >();
        leftUoPoint = playerManager.GetComponent<PlayersManager>().LeftUpPoint;
        rightDownPoint = playerManager.GetComponent<PlayersManager>().RightDownPoint ;


    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            //吐球 
           

            curtime += Time.deltaTime;
            if (curtime >= alltime)
            {
                BallSplitOutBall();
                curtime = 0;
            }
        }
        if (Input .GetKeyDown (KeyCode .W ))
        {
            BallSplit();
            //分身
           
           
        }

    }


   

    /// <summary>
    /// 球球移动
    /// </summary>
    /// <param name="移动方向"></param>
    public void BallMove(Vector3 dir)
    {
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            //  _speed = Mathf.Clamp((1 / ballRectTransform.localScale.x * 50),10,100);
            _speed = (1 / ballRectTransform.localScale.x * 50);
            float radius = ballRectTransform.rect.width / 2.0f * ballRectTransform.localScale.x;
            if (ballRectTransform.position.x <= rightDownPoint.position.x - radius )
            {
               // rigidbody2D.velocity = dir * _speed;
                transform.Translate(dir * Time.deltaTime * _speed);
            }
            else
            {
                ballRectTransform.position = new Vector3(rightDownPoint.position.x - radius,ballRectTransform .position .y,ballRectTransform .position .z );

            }
            if (ballRectTransform.position.x >= leftUoPoint.position.x + radius)
            {
                transform.Translate(dir * Time.deltaTime * _speed);
            }
            else
            {
                ballRectTransform.position = new Vector3(leftUoPoint.position.x + radius, ballRectTransform.position.y, ballRectTransform.position.z);
            }
            if (ballRectTransform.position.y <= leftUoPoint.position.y - radius)
            {
                transform.Translate(dir * Time.deltaTime * _speed);
            }
            else
            {
                ballRectTransform.position = new Vector3(ballRectTransform .position .x , leftUoPoint.position.y - radius, ballRectTransform.position.z);
            }
            if (ballRectTransform.position.y >= rightDownPoint.position.y + radius)
            {
                transform.Translate(dir * Time.deltaTime * _speed);

            }
            else
            {
                ballRectTransform.position = new Vector3(ballRectTransform.position.x, rightDownPoint .position.y + radius, ballRectTransform.position.z);
            }
            
        }
    }

    /// <summary>
    /// 球球分身
    /// </summary>
    public void BallSplit()
    {
        if (_scale.x <2)
        {
            return;
        }
        // 分开  弹射 
        GameObject otherCell = Instantiate(gameObject ,transform .parent );
        _scale = _scale / 2;
        ballRectTransform.localScale = _scale;
        otherCell.transform.localScale = ballRectTransform.localScale;
        // otherCell.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y ) * cellSplitSpeed);
        otherCell.GetComponent<Rigidbody2D>().velocity = new Vector2(direction .x,direction .y)*cellSplitSpeed ;
        _playerMass /= 2;
        addMassValue /= 2;
        //靠近

        //紧靠

        //合并
    }

    /// <summary>
    /// 球球吞食食物 增加质量 增加Scale
    /// </summary>
    /// <param name="增加质量值"></param>
    public void BallDevourFood(float mass)
    {
        _playerMass += mass;
        float y = Mathf.Log(addMassValue   , 3f);
        _scale = new Vector3(y, y, y);
        if (_scale.x <= 1)
        {
            addMassValue++;
        }
        else
        {
            ballRectTransform.localScale = _scale;
            addMassValue++;
        }

        //  Debug.Log("=====================================" + _playerMass + "===============================");
    }


    /// <summary>
    /// 球球吐球
    /// </summary>
    public void BallSplitOutBall()
    {
        if (_scale.x < 2)
        {
            return;
        }
        else
        {
            Vector3 objPos = ballRectTransform.position;
            // Vector3 objPos = transform.position + direction*ballRectTransform .rect .width *scale .x ;
            GameObject obj = Instantiate(splitBallFood, objPos, Quaternion .identity , playerManager .FoodManagerRect.transform );
            Vector2 v2 = new Vector2(direction .x,direction .y);
            obj.GetComponent<Rigidbody2D>().velocity = v2 * cellSplitSpeed;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D other )
    {
        
    }
}
