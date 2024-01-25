using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hardware", menuName = "Upgrades/Hardware ", order = 1)]
public class HardwareSO : ScriptableObject
{
    public Hardware.hardwareType hardwareType;
    public Sprite hardwareIcon;
    public string hardwareInGameName;
    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();
}
