using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesHandler : MonoBehaviour
{
    public FakePlayerControler playerControler;

    public List<Software> instaledSoftware = new List<Software>();
    public List<Hardware> instaledHardware = new List<Hardware>();

    public float movementSpeedBonus;
    public float damageBonus;

    public bool isInvulnerable;
    public bool shootForFree;

    public UnityEvent OnUpdate = new UnityEvent();
    public UnityEvent OnFixedUpdate = new UnityEvent();

    public UnityEvent OnDashButton = new UnityEvent();
    public UnityEvent OnDashStarted = new UnityEvent();
    public UnityEvent OnDashEnded = new UnityEvent();

    public UnityEvent OnSprintButton = new UnityEvent();
    public UnityEvent OnSprintStarted = new UnityEvent();
    public UnityEvent OnSprintEnded = new UnityEvent();

    public UnityEvent OnJumpButton = new UnityEvent();
    public UnityEvent OnJump = new UnityEvent();
    public UnityEvent OnDoubleJump = new UnityEvent();
    public UnityEvent OnLanding= new UnityEvent();

    public UnityEvent OnRightBeforeDeth = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    int nextUpgradeId;

    public static UpgradesHandler instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            nextUpgradeId = 0;
        }
        else
            Destroy(instance);
    }

    void Start()
    {
        ResetValues();

        OnDashButton.AddListener(OnDashDebug);

        OnSprintButton.AddListener(OnSprintDebug);
    }

    private void Update()
    {
        OnUpdate.Invoke();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate.Invoke();
    }
    void OnDashDebug()
    { Debug.Log("On dash Button Event"); }
    void OnSprintDebug()
    { Debug.Log("On sprint Button Event"); }

    public int getNextUpgradeID()
    {
        return nextUpgradeId++;
    }
    public void removeAllSoftware()
    {
        foreach (Software software in instaledSoftware)
        {
            foreach(UpgradeEffect effect in software.upgradeEffects)
            {
                effect.RemoveFromHandler();
            }
        }
        instaledSoftware.Clear();
    }

    public void removeAllHardware()
    {
        foreach (Hardware hardware in instaledHardware)
        {
            foreach (UpgradeEffect effect in hardware.upgradeEffects)
            {
                effect.RemoveFromHandler();
            }
        }
        instaledSoftware.Clear();
    }

    public void ResetValues()
    {
        instaledSoftware.Clear();
        instaledHardware.Clear();

        movementSpeedBonus = 1;
        damageBonus = 1;

        isInvulnerable = false;
        shootForFree = false;

        OnUpdate.RemoveAllListeners();
        OnFixedUpdate.RemoveAllListeners();

        OnDashButton.RemoveAllListeners();
        OnDashStarted.RemoveAllListeners();
        OnDashEnded.RemoveAllListeners();

        OnSprintButton.RemoveAllListeners();
        OnSprintStarted.RemoveAllListeners();
        OnSprintEnded.RemoveAllListeners();

        OnJumpButton.RemoveAllListeners();
        OnJump.RemoveAllListeners();
        OnDoubleJump.RemoveAllListeners();
        OnLanding.RemoveAllListeners();

        OnRightBeforeDeth.RemoveAllListeners();
        OnDeath.RemoveAllListeners();
    }
}