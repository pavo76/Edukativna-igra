using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoard : MonoBehaviour {

    bool enteredBoundary;
    bool readingActive;
    public GameObject BoardBackground;
    public GUIStyle style;
	
	// Use this for initialization
	void Start () {
		enteredBoundary=false;
		readingActive=false;
		BoardBackground.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (enteredBoundary && !readingActive)
        {
            if (Input.GetKeyDown(KeyCode.Return) ||Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                readingActive = true;
                BoardBackground.SetActive(true);
            }
        }

        if (enteredBoundary && readingActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                readingActive = false;
                BoardBackground.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enteredBoundary = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enteredBoundary = false;
            if (BoardBackground)
            {
                BoardBackground.SetActive(false);
                readingActive = false;
            }
        }
    }


    void OnGUI()
    {
        if (enteredBoundary && !readingActive)
        {            
            GUI.Label(new Rect(Screen.width/2-135, Screen.height/15-10, 270, 20), "Pritisni Enter za čitanje!", style);
        }

        if (enteredBoundary && readingActive)
        {
            GUI.Label(new Rect(Screen.width / 2 - 135, Screen.height / 15-10, 270, 20), "Pritisni Escape za izlazak!", style);
        }
    }
}
