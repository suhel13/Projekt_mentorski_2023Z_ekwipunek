using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelContorler : MonoBehaviour
{
    [SerializeField] GameObject softwarePanel;
    [SerializeField] GameObject hardwarePanel;
    GameObject activePanel;

    private void Start()
    {
        if (softwarePanel.active)
            activePanel = softwarePanel;

        if (hardwarePanel.active)
            activePanel = hardwarePanel;
    }

    public void ActivePanel()
    {
        ClearUpgradeHandler();
        ScanSoftware();
        ScanHardware();
    }
    void ClearUpgradeHandler()
    {
        UpgradesHandler.instance.ResetValues();
    }
    void ScanSoftware()
    {
        foreach(var item in softwarePanel.GetComponent<SoftwareGrid>().GetComponentsInChildren<Software>())
        {
            item.AddToHandler();
        }
    }
    void ScanHardware()
    {
        foreach (var item in hardwarePanel.GetComponent<HardwareSet>().GetComponentsInChildren<Hardware>())
        {
            item.AddToHandler();
        }
    }
    public void SwitchToSoftwarePanel()
    {
        if (activePanel != null)
            activePanel.SetActive(false);

        softwarePanel.SetActive(true);
        activePanel = softwarePanel;
    }
    public void SwitchToHardwarePanel()
    {
        if (activePanel != null)
            activePanel.SetActive(false);

        hardwarePanel.SetActive(true);
        activePanel = hardwarePanel;
    }
}
