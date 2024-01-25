using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Effect", menuName = "Upgrades/Efects/Dash ", order = 3)]
public class Dash : UpgradeEffect
{
    public float speed;
    Vector2 dashDirectrion;
    [HideInInspector] public bool isDashing = false;
    public float time;
    private float timer;
    public float cooldown;
    private float cooldownTimer; 

    public override void AddToHandler()
    {
        Debug.Log(name + " added to handler");
        UpgradesHandler.instance.OnDashButton.AddListener(DashStart);
        UpgradesHandler.instance.OnFixedUpdate.AddListener(DashFixedUpdate);
        UpgradesHandler.instance.OnUpdate.AddListener(DashUpdate);
    }
    public override void RemoveFromHandler()
    {
        DashEnded();
        UpgradesHandler.instance.OnDashButton.RemoveListener(DashStart);
        UpgradesHandler.instance.OnFixedUpdate.RemoveListener(DashFixedUpdate);
        UpgradesHandler.instance.OnUpdate.RemoveListener(DashUpdate);
    }

    void DashFixedUpdate()
    {
        if (isDashing == false)
            return;

        Debug.Log("dash");
        // dashDirection * speed 
    }
    void DashUpdate()
    {
        if (isDashing)
        {
            if (timer < time)
            {
                //zminana kierunku ruchu gracza w stronê dashDirectrion z prêdkoœci¹ speed
                timer += Time.deltaTime;
            }
            else
                DashEnded();
        }
        else if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
    }

    void DashStart()
    {
        Debug.Log("Dash started");
        if (cooldownTimer < cooldown)
            return;

        dashDirectrion.x = Input.GetAxis("Horizontal");
        dashDirectrion.y = Input.GetAxis("Vertical");
        dashDirectrion = dashDirectrion.normalized;

        timer = 0;

        isDashing = true;
        UpgradesHandler.instance.OnDashStarted.Invoke();
    }

    void DashEnded() 
    {
        if (isDashing == false)
            return;

        isDashing = false;
        UpgradesHandler.instance.OnDashEnded.Invoke();
        cooldownTimer = 0;
    }
}