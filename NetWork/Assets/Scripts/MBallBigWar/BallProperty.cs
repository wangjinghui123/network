using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace WJH
{
    public class BallProperty : MonoBehaviour
    {
        public float cellSplitSpeed = 4f;
        public GameObject splitBallFood;
        public Vector3 direction = Vector3.zero;//移动方向

        private Vector2 _position = Vector2.zero;
        private float _speed = 10f;
        private Vector3 _scale = Vector3.one;
        private float _playerMass = 0;//玩家mass
        private RectTransform ballRectTransform;//玩家自身坐标
        private float addMassValue = 2.0f;//进行缩放计算
        private move moveCtrl;//脚本获取移动方向
        private Sprite splitFoodTexture;
        private Tweener tweener = null;//dotweener用于实例化食物的初始移动动画;
        private Cells cellsFather;


        private PlayersManager playerManager;
        private RectTransform leftUpPoint;
        private RectTransform rightDownPoint;

        private float alltime = .5f;
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
            cellsFather = transform.parent.GetComponent<Cells >();
            ballRectTransform = GetComponent<RectTransform>();
            gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<Cells>()._cellSprite;
            _scale = ballRectTransform.localScale;
            playerManager = transform.parent.parent.GetComponent<PlayersManager>();

            rightDownPoint = playerManager.GetComponent<PlayersManager>().RightDownPoint;
            leftUpPoint = playerManager.GetComponent<PlayersManager>().LeftUpPoint;
            moveCtrl = this.GetComponent<move>();
            splitFoodTexture = transform.parent.GetComponent<Cells>().splitFoodSprite;
            playerManager.ballList.Add(this);


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
            if (Input.GetKeyDown(KeyCode.W))
            {
                BallSplit();
                //分身
            }
            if (moveCtrl.dir != Vector3.zero)
            {
                direction = moveCtrl.dir;
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
                //  Debug.Log(dir );
                _speed = (1 / ballRectTransform.localScale.x * 50);
                float radius = ballRectTransform.rect.width / 2.0f * ballRectTransform.localScale.x;
                if (ballRectTransform.position.x <= rightDownPoint.position.x - radius)
                {
                    // Debug.Log(rightDownPoint.position.x);
                    transform.Translate(dir * Time.deltaTime * _speed);
                }
                else
                {
                    ballRectTransform.position = new Vector3(rightDownPoint.position.x - radius, ballRectTransform.position.y, ballRectTransform.position.z);

                }
                if (ballRectTransform.position.x >= leftUpPoint.position.x + radius)
                {
                    transform.Translate(dir * Time.deltaTime * _speed);
                }
                else
                {
                    ballRectTransform.position = new Vector3(leftUpPoint.position.x + radius, ballRectTransform.position.y, ballRectTransform.position.z);
                }
                if (ballRectTransform.position.y <= leftUpPoint.position.y - radius)
                {
                    transform.Translate(dir * Time.deltaTime * _speed);
                }
                else
                {
                    ballRectTransform.position = new Vector3(ballRectTransform.position.x, leftUpPoint.position.y - radius, ballRectTransform.position.z);
                }
                if (ballRectTransform.position.y >= rightDownPoint.position.y + radius)
                {
                    transform.Translate(dir * Time.deltaTime * _speed);

                }
                else
                {
                    ballRectTransform.position = new Vector3(ballRectTransform.position.x, rightDownPoint.position.y + radius, ballRectTransform.position.z);
                }
            }
        }

        /// <summary>
        /// 球球分身
        /// </summary>
        public void BallSplit()
        {
            if (_scale.x < 2)
            {
                return;
            }
            // 分开  弹射 
            GameObject otherCell = InstantiateObj(direction, gameObject, ballRectTransform, transform.parent);
            transform.parent.GetComponent<Cells>().cells.Add(otherCell);
            _scale = _scale / 2;
            // ballRectTransform.localScale = _scale;
            ballRectTransform.DOScale(_scale, .8f);
            otherCell.transform.localScale = _scale;

           // _playerMass /= 2;
            addMassValue /= 2;
            //合并
        }

        /// <summary>
        /// 球球吞食食物 增加质量 增加Scale
        /// </summary>
        /// <param name="增加质量值"></param>
        /// <param name="增加质量值"></param>
        public void BallDevourFood(float mass, float addvalue)
        {
          // _playerMass += mass;
            cellsFather.playerMass += mass;
            float y = Mathf.Log(addMassValue, 3f);
            _scale = new Vector3(y, y, y);
            if (_scale.x <= 1)
            {
                addMassValue += addvalue;
            }
            else
            {
                //ballRectTransform.localScale = _scale;
                ballRectTransform.DOScale(_scale, .8f);
                addMassValue++;
            }

            //  Debug.Log("=====================================" + _playerMass + "===============================");
        }
        /// <summary>
        /// 球球吐球
        /// </summary>
        public void BallSplitOutBall()
        {
            if (_scale.x < 1.5f)
            {
                return;
            }
            else
            {
                float splitMass = Random.Range(50, 100);
                GameObject obj = InstantiateObj(direction, splitBallFood, ballRectTransform, playerManager.FoodManagerRect.transform);
                obj.GetComponent<Image>().sprite = splitFoodTexture;
                // _playerMass -= splitMass;
                cellsFather.playerMass -= splitMass;
                addMassValue--;
                float scalex = Mathf.Log(addMassValue, 3f);
                _scale = new Vector3(scalex, scalex, scalex);
                // ballRectTransform.localScale = _scale;
                ballRectTransform.DOScale(_scale, .8f);
            }
        }

        /// <summary>
        /// 实例化物体在方向前方
        /// </summary>
        /// <param name="SpawnDir"></param>
        /// <param name="prefab"></param>
        /// <param name="selfRectTransform"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject InstantiateObj(Vector3 SpawnDir, GameObject prefab, RectTransform selfRectTransform, Transform parent)
        {
            Vector3 pos = selfRectTransform.position + SpawnDir * selfRectTransform.rect.width * selfRectTransform.localScale.x;
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity, parent);
            SetMoveEdge(leftUpPoint, rightDownPoint, obj.GetComponent<RectTransform>(), direction);
            return obj;
        }

        public void SetMoveEdge(RectTransform leftUpPoint, RectTransform rightDownPoint, RectTransform selfTransform, Vector3 moveDir)
        {
            tweener = selfTransform.DOMove(moveDir * 100 + selfTransform.position + (selfTransform.rect.width / 2.0f) * Vector3.one, 1.5f).OnUpdate(() =>
            {
                float radious = selfTransform.rect.width / 2.0f;
                if (selfTransform.position.x >= rightDownPoint.position.x - radious)
                {
                    tweener.SetAutoKill(false);
                    selfTransform.position = new Vector3(rightDownPoint.position.x - radious, selfTransform.position.y, selfTransform.position.z);

                }
                if (selfTransform.position.x <= leftUpPoint.position.x + radious)
                {
                    tweener.SetAutoKill(false);
                    selfTransform.position = new Vector3(leftUpPoint.position.x + radious, selfTransform.position.y, selfTransform.position.z);
                }
                if (selfTransform.position.y >= leftUpPoint.position.y - radious)
                {
                    tweener.SetAutoKill(false);
                    selfTransform.position = new Vector3(selfTransform.position.x, leftUpPoint.position.y - radious, selfTransform.position.z);
                }
                if (selfTransform.position.y <= rightDownPoint.position.y + radious)
                {
                    tweener.SetAutoKill(false);
                    selfTransform.position = new Vector3(selfTransform.position.x, rightDownPoint.position.y + radious, selfTransform.position.z);
                }

            });

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            //如果碰到玩家
            if (other.gameObject.tag == "Player")
            {
                //检测是是否覆盖自己
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float radius = ballRectTransform.rect.width * ballRectTransform.localScale.x / 2.0f;
                //如果距离非常近
                if (distance < radius / 2.0f)
                {

                }
                //如果已经重合
                else if (distance < radius)
                {
                    //如果是自己
                    if (transform.parent.gameObject == other.transform.parent.gameObject && transform.localScale.x > other.transform.localScale.x)
                    {
                        Destroy(other.gameObject);
                    }
                    //如果是敌人  自己的Scale大于敌人的
                    else if (transform.parent.gameObject != other.transform.parent.gameObject && transform.localScale.x > other.transform.localScale.x)
                    {
                        //比较敌人与自己的Scale大小
                        BallDevourFood(other.transform .parent .GetComponent <Cells >().playerMass , 10);
                        Destroy(other.gameObject);
                    }
                }

                // Debug.Log("碰到自己");
            }


        }




    }
}

