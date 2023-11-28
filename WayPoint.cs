using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] pointsPosition;//存放路径点信息

    private void Awake()
    {
        pointsPosition=new Transform[transform.childCount];//获取到路径下面的路径点个数
        for(int i=0; i<pointsPosition.Length; i++)
        {
            pointsPosition[i]=transform.GetChild(i);//按照对应关系把路径点的位置赋值
        }
    }
}
