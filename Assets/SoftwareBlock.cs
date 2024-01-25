using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class SoftwareBlock : MonoBehaviour
{
    public SoftwareSlot softwareSlot;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool canBePlacedHere()
    {
        SoftwareSlot slot;
        if (isOverlappingSlot(out slot))
        {
            if(slot.ocupied)
                return false;
            else 
                return true;
        }
        else
            return false;
    }

    public void placeHere()
    {
        SoftwareSlot slot;
        isOverlappingSlot(out slot);
        slot.ocupied = true;
        slot.blockInSlot = this;
        softwareSlot = slot;
    }

    public Vector3 AlainVector()
    {
        SoftwareSlot slot;
        isOverlappingSlot(out slot);
        Vector3 AVector = slot.transform.position - transform.position;
        return AVector;
    }
    bool isOverlappingSlot(out SoftwareSlot slot)
    {
        slot = null;
        Collider2D Hit = Physics2D.OverlapBox(rectTransform.position, new Vector2(rectTransform.rect.width, rectTransform.rect.height), 0.0f);
        if (Hit == null)
        {
            return false;
        }
        if (Hit.tag != "SoftwareSlot")
        {
            return false;
        }
        slot = Hit.GetComponent<SoftwareSlot>();
        return true;
    }
}
