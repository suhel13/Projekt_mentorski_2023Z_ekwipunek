using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprint Effect", menuName = "Upgrades/Efects/Sprint ", order = 3)]
public class Sprint : UpgradeEffect
{
    public float movementSpeedBonus;
    [HideInInspector] public bool isSprinting = false;
    public float time;
    private float timer;
    public float cooldown;
    private float cooldownTimer;

    public override void AddToHandler()
    {
        UpgradesHandler.instance.OnSprintButton.AddListener(SprintStart);
        UpgradesHandler.instance.OnUpdate.AddListener(SprintUpdate);
    }
    public override void RemoveFromHandler()
    {
        SprintEnd();
        UpgradesHandler.instance.OnSprintButton.RemoveListener(SprintStart);
        UpgradesHandler.instance.OnUpdate.RemoveListener(SprintUpdate);
    }
    void SprintUpdate()
    {
        if (isSprinting)
        {
            if (timer < time)
            {
                timer += Time.deltaTime;
            }
            else
                SprintEnd();
        }
        else if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
    }

    void SprintStart()
    {
        if (isSprinting)
            return;
        if (cooldownTimer < cooldown)
            return;
        Debug.Log("Sprint started");
        timer = 0;

        isSprinting = true;
        UpgradesHandler.instance.movementSpeedBonus += movementSpeedBonus;
        UpgradesHandler.instance.OnSprintStarted.Invoke();
    }

    void SprintEnd()
    {
        if (isSprinting == false)
            return;

        Debug.Log("Sprint ended");
        isSprinting = false;

        cooldownTimer = 0;
        UpgradesHandler.instance.movementSpeedBonus -= movementSpeedBonus;
        UpgradesHandler.instance.OnSprintEnded.Invoke();
    }
}