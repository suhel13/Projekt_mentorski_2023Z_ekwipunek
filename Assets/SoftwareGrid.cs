using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoftwareGrid : MonoBehaviour
{
    [Range(2,5)]public int gridWith;
    [Range(4, 8)] public int gridHeight;
    [SerializeField] GameObject slotPrefab;
    [Range(0,100)] [SerializeField] float padding;

    [ExecuteInEditMode]
    public void GenereteGrid(int x, int y)
    {
        Vector2 posOffset = Vector2.zero;
        Vector2 gridSize = Vector2.zero;
        gridSize.x = x * slotPrefab.GetComponent<RectTransform>().rect.width;
        gridSize.y = y * slotPrefab.GetComponent<RectTransform>().rect.height;
        GetComponent<RectTransform>().sizeDelta = gridSize;
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                GameObject tempSoftwareBlock = Instantiate(slotPrefab, transform.position, Quaternion.identity, transform);
                posOffset.x = (j - (x - 1) / 2.0f) * tempSoftwareBlock.GetComponent<RectTransform>().rect.width;
                posOffset.y = ((y - 1) / 2.0f - i) * tempSoftwareBlock.GetComponent<RectTransform>().rect.height;
                tempSoftwareBlock.GetComponent<RectTransform>().anchoredPosition = posOffset;
            }
        }

    }
    public void RemoveAllGridSlots()
    {
        foreach(var item in GetComponentsInChildren<SoftwareSlot>())
        {
            DestroyImmediate(item.gameObject);
        }
    }
    public void SaveSoftwareSet(int setId)
    {
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();

        SerializableList<SoftwareSaveObject> softwareSaveObjectList = new SerializableList<SoftwareSaveObject>();
        foreach (Software software in GetComponentsInChildren<Software>())
        {
            SoftwareSaveObject tempSoftwareSaveObject = new SoftwareSaveObject();
            tempSoftwareSaveObject.name = software.name;
            tempSoftwareSaveObject.position = software.transform.position;
            softwareSaveObjectList.list.Add(tempSoftwareSaveObject);  
        }
        string fileName = String.Format("SoftwareSet{0}JSON", setId);
        saverLoader.SaveToJSON(softwareSaveObjectList, fileName);
    }

    public void LoadHardwareSet(int setId)
    {
        EquimpentSaverLoader saverLoader = new EquimpentSaverLoader();
        string fileName = String.Format("SoftwareSet{0}JSON", setId);

        SerializableList<SoftwareSaveObject> softwareLoadedObjectList = saverLoader.LoadFromJSON<SerializableList<SoftwareSaveObject>>(fileName);

        for (int i = 0; i < softwareLoadedObjectList.list.Count; i++)
        {
            
        }
        Debug.Log("Software Set " + setId + " fully Loaded");
    }
}

[CustomEditor(typeof(SoftwareGrid))]
public class SoftwareGrif_Inspector :Editor
{
    SoftwareGrid myScript;
    void OnEnable()
    {
        myScript = (SoftwareGrid)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Generate Grid"))
        {
            myScript.RemoveAllGridSlots();
            myScript.GenereteGrid(myScript.gridWith, myScript.gridHeight);
        }
    }
}