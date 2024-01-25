using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultipleJump  Effect", menuName = "Upgrades/Efects/MultipleJump ", order = 3)]
public class MultipleJump : UpgradeEffect
{
    public int bonusJumps;
    int bonusJumpsCounter;

    public override void AddToHandler()
    {
        UpgradesHandler.instance.OnJumpButton.AddListener(AirJump);
        UpgradesHandler.instance.OnLanding.AddListener(Landing);
    }
    public override void RemoveFromHandler()
    {
        UpgradesHandler.instance.OnJumpButton.RemoveListener(AirJump);
        UpgradesHandler.instance.OnLanding.RemoveListener(Landing);
    }

    void AirJump()
    {
        if (UpgradesHandler.instance.playerControler.isGrunded)
            return;
        if (bonusJumpsCounter >= bonusJumps)
            return;
        bonusJumpsCounter++;
        UpgradesHandler.instance.playerControler.Jump();
    }

    void Landing()
    {
        bonusJumpsCounter = 0;
    }
}