
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoftwareEquipment : MonoBehaviour
{
    public static SoftwareEquipment Instance;
    List<SoftwareSlot> slots;
    Dictionary<string, SoftwareSO> allSoftwareSODict = new Dictionary<string, SoftwareSO>();

    public GameObject softwareBlockPrefab;
    [SerializeField] GameObject softwarePrefab;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        slots = GetComponentsInChildren<SoftwareSlot>().ToList<SoftwareSlot>();

    }
    private void Start()
    {
        LoadAllSoftwareSO();
        CreateAllSoftwareFromDict();
        //LoadSoftwareToEquipment();
    }
    List<Software> softwaresInEquipment = new List<Software>();
    List<SoftwareSlot> equipmentSlots = new List<SoftwareSlot>();

    void LoadAllSoftwareSO()
    {
        foreach (var softwareSO in Resources.LoadAll<SoftwareSO>("Software/"))
        {
            allSoftwareSODict.Add(softwareSO.name, softwareSO);
        }
    }
    void CreateAllSoftwareFromDict()
    {
        foreach(KeyValuePair<string, SoftwareSO> entry in allSoftwareSODict)
        {
            Software tempsoftware =  CreateSoftware(entry.Value);
            AddToEquipment(tempsoftware);
            MoveToFirstEmptySlot(tempsoftware);
            tempsoftware.id = UpgradesHandler.instance.getNextUpgradeID();
        }
    }
    public void LoadSoftwareToEquipment()
    {
        foreach (Software software in softwaresInEquipment)
        {
            Destroy(software.gameObject);
        }

        softwaresInEquipment.Clear();

        foreach (SoftwareSlot slot in slots)
        {
            slot.RemoveSoftware();
        }
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();

        SerializableList<SoftwareSaveObject> softwareLoadedObjectList = saverLoader.LoadFromJSON<SerializableList<SoftwareSaveObject>>("SoftwareEquipmentJSON");
        foreach (SoftwareSaveObject software in softwareLoadedObjectList.list)
        {
            Software tempSoftware = CreateSoftware(software.name);
            AddToEquipment(tempSoftware);
            MoveToFirstEmptySlot(tempSoftware);
        }
    }

    public void SaveEquipment()
    {
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();
        SerializableList<SoftwareSaveObject> softwareSaveObjectList = new SerializableList<SoftwareSaveObject>();
        foreach (Software software in softwaresInEquipment)
        {
            SoftwareSaveObject tempSaveObject = new SoftwareSaveObject();
            tempSaveObject.name = software.name;
            softwareSaveObjectList.list.Add(tempSaveObject);
        }
        saverLoader.SaveToJSON(softwareSaveObjectList, "SoftwareEquipmentJSON");
    }
    public Software CreateSoftware(SoftwareSO softwareSO)
    {
        Software tempSoftware = Instantiate(softwarePrefab).GetComponent<Software>();
        tempSoftware.InitSoftware(softwareSO);
        return tempSoftware;
    }
    public Software CreateSoftware(string softwareName)
    {
        SoftwareSO softwareSO = allSoftwareSODict[softwareName];
        Software tempSoftware = Instantiate(softwarePrefab).GetComponent<Software>();

        tempSoftware.InitSoftware(softwareSO);
        return tempSoftware;
    }

    public void AddToEquipment(Software software)
    {
        if (softwaresInEquipment.Contains(software))
            return;

        softwaresInEquipment.Add(software);
    }
    public void MoveToFirstEmptySlot(Software software)
    {
        if (software.softwareSlot != null)
            software.softwareSlot.RemoveSoftware();

        slots[FirstEmptySlot()].AddSoftware(software);
    }

    public void RemoveFromEquipment(Software software)
    {
        if (softwaresInEquipment.Contains(software))
        {
            softwaresInEquipment.Remove(software);
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
}