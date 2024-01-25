using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeEffect : ScriptableObject
{
    public string descryption;
    public abstract void AddToHandler();
    public abstract void RemoveFromHandler();
}