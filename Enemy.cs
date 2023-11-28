using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] waypoints;//路径数组
    private int index = 0;//路径点索引
    public float speed=3.0f;//敌人移动速度
    public float hp;
    public float tolHp;
    public GameObject deadEffect;//死亡特效
    private Slider hpSlider;//血量条
    void Start()
    {
        waypoints = WayPoints.pointsPosition;//获取WayPoints类里的路径点信息
        tolHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        Move();
        
    }

    /// <summary>
    /// 敌人在路径上移动
    /// </summary>
    private void Move()
    {
       if (index > waypoints.Length - 1) return;
        transform.Translate((waypoints[index].position-transform.position).normalized*Time.deltaTime*speed);
        if (Vector3.Distance(waypoints[index].position,transform.position)<0.2f)
        {
            index++;
        }
        if(index > waypoints.Length -1)
        {
           ReachDestation();
        }
    }

    private void ReachDestation()
    {
        GameManager.instance.Fail();
       GameObject.Destroy(this.gameObject);
        OnDestory();
    }

    void OnDestory()
    {
        EnemySpawner.curEnemyAlive--;
    }
    //敌人受伤掉血
    public void OnTakeDamage(float damage)
    {
        if(hp<=0) return;
        hp -= damage;
        hpSlider.value =hp / tolHp;
        if(hp<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        GameObject dieEffect=GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
        OnDestory();
        Destroy(dieEffect,1.0f);
        Destroy(this.gameObject);
    }
}
