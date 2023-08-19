using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble = 5;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();//自動關聯身上的rigidbody組件
        rb.angularVelocity = Random.insideUnitSphere * tumble;//對rigidbody設定角速度，使其自轉
    }
}
