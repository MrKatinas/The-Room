using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Initialize : MonoBehaviour {

    #region Fields

    private static readonly string path_Prefab_Room = "Prefabs/Room";
    private static readonly string path_Prefab = "Prefabs";

    [Header("Size Settings")]
    [Tooltip("Change Room size, come from User inputs")]
    [SerializeField] private Vector3 size = new Vector3(2, 1, 2);

    [Header("Level Settings")]
    [Tooltip("Main Menu Scene name")]
    [SerializeField] private string levelName;

    #endregion

    #region Referencees

    private GameObject Prefab_Room;
    private LevelManager levelManager;

    #endregion

    #region Unity methods

    private void Awake()
    {
        GetSize();
        InstantiateRoom();
        InstantiateStartObject();
        
    }

    private void Start()
    {
        Destroy(this.gameObject);
    }

    #endregion

    #region Object Instantiation

    private void InstantiateRoom()
    {
        Prefab_Room = Resources.Load<Room>(path_Prefab_Room).gameObject;

        if (Prefab_Room)
        {
            Prefab_Room.transform.localScale = size;
            Instantiate(Prefab_Room);
        }
        else
        {
            Debug.LogError("Nebuvo surastas kambarys");
        }
    }

    private void InstantiateStartObject()
    {
        var elements = Resources.LoadAll<Instantiate>(path_Prefab);

        foreach (var element in elements)
        {
            var temp = element.gameObject;

            if (temp)
            {
                Instantiate(temp);
            }
            else
            {
                Debug.LogError("Nebuvo surastas " + element.name);
            }
        }

    }

    private void GetSize()
    {
        var width = PlayerPrefs.GetFloat("Width");
        var length = PlayerPrefs.GetFloat("Length");

        size.x = width;
        size.z = length;
    }

    #endregion

   
}
