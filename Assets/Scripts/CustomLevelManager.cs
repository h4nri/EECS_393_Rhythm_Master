﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomLevelManager : MonoBehaviour {

    Dropdown Dropdown;

    void Start()
    {
        Dropdown = GameObject.FindGameObjectWithTag("Custom Song Dropdown").GetComponent<Dropdown>();
        Dropdown.ClearOptions();
        Dropdown.AddOptions(CustomLevelData.LevelData.CustomLevels);
    }

    public void UpdateDropdown() 
    {
        Dropdown = GameObject.FindGameObjectWithTag("Custom Song Dropdown").GetComponent<Dropdown>();
        Dropdown.ClearOptions();
        Dropdown.AddOptions(CustomLevelData.LevelData.CustomLevels);
    }

}
