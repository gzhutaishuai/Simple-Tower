using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;//敌人种类的数组
    public Transform startPosition;//生成敌人的起始点
    public float waveRate=3.0f;//每波的间隔
    public static int curEnemyAlive=0;
    public Coroutine coroutine;
    void Start()
    {
       coroutine=StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i=0;i<wave.count;i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, startPosition.position, Quaternion.identity); 
                curEnemyAlive++;
                if(i!=wave.count-1)
                yield return new WaitForSeconds(wave.rate);
            }
            while(curEnemyAlive>0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while(curEnemyAlive>0)
        {
            yield return 0;
        }
        GameManager.instance.Win();
    }
}

