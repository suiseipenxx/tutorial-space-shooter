using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;//關聯爆炸特效預製物件
    public GameObject playerExplosion;//關聯飛機爆炸預製物件
    public int scoreValues;//爆炸時要增加的分數
    private GameController gameController;//關聯GameController組件

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");//找尋整個場景中tag為GameController的遊戲物件
        gameController = gameControllerObject.GetComponent<GameController>();//從該遊戲物件中關聯掛在身上的GameController組件
    }

    private void OnTriggerEnter(Collider other)//當有任意碰撞器進入此觸發器會自動被呼叫此方法
    {
        if (other.CompareTag("Boundary"))//如果進入的tag為Boundary則忽略
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);//產生爆炸特效到當前物件所在的座標上
        if (other.CompareTag("Player"))//如果進入的tag為Player
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);//產生飛機爆炸特效到當前物件所在的座標上
            gameController.GameOver();//呼叫GameController的GameOver方法，通知遊戲結束了
        }
        gameController.AddScore(scoreValues);//呼叫GameController的AddScore方法，增加分數
        Destroy(other.gameObject);//刪除進入的碰撞器的遊戲物件
        Destroy(gameObject);//刪除自己
    }
}
