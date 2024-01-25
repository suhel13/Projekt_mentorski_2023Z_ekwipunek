using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareSet : MonoBehaviour
{
    public int setId;
    void Start()
    {
        //LoadHardwareSet(setId);
    }
    public List<HardwareSlot> hardwareSlots;
    public void SaveHardwareSet(int setId)
    {
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();
        SerializableList<HardwareSaveObject> hardwareSaveObjectList = new SerializableList<HardwareSaveObject>();
        foreach (HardwareSlot hardwareSlot in hardwareSlots)
        {
            HardwareSaveObject tempSaveObject = new HardwareSaveObject();
            if (hardwareSlot.ocupied)
            {
                tempSaveObject.name = hardwareSlot.hardwareInSlot.name;
                tempSaveObject.amount = 1;
            }
            else
            {
                tempSaveObject.name = "";
                tempSaveObject.amount = 0;
            }
            hardwareSaveObjectList.list.Add(tempSaveObject);
        }
        string fileName = String.Format("HardwareSet{0}JSON", setId);
        saverLoader.SaveToJSON(hardwareSaveObjectList, fileName);
    }

    public void LoadHardwareSet(int setId)
    {
        foreach (HardwareSlot hardwareSlot in hardwareSlots)
        {
            if (hardwareSlot.ocupied)
            {
                Destroy(hardwareSlot.hardwareInSlot.gameObject);
                hardwareSlot.RemoveHardwareData();
            }
        }

        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();
        string fileName = String.Format("HardwareSet{0}JSON", setId);
        SerializableList<HardwareSaveObject> hardwareLoadedObjectList = saverLoader.LoadFromJSON<SerializableList<HardwareSaveObject>>(fileName);

        for (int i = 0; i < hardwareLoadedObjectList.list.Count; i++)
        {
            Debug.Log(i);
            if (hardwareLoadedObjectList.list[i].amount == 1)
                hardwareSlots[i].AddHardware(HardwareEquipment.Instance.CreateHardware(hardwareLoadedObjectList.list[i].name));
        }
        Debug.Log("Set " + setId + " fully LOaded");
    }
}