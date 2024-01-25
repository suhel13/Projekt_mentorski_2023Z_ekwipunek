using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerControler : MonoBehaviour
{
    float groundedResetTime = 4;
    float groundedResetTimer;
    public bool isGrunded;
    UpgradesHandler handler;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        UpgradesHandler.instance.playerControler = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Debug.Log("Left Control Presed");
            UpgradesHandler.instance.OnDashButton.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift Presed");
            UpgradesHandler.instance.OnSprintButton.Invoke();
        }
        if( Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Presed");
            UpgradesHandler.instance.OnJumpButton.Invoke();

            if (isGrunded)
            {
                Jump();
            }
        }
        if (UpgradesHandler.instance.playerControler.isGrunded == false)
        {
            if (groundedResetTimer > groundedResetTime)
            {
                UpgradesHandler.instance.OnLanding.Invoke();
                isGrunded = true;
                Debug.Log("Landing ");
            }
            else
            {
                groundedResetTimer += Time.deltaTime;
            }
        }

    }

    public void Jump()
    {
        isGrunded = false;
        Debug.Log("Jump");
        groundedResetTimer = 0;
    }
}