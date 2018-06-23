using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour {
    #region Fields

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
