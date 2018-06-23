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
            InstantiateRoomTextures();
        }
        else
        {
            Debug.LogError("Nebuvo surastas kambarys");
        }
    }

    private void InstantiateRoomTextures()
    {
        foreach (var child in FindObjectOfType<Room>().GetComponentsInChildren<Transform>().Skip(1))
        {
            Vector2 scale;
            Debug.Log(child.name);
            Debug.Log(child.eulerAngles);


            if (!(child.localEulerAngles.x == 270.0f))
            {
                scale = new Vector2(size.x, size.z);
            }
            else if (child.localEulerAngles.y == 0f || child.localEulerAngles.y == 180.0f)
            {
                scale = new Vector2(size.x, 1f);
            }
            else
            {
                scale = new Vector2(size.z, 1f);
            }

            child.gameObject.GetComponent<MeshRenderer>().material.mainTextureScale = scale;
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
