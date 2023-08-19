using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)//當有任意碰撞器離開此觸發器會自動被呼叫此方法
    {
        Destroy(other.gameObject);//刪除離開的碰撞器的遊戲物件
    }
}
