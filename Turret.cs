using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //创建存储进入范围的敌人列表
   private List<GameObject> enemys= new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Enemy")
        enemys.Remove(other.gameObject);
    }

    public float attackRate=1.0f;//攻速
    private float timer;//计时器

    public GameObject bulletPrefab;//子弹

    public Transform firePosition;//子弹生成的位置

    public Transform head;//旋转位置

    public bool isLaser;//是否是激光炮塔

    public int laserDamage=2;//激光炮台伤害

    public LineRenderer lineRenderer;//激光

    public GameObject laserEffect;//激光命中敌人的特效
    private void Start()
    {
        timer = attackRate;
    }

    private void Update()
    {
        if (enemys.Count > 0 && enemys[0]!=null)
        {
            Vector3 targetPosioton=enemys[0].transform.position;
            targetPosioton.y = head.transform.position.y;
            head.LookAt(targetPosioton);

        }
        if (!isLaser)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if(lineRenderer.enabled == false)
            {
                lineRenderer.enabled = true;
            }
            laserEffect.SetActive(true);
            if (enemys[0]==null)
            {
                UpdateEnemy();
            }
            if(enemys.Count>0)
            {
                //激光的射向位置
                lineRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position});
                enemys[0].GetComponent<Enemy>().OnTakeDamage(laserDamage * Time.deltaTime);
                //激光命中敌人的特效位置
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            lineRenderer.enabled=false;
            laserEffect.SetActive (false);
        }
        
    }
    private void Attack()
    {
        //如果当前锁定的敌人被销毁
        if (enemys[0]==null)
        {
            UpdateEnemy();
        }
        
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRate;
        }
    }
    private void UpdateEnemy()
    {
        //定义一个List，用来保存变空的敌人
        List<int> emptyIndex=new List<int>();  
        //检测哪个位置的敌人被销毁
        for(int i=0;i<enemys.Count; i++)
        {
            if (enemys[i]==null)
            {
                emptyIndex.Add(i);
            }
        }
        for(int i=0;i<emptyIndex.Count;i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
}
