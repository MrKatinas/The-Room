using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour {
    #region Fields

    private static readonly string path_Prefab_Room = "Prefabs/Room";
    private static readonly string path_Prefab = "Prefabs";

    [Header("Size Settings")]
    [Tooltip("Change Room size, come from User inputs")]
    [SerializeField]
    private Vector3 size = new Vector3(2, 1, 2);

    [Header("Level Settings")]
    [Tooltip("Main Menu Scene name")]
    [SerializeField]
    private string levelName;

    #endregion

    #region Referencees

    private GameObject Prefab_Room;
    private LevelManager levelManager;

    #endregion

    #region Unity methods

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (levelManager)
        {
            SetupButton();
            Destroy(this);
        }
        else
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }

    #endregion

    #region Button setup

    public void SetupButton()
    {
        var button = FindObjectOfType<Button>();
        button.onClick.AddListener(this.ButtonClicked);
    }

    public void ButtonClicked()
    {
        levelManager.LoadLevel(levelName);
    }

    #endregion
}
