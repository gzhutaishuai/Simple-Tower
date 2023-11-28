using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;

    //单例
    public static GameManager instance;

    private EnemySpawner enemySpawner;

    private void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    //游戏胜利
    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "胜 利";
    }
    //游戏失败
    public void Fail()
    {
        enemySpawner.Stop();
        endUI.SetActive(true );
        endMessage.text = "失 败";
    }
    //重新开始游戏，重新加载当前场景
    public void ButtonStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //返回菜单
    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
