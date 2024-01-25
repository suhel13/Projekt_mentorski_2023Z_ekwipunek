using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Last Chance Double Damage Effect", menuName = "Upgrades/Efects/Last Chance Double Damage ", order = 3)]
public class LastChanceDoubleDamage : UpgradeEffect
{
    public override void AddToHandler()
    {
        UpgradesHandler.instance.OnDeath.AddListener(doubleDamage);
    }

    public override void RemoveFromHandler()
    {
        UpgradesHandler.instance.OnDeath.RemoveListener(doubleDamage);
    }
    void doubleDamage()
    {
        UpgradesHandler.instance.damageBonus *= 2;
    }
}