using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hardware : MonoBehaviour, IDragHandler,IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IMouseHoverHandler 
{
    public enum hardwareType { head, chest, arm, leg, all, equipment}
    public hardwareType type;
    //public string description;
    public GameObject descryptionGO;
    RectTransform rectTransform;
    public HardwareSlot hardwareSlot;
    Vector3 offSet;
    Vector3 descryptionOffSet = new Vector3(10,-10);
    bool draged = false;
    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void InitHardware(HardwareSO hardwareSO)
    {
        type = hardwareSO.hardwareType;
        name = hardwareSO.name;
        GetComponent<Image>().sprite = hardwareSO.hardwareIcon;
        upgradeEffects = hardwareSO.upgradeEffects;
        string descryption = hardwareSO.hardwareInGameName + "<br>";

        foreach(UpgradeEffect effect in upgradeEffects) 
        {
            descryption+= effect.descryption + "<br>";
        }
        descryptionGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = descryption;
    }
    void ShowDescryption(bool show)
    {
        if(show)
        {
            descryptionGO.transform.SetParent(UIInputManager.Instance.topUILayer.transform);
            descryptionGO.SetActive(true);
        }
        else
        {
            descryptionGO.transform.SetParent(transform);
            descryptionGO.SetActive(false);
        }
    }
    bool isOverlappingSlot(out HardwareSlot slot)
    {
        slot = null;
        Collider2D Hit = Physics2D.OverlapBox(rectTransform.position, new Vector2( rectTransform.rect.width, rectTransform.rect.height) ,0.0f);
        if(Hit == null)
        {
            return false;
        }
        if(Hit.tag != "HardwareSlot")
        {
            return false;
        }
        slot = Hit.GetComponent<HardwareSlot>();
        return true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offSet =  rectTransform.position - Input.mousePosition;
        ShowDescryption(false);
        draged = true;
        hardwareSlot.RemoveHardware();
        transform.SetParent(UIInputManager.Instance.topUILayer);
        Debug.Log("picked UP", gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        draged = true;
        rectTransform.position = Input.mousePosition + offSet;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draged = false;
        HardwareSlot tempSlot;
        if (isOverlappingSlot(out tempSlot))
        {
            if (tempSlot.AddHardware(this) == false)
            {
                //Cant be added to this Slot
                HardwareEquipment.Instance.AddToEquipment(this);
                HardwareEquipment.Instance.MoveToFirstEmptySlot(this);
            }
            else if (tempSlot.slotType == Hardware.hardwareType.equipment)
            {
                HardwareEquipment.Instance.AddToEquipment(this);
            }
        }
        else
        {
            //No overlapping slot
            HardwareEquipment.Instance.AddToEquipment(this);
            HardwareEquipment.Instance.MoveToFirstEmptySlot(this);
        }
        Debug.Log("droped", gameObject);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse enter", gameObject);
        ShowDescryption(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse Exit", gameObject);
        ShowDescryption(false);
    }

    public void OnMouseHover()
    {
        //Debug.Log("mouse over", gameObject);
        if (draged == false)
        {
            descryptionGO.GetComponent<RectTransform>().position = Input.mousePosition + descryptionOffSet;
        }
    }

    public void AddToHandler()
    {
        if (UpgradesHandler.instance.instaledHardware.Contains(this))
            return;

        Hardware copy = Copy();
        UpgradesHandler.instance.instaledHardware.Add(copy);
        foreach(UpgradeEffect effect in copy.upgradeEffects)
        {
            effect.AddToHandler();
        }
    }
    public Hardware Copy()
    {
        Hardware copy = new Hardware();
        copy.name = name;
        copy.type = type;
        copy.upgradeEffects = new List<UpgradeEffect>();
        foreach (UpgradeEffect effect in upgradeEffects)
        {
            copy.upgradeEffects.Add(effect);
        }
        return copy;
    }
}