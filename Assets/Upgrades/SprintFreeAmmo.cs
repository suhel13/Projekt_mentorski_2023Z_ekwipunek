using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprint Free Ammo Effect", menuName = "Upgrades/Efects/Sprint Free Ammo", order = 3)]
public class SprintFreeAmmo : UpgradeEffect
{
    public override void AddToHandler()
    {
        UpgradesHandler.instance.OnSprintStarted.AddListener(StartFreeAmmo);
        UpgradesHandler.instance.OnSprintEnded.AddListener(EndFreeAmmo);
    }

    public override void RemoveFromHandler()
    {
        EndFreeAmmo();
        UpgradesHandler.instance.OnSprintStarted.RemoveListener(StartFreeAmmo);
        UpgradesHandler.instance.OnSprintEnded.RemoveListener(EndFreeAmmo);
    }
    public void StartFreeAmmo()
    {
        UpgradesHandler.instance.shootForFree = true;
    }
    public void EndFreeAmmo()
    {
        UpgradesHandler.instance.shootForFree = false;
    }
}