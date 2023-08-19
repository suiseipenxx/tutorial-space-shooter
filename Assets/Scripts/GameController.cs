using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;//為了要使用讀取場景的功能

public class GameController : MonoBehaviour
{
    public GameObject hazard;//關聯隕石的預製物件
    public Vector3 spawnValues;//設定產生隕石的參考座標
    public int hazardCount;//設定每一播隕石產生數量
    public float spawnWait;//設定每一顆隕石產生間隔
    public float startWait;//設定遊戲執行後延遲多久開始產生隕石
    public float waveWait;//設定每一播隕石的間隔
    public TextMeshProUGUI scoreText;//關聯顯示分數的文字
    public TextMeshProUGUI gameOverText;//關聯顯示遊戲結束的文字
    public TextMeshProUGUI restartText;//關聯顯示重新開始的文字
    public int score;//紀錄贏分
    private bool gameOver;//標記當前遊戲是否結束
    private bool restart;//標記當前是否可以重啟遊戲

    // Start is called before the first frame update
    void Start()
    {
        score = 0;//初始化參數
        gameOver = false;//初始化參數
        restart = false;//初始化參數
        gameOverText.gameObject.SetActive(false);//關閉文字顯示
        restartText.gameObject.SetActive(false);//關閉文字顯示
        UpdateScore();//更新分數文字
        StartCoroutine(SpawnWaves());//啟用一個協同程序
    }

    private void Update()
    {
        if (restart)//如果當前已可重啟
        {
            if (Input.GetKeyDown(KeyCode.R))//判斷玩家是否輸入R鍵
            {
                SceneManager.LoadScene(0);//重新讀取第一個場景
            }
        }
    }

    private IEnumerator SpawnWaves()//協同程序
    {
        yield return new WaitForSeconds(startWait);//等待startWait的時間
        while (true)//死迴圈，永遠執行
        {
            for (int i = 0; i < hazardCount; i++)//for迴圈，執行hazardCount的次數
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);//隨機產生x軸範圍內的座標
                Quaternion spawnRotation = Quaternion.identity;//使用預設值套用在產生物件的旋轉
                Instantiate(hazard, spawnPosition, spawnRotation);//生成隕石到場景上，依照spawnPosition的位置和spawnRotation的旋轉
                yield return new WaitForSeconds(spawnWait);//等待spawnWait時間，再繼續執行下一次for迴圈
            }
            yield return new WaitForSeconds(waveWait);//等待waveWait時間
            if (gameOver)//如果遊戲結束了
            {
                restart = true;//標記可以重新開始
                restartText.gameObject.SetActive(true);//開啟重新開始的文字
                break;//中斷while迴圈
            }
        }
    }

    public void AddScore(int addScore)//提供給其他組件呼叫，可傳入分數
    {
        score += addScore;//將當前的分數累加上去
        UpdateScore();//呼叫刷新分數UI文字
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;//更新分數文字內容
    }

    public void GameOver()//當飛機爆炸會被呼叫，代表遊戲結束了
    {
        gameOverText.gameObject.SetActive(true);//開啟遊戲結束的文字
        gameOver = true;//標記遊戲已結束的狀態
    }
}
