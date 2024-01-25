using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Software : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IMouseHoverHandler
{
    //public string description;
    RectTransform rectTransform;
    public SoftwareSlot softwareSlot;
    Vector3 offSet;
    bool draged = false;
    public int id;
    public GameObject descryptionGO;
    Vector3 descrptionOffSet = new Vector3(10, -10);

    List<SoftwareBlock> softwareBlocks = new List<SoftwareBlock>();

    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();

    // Start is called before the first frame update
    void Start()
    {
        softwareBlocks = GetComponentsInChildren<SoftwareBlock>().ToList();

        rectTransform = GetComponent<RectTransform>();
        //SoftwareEquipment.Instance.AddToEquipment(this);
        //SoftwareEquipment.Instance.MoveToFirstEmptySlot(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitSoftware(SoftwareSO softwareSO)
    {
        name = softwareSO.name;
        Vector2 posOffset = Vector2.zero;
        upgradeEffects = softwareSO.upgradeEffects;
        for (int i = 0; i < softwareSO.size; i++)
        {
            for (int j = 0; j < softwareSO.size; j++)
            {
                if (softwareSO.getValue(j, i))
                {
                    GameObject tempSoftwareBlock = Instantiate(SoftwareEquipment.Instance.softwareBlockPrefab, transform.position, Quaternion.identity, transform);
                    posOffset.x = (j - (softwareSO.size - 1) / 2.0f) * tempSoftwareBlock.GetComponent<RectTransform>().rect.width;
                    posOffset.y = ((softwareSO.size - 1) / 2.0f - i) * tempSoftwareBlock.GetComponent<RectTransform>().rect.height;
                    tempSoftwareBlock.GetComponent<RectTransform>().anchoredPosition = posOffset;
                    //tempSoftwareBlock.GetComponent<Image>().sprite = softwareSO.softwareBlockSprite;
                    tempSoftwareBlock.GetComponent<Image>().color = softwareSO.spriteColor;
                }
            }
        }
        string descryption = softwareSO.softwareInGameName + "<br>";

        foreach (UpgradeEffect effect in upgradeEffects)
        {
            descryption += effect.descryption + "<br>";
        }
        descryptionGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = descryption;
    }

    public void OnAddToEquipment()
    {
        Debug.Log("On software add to EQ");
        transform.localScale = Vector3.one /4f;
    }
    public void OnRemoveFromEquipment()
    {
        softwareSlot = null;
        Debug.Log("On software remove from EQ");
        transform.localScale = Vector3.one;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Debug.Log("picked UP", gameObject);
        offSet = rectTransform.position - Input.mousePosition;
        UIInputManager.Instance.dragedSoftware = this;
        if (softwareSlot != null && softwareSlot.slotType == SoftwareSlot.SoftwareType.Equipment)
        {
            softwareSlot.RemoveSoftware();
            OnRemoveFromEquipment();
        }
        else if (softwareSlot == null)
        {
            RemoveFromHandler();
            ClearAllSlots();
        }

        //ShowDescryption(false);
        draged = true;
        //softwareSlot.RemoveBlock();
        ShowDescryption(true);
        transform.SetParent(UIInputManager.Instance.topUILayer);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (draged)
            rectTransform.position = Input.mousePosition + offSet;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Debug.Log("droped", gameObject);
        draged = false;
        SoftwareSlot tempSlot;
        UIInputManager.Instance.dragedSoftware = null;
        if (isOverlappingSlot(out tempSlot))
        {
            if (tempSlot.slotType == SoftwareSlot.SoftwareType.Equipment)
            {
                if (tempSlot.ocupied == false)
                {
                    SoftwareEquipment.Instance.AddToEquipment(this);
                    tempSlot.AddSoftware(this);
                }
                else
                {
                    SoftwareEquipment.Instance.AddToEquipment(this);
                    SoftwareEquipment.Instance.MoveToFirstEmptySlot(this);
                }
            }
            else if (tempSlot.slotType == SoftwareSlot.SoftwareType.Slot)
            {
                if (canAllBlockBePlaced())
                {
                    Debug.Log("All Block can be placed");
                    placeAllBlock();
                    AlainToBlock_0();
                    transform.SetParent(GetSoftwareGrigTransform());
                    AddToHandler();
                }
                else
                {
                    SoftwareEquipment.Instance.AddToEquipment(this);
                    SoftwareEquipment.Instance.MoveToFirstEmptySlot(this);
                }
            }
        }
        else
        {
            //No overlapping slot
            SoftwareEquipment.Instance.AddToEquipment(this);
            SoftwareEquipment.Instance.MoveToFirstEmptySlot(this);
        }

    }
    public void Rotate(float angle)
    {
        if (draged)
            transform.eulerAngles += Vector3.forward * angle;
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
    void ClearAllSlots()
    {
        Debug.Log("Clear all software Block from " + name, gameObject);
        foreach (SoftwareBlock block in softwareBlocks)
        {
            if(block.softwareSlot != null)
            {
                block.softwareSlot.RemoveBlock();
                block.softwareSlot = null;
            }
        }
    }
    void placeAllBlock()
    {
        foreach (SoftwareBlock block in softwareBlocks)
        {
            block.placeHere();
        }
    }
    bool canAllBlockBePlaced()
    {
        foreach (SoftwareBlock block in softwareBlocks)
        {
            if (block.canBePlacedHere() == false)
                return false;
        }
        return true;
    }

    void AlainToBlock_0()
    {
        transform.position += softwareBlocks[0].AlainVector();
    }
    Transform GetSoftwareGrigTransform()
    {
        Transform target = softwareBlocks[0].softwareSlot.transform;
        do
        {
            target = target.parent;
        } while (target.gameObject.GetComponent<SoftwareGrid>() == null);
        return target;
    }
    void ShowDescryption(bool show)
    {
        if (show)
        {
            descryptionGO.transform.SetParent(UIInputManager.Instance.topUILayer.transform);
            descryptionGO.SetActive(true);
            descryptionGO.transform.localScale = Vector3.one;
            descryptionGO.transform.eulerAngles = Vector3.zero;
            descryptionGO.transform.localPosition = Vector3.zero +Vector3.left * 100;
        }
        else
        {
            descryptionGO.transform.SetParent(transform);
            descryptionGO.SetActive(false);
        }
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
            descryptionGO.GetComponent<RectTransform>().position = Input.mousePosition + descrptionOffSet;
        }
    }

    public void AddToHandler()
    {
        if (UpgradesHandler.instance.instaledSoftware.Contains(this))
            return;

        Software copy = Instantiate(gameObject).GetComponent<Software>();

        copy.transform.SetParent(UpgradesHandler.instance.transform);

        copy.id = id;

        UpgradesHandler.instance.instaledSoftware.Add(copy);

        foreach (UpgradeEffect effect in copy.upgradeEffects)
        {
            Debug.Log(effect.name);
            effect.AddToHandler();
        }
    }
    public void RemoveFromHandler()
    {
        Debug.Log("removing start: " + name);
        Software copyFromHandler = null;
        foreach ( Software software in UpgradesHandler.instance.instaledSoftware)
        {
            if(software.id == id)
                copyFromHandler = software;
        }

        Debug.Log(copyFromHandler);
        if (copyFromHandler == null)
            return;

        Debug.Log("Removing Software");

        foreach (UpgradeEffect effect in copyFromHandler.upgradeEffects)
        {
            effect.RemoveFromHandler();
        }
        foreach(Software software in UpgradesHandler.instance.instaledSoftware)
        {
            if (software.id == id)
            {
                Destroy(software.gameObject);
                UpgradesHandler.instance.instaledSoftware.Remove(copyFromHandler);
                break;
            }
        }
    }
}