using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    //敌人位置
    public Transform target;
    //击中特效
    public GameObject explosionEffect;

    public void SetTarget(Transform _target) 
    { 
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Die();
            return;
        }
        //朝向
     transform.LookAt(target.position);
        //向敌人移动
     transform.Translate(Vector3.forward*speed*Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().OnTakeDamage(damage);
            Die();
        }
    }
    private void Die()
    {
        //生成子弹击中特效
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
        Destroy(this.gameObject);
    }
}
