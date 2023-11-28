using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData 
{
    public GameObject turretPrefab;
    public float cost;
    public GameObject turretPrefan_Upgrade;
    public float cost_Upgrade;
    public TurretType turretType;
}

public enum TurretType
{
     LaserTurret,
     MisssileTurret,
     StandardTurret
}
