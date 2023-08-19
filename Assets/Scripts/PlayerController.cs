using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]//特性Attribute，標記這個類別可以被序列化成文件，讓他可以在Inspector設定
    public class Boundary//宣告一個名為Boundary的類別，其目的用來記錄邊界的範圍
    {
        public float xMin;
        public float xMax;
        public float zMin;
        public float zMax;
    }

    public float speed = 10;//飛船移動的速度
    public float tilt = 4;//飛船左右移動時機身的傾斜度
    public Boundary boundary;//宣告一個Boundary類型的變數
    public GameObject shot;//用來關聯子彈的Prefab
    public Transform shotSpawn;//用來關聯子彈發射的位置參考
    public float fireRate = 0.25f;//子彈發射頻率

    private float nextFire;//記錄下一次可發射的時間
    private Rigidbody rb;//關聯身上的rigidbody組件
    private AudioSource audioSource;//關聯身上的audioSource組件

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();//自動關聯身上的rigidbody組件
        audioSource = GetComponent<AudioSource>();//自動關聯身上的audioSource組件
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)//每一幀判斷玩家是否有輸入"Fire1"的熱鍵組，且現在的時間要大於下一次能發射的時間
        {
            nextFire = Time.time + fireRate;//把下一次能發射的時候改成現在時間加上發射頻率
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);//生成一個子彈到shotSpawn的座標和旋轉到場景上
            audioSource.Play();//播放音效
        }    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");//讀取玩家水平熱鍵組輸入的值(-1~1)
        float moveVertical = Input.GetAxis("Vertical");//讀取玩家垂直熱鍵組輸入的值(-1~1)
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);//將輸入值存放在一個三維結構中
        rb.velocity = movement * speed;//將rigidbody的速度給予當前輸入值方向乘上一speed變數控制整體速度大小
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);//根據當前x軸速度去傾斜飛機z軸角度

        rb.position = new Vector3//現在飛機能移動的邊界範圍
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),//將給予值限定在指定的範圍內
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }
}
