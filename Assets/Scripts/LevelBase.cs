﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelBase : MonoBehaviour
{

    private int BPM = LevelEditor.BPM;
    private string SongName = LevelEditor.SongChoice;
    private int Difficulty = LevelEditor.Difficulty;
    private float DistanceBetweenNotes;
    AudioSource audioSource;
    public string LevelName { get; set;}

    // Use this for initialization
    void Start () {

        // Calculating based off of formula in README
        int FallSpeed = CalculateFallSpeed(Difficulty);
        double BPS = (double)BPM / (double)60;
        DistanceBetweenNotes = (float) (1 / (BPS / FallSpeed));

        // Getting correct audiosource and length in seconds
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(SongName);

        // length of audio in seconds
        double length = audioSource.clip.length;

        for (float i = DistanceBetweenNotes * 2; i < length; i = i + DistanceBetweenNotes)
        {
            GameObject note = Instantiate(Resources.Load("Prefabs/Basic Notes/Base Note")) as GameObject;
            note.transform.Translate(0 , i, 0);
        }

        //Vector3 x = Input.mousePosition;
        //Vector2 scroll = Input.mouseScrollDelta;

        // Naming the new level, making sure no duplicates
        LevelName = "Custom Level: " + SongName + " - " + LevelName;
        string BaseName = LevelName;
        int count = 1;
        while (CustomLevelData.LevelData.CustomLevels.IndexOf(LevelName) != -1)
        {
            LevelName = BaseName + " " + count;
            count++;
        }

    }
	

	void OnUpdate () {

        //GameObject[] AllNotes = GameObject.FindGameObjectsWithTag("BaseNote");
        //foreach (GameObject o in AllNotes)
        //{
        //    Vector3 pos = o.transform.position;
        //    pos.y += Input.GetAxis("Vertical") * 1;
        //    o.transform.Translate(pos);

        //}

    }

    private int CalculateFallSpeed(int diff) 
    {
        int FallSpeed;
        if (diff == 0)
        {
            FallSpeed = 5;
            LevelName = "Easy";
        }
        else if (diff == 1)
        {
            FallSpeed = 4;
            LevelName = "Medium";
        }
        else
        {
            FallSpeed = 3;
            LevelName = "Hard";
        }

        return FallSpeed;
    }

    public void SaveCurrentLevel() 
    {
        CustomLevelData.LevelData.CustomLevels.Add(LevelName);

        // Add all the current notes to a GameObject
        GameObject[] notes;
        notes = GameObject.FindGameObjectsWithTag("BaseNote");

        NoteData[] NoteDatas = new NoteData[notes.Length];
        int count = 0;
        foreach(GameObject note in notes){
            Vector3 position = note.transform.position;
            Quaternion rotation = note.transform.rotation;
            Vector3 scaling = note.transform.localScale;
            NoteData data = new NoteData(position.x, position.y, position.z,
                                         rotation.x, rotation.y, rotation.z, rotation.w,
                                         scaling.x, scaling.y, scaling.z);
            NoteDatas[count] = data;
            count++;
        }

        CustomLevelData.LevelData.SongNames.Add(SongName);
        CustomLevelData.LevelData.Notes.Add(NoteDatas);

        // Save the newly updated data
        CustomLevelData.LevelData.Save();

        // Go back to the Level Select
        SceneManager.LoadScene("Custom Level Select");


    }



}
