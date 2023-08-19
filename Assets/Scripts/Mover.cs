using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 20;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();//自動關聯身上的rigidbody組件
        rb.velocity = transform.forward * speed;//對rigidbody設定速度
    }
}
