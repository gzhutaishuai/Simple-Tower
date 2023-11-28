using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject mapCubeGo;//当前cube上的炮台

    public GameObject buildEffect;//创建时的粒子特效

    public Renderer renderer;

    public bool isUpgraded;//是否升级过
    [HideInInspector]
    public TurretData turretData;//炮塔信息
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    ///建造炮台
    /// </summary>
    /// <param name="turretPrefab"></param>
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        //实例化炮台
        isUpgraded = false;
      mapCubeGo=GameObject.Instantiate(turretData.turretPrefab,transform.position,Quaternion.identity);
      GameObject effect= GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect,1.0f);
    }

    public void UpgradeTurret()
    {
        if (isUpgraded) return;
        Destroy(mapCubeGo);
        isUpgraded=true;
        mapCubeGo = GameObject.Instantiate(turretData.turretPrefan_Upgrade, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void DestoryTurret()
    {
        Destroy(mapCubeGo);
        isUpgraded=false;
        mapCubeGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    //设置鼠标移动到cube上变红的效果
    private void OnMouseEnter()
    {
        if(mapCubeGo==null&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderer.material.color = Color.red;
        }
    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
