using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LastChance Effect", menuName = "Upgrades/Efects/LastChance ", order = 3)]
public class LastChance : UpgradeEffect
{
    public float invulnerabilityDuration;
    public float invulnerabilityCooldown;
    Invulnerability invurability;
    
    public override void AddToHandler()
    {
        invurability = new Invulnerability(invulnerabilityDuration, invulnerabilityCooldown);

        UpgradesHandler.instance.OnDeath.AddListener(Revive);
        UpgradesHandler.instance.OnUpdate.AddListener(invurability.InvurabilityUpdate);
    }

    public override void RemoveFromHandler()
    {
        UpgradesHandler.instance.OnDeath.RemoveListener(Revive);
        UpgradesHandler.instance.OnUpdate.RemoveListener(invurability.InvurabilityUpdate);
    }


    void Revive()
    {
        invurability.startIvurability();
        UpgradesHandler.instance.playerControler.hp = 0.1f;
        UpgradesHandler.instance.removeAllHardware();
    }   
}