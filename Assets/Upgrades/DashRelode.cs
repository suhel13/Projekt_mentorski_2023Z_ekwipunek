using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Relode Effects", menuName = "Upgrades/Efects/Dash Relode ", order = 3)]
public class DashRelode : UpgradeEffect
{
    public override void AddToHandler()
    {
        UpgradesHandler.instance.OnDashStarted.AddListener(RelodeWeppon);
    }
    public override void RemoveFromHandler()
    {
        UpgradesHandler.instance.OnDashStarted.RemoveListener(RelodeWeppon);
    }
    void RelodeWeppon()
    {
        Debug.Log("Relode weppon from dash");
    }
}
