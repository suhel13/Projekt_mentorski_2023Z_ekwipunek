using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class HardwareSlot : MonoBehaviour
{
    public Hardware.hardwareType slotType;
    public Hardware hardwareInSlot;
    public bool ocupied;

    public bool AddHardware(Hardware hardware)
    {
        Debug.Log(slotType.ToString());
        if (hardware.type != slotType)
        {
            if(slotType != Hardware.hardwareType.all && slotType != Hardware.hardwareType.equipment) 
            {
                Debug.Log("Wrond slot ", gameObject);
                return false;
            }
        }
        if (ocupied)
        {
            Debug.Log("Spwaped", gameObject);
            SwapHardwares(hardware);
            return true;
        }
        else
        {
            if(slotType == Hardware.hardwareType.equipment)
            {
                Debug.Log("Add to equipment");
                HardwareEquipment.Instance.AddToEquipment(hardware);
            }

            Debug.Log(hardware.name + " installed", gameObject);
            hardware.transform.SetParent(this.transform);
            hardware.GetComponent<RectTransform>().localPosition = Vector3.zero;
            hardware.hardwareSlot = this;
            hardwareInSlot = hardware;
            ocupied = true;
            return true;
        }
    }
    public void SwapHardwares(Hardware newHardware)
    {
        HardwareSlot slotA;
        HardwareSlot slotB;
        Hardware hardwareB;

        slotA = newHardware.hardwareSlot;
        slotB = this;
        hardwareB = hardwareInSlot;
        slotA.RemoveHardware();
        slotB.RemoveHardware();

        slotA.AddHardware(hardwareB);
        slotB.AddHardware(newHardware);

    }
    public void RemoveHardware()
    {
        HardwareEquipment.Instance.RemoveFromEquipment(hardwareInSlot);
        hardwareInSlot = null;
        ocupied = false;
    }
    public void RemoveHardwareData()
    {
        hardwareInSlot = null;
        ocupied = false;
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
