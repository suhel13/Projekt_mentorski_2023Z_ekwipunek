using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Jump Invurability Effect", menuName = "Upgrades/Efects/Double Jump Invurability", order = 3)]
public class DoubleJumpInvurability : UpgradeEffect
{
    public float invulnerabilityDuration;
    public float invulnerabilityCooldown;
    Invulnerability invurability;

    public override void AddToHandler()
    {
        invurability = new Invulnerability(invulnerabilityDuration, invulnerabilityCooldown);

        UpgradesHandler.instance.OnDoubleJump.AddListener(invurability.startIvurability);
        UpgradesHandler.instance.OnUpdate.AddListener(invurability.InvurabilityUpdate);
    }

    public override void RemoveFromHandler()
    {
        UpgradesHandler.instance.OnDoubleJump.RemoveListener(invurability.startIvurability);
        UpgradesHandler.instance.OnUpdate.RemoveListener(invurability.InvurabilityUpdate);
    }

}