using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    //三种炮台
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    //当前选中的炮台(UI)
    public TurretData selectedTurretData=null;
    //场景中的物体
    public MapCube selectedMapCube;
    //总金钱
    public float tolMoney=800f;

    //金钱文本
    public Text tolMoneyText;
    //金钱不足时的闪烁动画
    public Animator ani;

    //升级界面画布
    public GameObject upgradeCanvas;

    //升级按钮
    public Button buttonUp;

    //升级面板的动画
    private Animator upgradeCanvasAnimator;


    private void Start()
    {
        upgradeCanvasAnimator=upgradeCanvas.GetComponent<Animator>();
    }
    private void Update()
    {
        //如果点击了鼠标左键
         if(Input.GetMouseButtonDown(0))
        {
            //如果鼠标当前的点不在UI上
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);//通过屏幕坐标转换为射线
                RaycastHit hit;//碰撞信息
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));//是否碰撞
                if(isCollider )
                {
                   MapCube mapCube=hit.collider.GetComponent<MapCube>();//得到点击的mapCube
                   if(selectedTurretData.turretPrefab !=null && mapCube.mapCubeGo==null)
                    {
                        //创建炮台
                        if(tolMoney>=selectedTurretData.cost)//如果总金钱大于花费金钱
                        {
                            
                            mapCube.BuildTurret(selectedTurretData);//创建炮台
                            ChangeMoney(-selectedTurretData.cost);//减去花费
                        }
                        else
                        {
                            //TODO 提示金钱不足
                            ani.SetTrigger("Flash");
                        }
                    }
                    else if(mapCube.mapCubeGo!=null)
                    {
                        //TODO 升级炮台
                        //如果炮台已经被鼠标点击过一次了，并且Ui已经显示出来
                        if(selectedMapCube==mapCube&&upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());//那么关闭UI
                            
                        }
                        //否则显示UI
                        else
                        {
                        ShowUpgradeUI(mapCube.transform.position+new Vector3(0,4,0), mapCube.isUpgraded);
                        }
                        selectedMapCube= mapCube;//每次都要更新当前选择的炮台
                    }
                }
            }
        }
    }

    public void ChangeMoney(float change)
    {
        
        tolMoney += change;
        tolMoneyText.text = tolMoney + "￥";
    }

    public void OnSelectedLaserTurret(bool isOn)
    {
        if(isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnSelectedMissileTurret(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnSelectedStandardTurret(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    private void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgradeUI");//先停止上一个隐藏UI的动画
        upgradeCanvas.SetActive(false);//切换到当前炮塔的动画事件
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUp.interactable = !isDisableUpgrade;
    }   
    
    IEnumerator  HideUpgradeUI()
    {
        //触发隐藏UI的trigger
        upgradeCanvasAnimator.SetTrigger("Hide");  
        yield return new WaitForSeconds(0.5f);
        upgradeCanvas.SetActive(false);
    }
   
    //点击升级按钮
    public void OnUpgradeButtonDown()
    {
        if(tolMoney>=selectedMapCube.turretData.cost_Upgrade)
        {
            ChangeMoney(-selectedMapCube.turretData.cost_Upgrade);
            selectedMapCube.UpgradeTurret();
        }
        
        StartCoroutine(HideUpgradeUI());    
    }
    //点击拆除按钮
    public void OnDestroyButtonDown()
    {
        ChangeMoney(selectedMapCube.turretData.cost/2);
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
