﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    #region Fields

    [SerializeField] private float defaultLength = 2f;

    [SerializeField] private float defaultWidth = 2f;

    #endregion

    #region Referencees

    [SerializeField] private InputField Length;

    [SerializeField] private InputField Width;

    #endregion

    #region Unity methods

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }

    #endregion

    #region Scene/Level Management

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Created Only for Create Button in Room Creation scene
    /// Change Level And save all inputs
    /// </summary>
    public void Change_Level_And_Save_Data(string name)
    {
        PlayerPrefs.SetFloat("Length", Length.text != String.Empty ? float.Parse(Length.text) : defaultLength);
        PlayerPrefs.SetFloat("Width", Width.text != String.Empty ? float.Parse(Width.text) : defaultWidth);

        LoadLevel(name);
    }

    #endregion
}
