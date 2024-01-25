using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum itemNames { Palstic, Titan, Nanotubes, Silicon }
    List<itemNames> items;
    public static Equipment Instance { set; private get; }
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class ItemCost
{
    Equipment.itemNames itemName;
    int cost;
}