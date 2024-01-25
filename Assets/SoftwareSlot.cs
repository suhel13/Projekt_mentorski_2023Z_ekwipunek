using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class SoftwareSlot : MonoBehaviour
{
    public enum SoftwareType { Slot, Equipment}
    public SoftwareType slotType;
    public SoftwareBlock blockInSlot;
    public Software softwareInSlot;
    public bool ocupied;

    public void AddBlock(SoftwareBlock block)
    {
        ocupied = true;
        blockInSlot = block;
    }

    public void AddSoftware(Software software)
    {
        software.OnAddToEquipment();
        software.transform.SetParent(transform);
        software.GetComponent<RectTransform>().localPosition = Vector3.zero;
        software.softwareSlot = this;
        ocupied = true;
        softwareInSlot = software;

    }
    public void RemoveBlock()
    {
        blockInSlot = null;
        ocupied = false;
    }
    public void RemoveSoftware()
    {
        if (softwareInSlot != null)
        {
            softwareInSlot.transform.SetParent(UIInputManager.Instance.topUILayer.transform);
            softwareInSlot = null;
        }
        ocupied = false;
    }
}