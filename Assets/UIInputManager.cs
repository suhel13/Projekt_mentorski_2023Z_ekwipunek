using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class UIInputManager : MonoBehaviour
{
    public string gameScene_level_1_Name;
    Vector2 mausePos;
    public Transform topUILayer;
    [HideInInspector] public Software dragedSoftware;
    public List<GameObject> UpgradePanels = new List<GameObject>();
    public static UIInputManager Instance;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData,raycastResults);

        IMouseHoverHandler tempMouseHoverHandler;
        foreach (var item in raycastResults)
        {
            if (item.gameObject.TryGetComponent<IMouseHoverHandler>(out tempMouseHoverHandler))
            {
                tempMouseHoverHandler.OnMouseHover();
            }
        }

        if(Input.GetMouseButtonDown(1)) 
        {
            if(dragedSoftware != null)
            {
                dragedSoftware.Rotate(90);
            }
        }
    }

    public void SwitchToUpgradePanel(int robotId)
    {
        foreach(var item in UpgradePanels)
        {
            item.SetActive(false);
        }
        UpgradePanels[robotId].SetActive(true);
    }
    public void switchToGameScene()
    {
        SceneManager.LoadScene(gameScene_level_1_Name);
    }
}