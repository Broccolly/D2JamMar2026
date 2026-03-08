using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level : MonoBehaviour
{
    private Dictionary<Vector3Int, GameObject> objectMap;
    private Dictionary<Vector3Int, Vector3Int> targetMap;

    Grid grid;

    public InputActionReference trigger;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        objectMap = new Dictionary<Vector3Int, GameObject>();
        targetMap = new Dictionary<Vector3Int, Vector3Int>();
        // Set all conveyors to true grid position and add to the map
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            
            if (obj.CompareTag("Machine"))
            {
                Vector3Int gridPos = grid.WorldToCell((obj.transform.position + 0.5f * new Vector3(1f,1f,1f)));
                if (objectMap.ContainsKey(gridPos))
                {
                    Debug.LogError("Level : GridPos {} already occupied");
                    Destroy(obj);
                }
                else
                {
                    objectMap[gridPos] = obj;
                    obj.transform.position = grid.CellToWorld(gridPos);
                    obj.GetComponent<IMachine>().gridPos = gridPos;
                }
            }
        }
        trigger.action.started += TriggerMachines;
    }

    private void TriggerMachines(InputAction.CallbackContext context)
    {
        Debug.Log("Level Trigger!");
        foreach (GameObject obj in objectMap.Values)
        {
            Debug.Log("Looping through Machines!");
            IMachine mach = obj.GetComponent<IMachine>();
            mach.OnActivate();
        }
    }

    void Start()
    {
        // Go through all conveyors again (using map this time) to set the directionals for each gridPos
        foreach (Vector3Int vec in objectMap.Keys)
        {
            UpdateTargetForGridPos(vec);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3Int UpdateTargetForGridPos(Vector3Int gridPos)
    {
        GameObject obj = objectMap[gridPos];
        IMachine conv = obj.GetComponent<IMachine>();
        targetMap[gridPos] = gridPos + conv.GetWorldTargetDirection();
        //Debug.LogFormat($"{gridPos} -> {targetMap[gridPos]}");
        return targetMap[gridPos];
    }

    public GameObject GetObjectInGridPos(Vector3Int gridPos)
    {
        if (!objectMap.ContainsKey(gridPos))
        {
            return null;
        }
        else
        {
            return objectMap[gridPos];
        }
    }

    public GameObject? GetTargetObjectForGridPos(Vector3Int gridPos)
    {
        Vector3Int? targetPos = GetTargetPosForGridPos(gridPos);
        if (targetPos == null || !objectMap.ContainsKey(targetPos ?? Vector3Int.zero))
        {
            return null;
        }
        else
        {
            return objectMap[targetPos ?? Vector3Int.zero];
        }
    }

    public Vector3Int? GetTargetPosForGridPos(Vector3Int gridPos)
    {
        if (!targetMap.ContainsKey(gridPos))
        {
            return null;
        }
        return targetMap[gridPos];
    }
}