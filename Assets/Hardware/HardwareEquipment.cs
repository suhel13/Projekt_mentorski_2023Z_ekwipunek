using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class HardwareEquipment : MonoBehaviour
{
    public static HardwareEquipment Instance { get; private set; }
    List<HardwareSlot> slots;
    List<Hardware> hardwaresInEquipment = new List<Hardware>();
    Dictionary<string, int> hardwaresInEquipmentAmounts = new Dictionary<string, int>();

    List<HardwareSO> allHardwareSOs = new List<HardwareSO>();
    Dictionary<string, HardwareSO> allHardwareSODict = new Dictionary<string, HardwareSO>();
    [SerializeField] GameObject hardwarePrefab;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        slots = GetComponentsInChildren<HardwareSlot>().ToList();

    }
    private void Start()
    {
        LoadAllHardwareSO();
        CreateAllHardwareFromDict();
        //LoadHardwareToEquipment();
    }
    void CreateAllHardwareFromDict()
    {
        foreach (KeyValuePair<string, HardwareSO> entry in allHardwareSODict)
        {
            Hardware tempHardware = CreateHardware(entry.Value);
            AddToEquipment(tempHardware);
            MoveToFirstEmptySlot(tempHardware);
            tempHardware.id = UpgradesHandler.instance.getNextUpgradeID();
        }
    }
    void LoadAllHardwareSO()
    {
        foreach (var hardwareSO in Resources.LoadAll<HardwareSO>("Hardware/")) 
        {
            allHardwareSODict.Add(hardwareSO.name,hardwareSO);
        }
    }
    public void LoadHardwareToEquipment()
    {
        foreach (Hardware hardware in hardwaresInEquipment)
        {
            Destroy(hardware.gameObject);
        }

        hardwaresInEquipment.Clear();
        hardwaresInEquipmentAmounts.Clear();
        foreach (HardwareSlot slot in slots)
        {
            slot.RemoveHardwareData();
        }

        if (File.Exists("HardwareEquipmentJSON"))
        {
            EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();

            SerializableList<HardwareSaveObject> hardwareLoadedObjectList = saverLoader.LoadFromJSON<SerializableList<HardwareSaveObject>>("HardwareEquipmentJSON");
            foreach (HardwareSaveObject hardware in hardwareLoadedObjectList.list)
            {
                Hardware tempHardware = CreateHardware(hardware.name);
                AddToEquipment(tempHardware);
                MoveToFirstEmptySlot(tempHardware);
            }
        }
    }

    public void SaveEquipment()
    {
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();
        SerializableList<HardwareSaveObject> hardwareSaveObjectList = new SerializableList<HardwareSaveObject>();
        foreach (Hardware hardware in hardwaresInEquipment)
        {
            HardwareSaveObject tempSaveObject = new HardwareSaveObject();
            tempSaveObject.name = hardware.name;
            tempSaveObject.amount = 1;
            hardwareSaveObjectList.list.Add(tempSaveObject);
        }
        saverLoader.SaveToJSON(hardwareSaveObjectList, "HardwareEquipmentJSON");
    }

    public Hardware CreateHardware(HardwareSO hardwareSO)
    {
        Hardware tempHarware = Instantiate(hardwarePrefab).GetComponent<Hardware>();
        tempHarware.InitHardware(hardwareSO);
        return tempHarware;
    }
    public Hardware CreateHardware(string hardwareName)
    {
        Hardware tempHarware = Instantiate(hardwarePrefab).GetComponent<Hardware>();
        tempHarware.InitHardware(allHardwareSODict[hardwareName]);
        return tempHarware;
    }

    public void AddToEquipment(Hardware hardware)
    {
        if (hardwaresInEquipment.Contains(hardware))
            return;
        if (hardwaresInEquipmentAmounts.ContainsKey(hardware.name))
            hardwaresInEquipmentAmounts[hardware.name] += 1;
        else
            hardwaresInEquipmentAmounts[hardware.name] = 1;

            hardwaresInEquipment.Add(hardware);
    }
    public void MoveToFirstEmptySlot(Hardware hardware)
    {
        if(hardware.hardwareSlot!= null)
            hardware.hardwareSlot.RemoveHardware();

        slots[FirstEmptySlot()].AddHardware(hardware);
    }

    public void ShowInEquipment(Hardware hardware)
    {
        slots[FirstEmptySlot()].AddHardware(hardware);
    }

    public void RemoveFromEquipment(Hardware hardware) 
    {
        if (hardwaresInEquipment.Contains(hardware))
        {
            hardwaresInEquipment.Remove(hardware);
            hardwaresInEquipmentAmounts[hardware.name] -= 1;
        }
    }
    int FirstEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++) 
        {
            if (slots[i].ocupied == false)
                return i;
        }
        throw new System.Exception("No emty Space in Equipment");
    }
    public void ShowAllType() { ShowType(Hardware.hardwareType.all); }
    public void ShowHeadType() { ShowType(Hardware.hardwareType.head); }
    public void ShowChestType() { ShowType(Hardware.hardwareType.chest); }
    public void ShowArmType() { ShowType(Hardware.hardwareType.arm); }
    public void ShowLegType() { ShowType(Hardware.hardwareType.leg); }


    void ShowType(Hardware.hardwareType type)
    {
        foreach (HardwareSlot slot in slots)
        {
            if (slot.hardwareInSlot != null)
            {
                slot.hardwareInSlot.gameObject.SetActive(false);
                slot.RemoveHardwareData();
            }
        }
        if (type == Hardware.hardwareType.all)
        {
            foreach (Hardware hardware in hardwaresInEquipment)
            {
                hardware.gameObject.SetActive(true);
                ShowInEquipment(hardware);
            }
        }
        else
        {
            foreach (Hardware hardware in hardwaresInEquipment)
            {
                if (hardware.type == type)
                {
                    hardware.gameObject.SetActive(true);
                    ShowInEquipment(hardware);
                }
            }
        }
    }
}