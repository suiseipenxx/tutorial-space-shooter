using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);//設定幾秒後自我刪除
    }
}
