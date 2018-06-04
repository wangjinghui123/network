using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private float thornMass = 50;
    private Vector3 _thornScale;
    private ThornManager thornManager;
    private void Start()
    {
        _thornScale = this.transform.localScale;
        thornManager = ThornManager.Instance;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&&other.transform .parent .GetComponent <Cells >().isInvincible )
        {

            if (_thornScale.x < other.transform.localScale.x)
            {
                //thornManager.isSpawnThorn = true;
                //分身 体重增加 刺球消失 随机生成新的刺球
                other.GetComponent<BallProperty>().BallSplit();
                other.GetComponent<BallProperty>().BallDevourFood(thornMass, 2);

                Destroy(gameObject);
            }
        }
    }
}
