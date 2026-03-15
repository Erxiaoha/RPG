using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 目标是玩家从楼梯上去的时候关闭山脉控制器
public class Elevation_Entry : MonoBehaviour
{
    public Collider2D[] mountainColliers;
    public Collider2D[] boundaryColliers;

    private void OnTriggerEnter2D(Collider2D collision) //玩家经过这个触发器的时候 里面的collision实际上是过来触碰它的碰撞体，比如玩家走路到了这个碰撞器，这里面的参数就是玩家本身的碰撞体。
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliers)
            {
                mountain.enabled = false;
            }

            foreach (Collider2D boundary in boundaryColliers)
            {
                boundary.enabled = true;
            }
            // 这里是玩家的
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
