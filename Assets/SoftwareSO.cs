using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Software", menuName = "Upgrades/Software ", order = 1)]
public class SoftwareSO : ScriptableObject
{
    public Sprite softwareBlockSprite;
    public Color spriteColor = Color.white;
    public string softwareInGameName;
    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();
    public float reserchCost;
    public bool isReserched;
    [Range(2, 5)] public int size = 4;
    [HideInInspector] public bool shape_0_0;
    [HideInInspector] public bool shape_1_0;
    [HideInInspector] public bool shape_2_0;
    [HideInInspector] public bool shape_3_0;
    [HideInInspector] public bool shape_4_0;

    [HideInInspector] public bool shape_0_1;
    [HideInInspector] public bool shape_1_1;
    [HideInInspector] public bool shape_2_1;
    [HideInInspector] public bool shape_3_1;
    [HideInInspector] public bool shape_4_1;

    [HideInInspector] public bool shape_0_2;
    [HideInInspector] public bool shape_1_2;
    [HideInInspector] public bool shape_2_2;
    [HideInInspector] public bool shape_3_2;
    [HideInInspector] public bool shape_4_2;

    [HideInInspector] public bool shape_0_3;
    [HideInInspector] public bool shape_1_3;
    [HideInInspector] public bool shape_2_3;
    [HideInInspector] public bool shape_3_3;
    [HideInInspector] public bool shape_4_3;

    [HideInInspector] public bool shape_0_4;
    [HideInInspector] public bool shape_1_4;
    [HideInInspector] public bool shape_2_4;
    [HideInInspector] public bool shape_3_4;
    [HideInInspector] public bool shape_4_4;
    public bool getValue(int x, int y)
    {
        switch (x, y)
        {
            case (0, 0):
                return shape_0_0;
            case (1, 0):
                return shape_1_0;
            case (2, 0):
                return shape_2_0;
            case (3, 0):
                return shape_3_0;
            case (4, 0):
                return shape_4_0;

            case (0, 1):
                return shape_0_1;
            case (1, 1):
                return shape_1_1;
            case (2, 1):
                return shape_2_1;
            case (3, 1):
                return shape_3_1;
            case (4, 1):
                return shape_4_1;

            case (0, 2):
                return shape_0_2;
            case (1, 2):
                return shape_1_2;
            case (2, 2):
                return shape_2_2;
            case (3, 2):
                return shape_3_2;
            case (4, 2):
                return shape_4_2;

            case (0, 3):
                return shape_0_3;
            case (1, 3):
                return shape_1_3;
            case (2, 3):
                return shape_2_3;
            case (3, 3):
                return shape_3_3;
            case (4, 3):
                return shape_4_3;

            case (0, 4):
                return shape_0_4;
            case (1, 4):
                return shape_1_4;
            case (2, 4):
                return shape_2_4;
            case (3, 4):
                return shape_3_4;
            case (4, 4):
                return shape_4_4;
            default:
                return false;
        }
    }
    public void setValue(int x, int y, bool value)
    {
        switch (x, y)
        {
            case (0, 0):
                shape_0_0 = value;
                break;
            case (1, 0):
                shape_1_0 = value;
                break;
            case (2, 0):
                shape_2_0 = value;
                break;
            case (3, 0):
                shape_3_0 = value;
                break;
            case (4, 0):
                shape_4_0 = value;
                break;

            case (0, 1):
                shape_0_1 = value;
                break;
            case (1, 1):
                shape_1_1 = value;
                break;
            case (2, 1):
                shape_2_1 = value;
                break;
            case (3, 1):
                shape_3_1 = value;
                break;
            case (4, 1):
                shape_4_1 = value;
                break;

            case (0, 2):
                shape_0_2 = value;
                break;
            case (1, 2):
                shape_1_2 = value;
                break;
            case (2, 2):
                shape_2_2 = value;
                break;
            case (3, 2):
                shape_3_2 = value;
                break;
            case (4, 2):
                shape_4_2 = value;
                break;

            case (0, 3):
                shape_0_3 = value;
                break;
            case (1, 3):
                shape_1_3 = value;
                break;
            case (2, 3):
                shape_2_3 = value;
                break;
            case (3, 3):
                shape_3_3 = value;
                break;
            case (4, 3):
                shape_4_3 = value;
                break;

            case (0, 4):
                shape_0_4 = value;
                break;
            case (1, 4):
                shape_1_4 = value;
                break;
            case (2, 4):
                shape_2_4 = value;
                break;
            case (3, 4):
                shape_3_4 = value;
                break;
            case (4, 4):
                shape_4_4 = value;
                break;

            default:
                break;
        }
    }
    public bool Latch(int x, int y)
    {
        if (getValue(x, y))
        {
            setValue(x, y, false);
            return false;
        }
        else
        {
            setValue(x, y, true);
            return true;
        }
        //Debug.Log(getValue(x,y) + " X: " + x + " Y: " + y);
    }
}

[CustomEditor(typeof(SoftwareSO))]
public class SoftwareSO_Inspector : Editor
{

    float buttonSize = 50;
    SoftwareSO myScript;
    //SerializedObject serializedObject;
    void OnEnable()
    {
        myScript = (SoftwareSO)target;
        //serializedObject = new SerializedObject(myScript);
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SerializedObject serializedObject = new SerializedObject(myScript);

        //Debug.Log(serializedPropertySoftwareShape.arraySize);

        for (int i = 0; i<myScript.size; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < myScript.size; j++)
            {
                SerializedProperty serializedPropertyShape = serializedObject.FindProperty(string.Format("shape_{0}_{1}", j, i));
                if (myScript.getValue(j,i))
                {
                    GUI.backgroundColor = Color.green;
                }
                else
                {
                    GUI.backgroundColor = Color.grey;
                }
                if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
                {
                    serializedPropertyShape.boolValue = myScript.Latch(j, i);
                }
            }
            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}


[Serializable]
public class SoftwareShape
{
    [Range(2, 5)] public int size = 4;
    public bool[,] shape;
    private bool isContructed = false;
    public SoftwareShape()
    {
        if (isContructed)
            return;
        shape = new bool[5, 5];
        isContructed = true;
    }
    public void Latch(int x, int y)
    {
        if (shape[x, y])
        {
            shape[x, y] = false;
        }
        else
        {
            shape[x, y] = true;
        }
        Debug.Log(shape[x, y] + " X: " + x + " Y: " + y);
    }
}