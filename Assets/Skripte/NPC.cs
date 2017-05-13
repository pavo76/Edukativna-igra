using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    bool enteredBoundary;
    bool readingActive;
    public GUIStyle style;
    public GameObject DialogueBackground;
    public GameObject PictureBacgorund;
    public GameObject[] Dialogue;
    private int NextDialogueIndex;
    // Use this for initialization
    void Start()
    {
        enteredBoundary = false;
        readingActive = false;
        DialogueBackground.SetActive(false);
        PictureBacgorund.SetActive(false);
        foreach (GameObject dialoguePart in Dialogue)
        {
            dialoguePart.SetActive(false);
        }
        NextDialogueIndex = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (enteredBoundary && !readingActive)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                readingActive = true;
                DialogueBackground.SetActive(true);
                Dialogue[NextDialogueIndex].SetActive(true);
                var pictureChecker= Dialogue[NextDialogueIndex].GetComponent<HasPictureCheck>();
                if (pictureChecker.hasPicture)
                {
                    PictureBacgorund.SetActive(true);
                }
                NextDialogueIndex += 1;
            }
        }

        else if (enteredBoundary && readingActive)
        {
            if (NextDialogueIndex < Dialogue.Length)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    var Picture_Checker = Dialogue[NextDialogueIndex - 1].GetComponent<HasPictureCheck>();
                    if (Picture_Checker.hasPicture)
                    {
                        PictureBacgorund.SetActive(false);
                    }
                    Dialogue[NextDialogueIndex - 1].SetActive(false);
                    Picture_Checker = Dialogue[NextDialogueIndex].GetComponent<HasPictureCheck>();
                    if (Picture_Checker.hasPicture)
                    {
                        PictureBacgorund.SetActive(true);
                    }
                    Dialogue[NextDialogueIndex].SetActive(true);
                    NextDialogueIndex += 1;
                }
            }

            else if (NextDialogueIndex >= Dialogue.Length)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    readingActive = false;
                    DialogueBackground.SetActive(false);
                    Dialogue[NextDialogueIndex - 1].SetActive(false);
                    var Picture_Checker = Dialogue[NextDialogueIndex - 1].GetComponent<HasPictureCheck>();
                    if (Picture_Checker.hasPicture)
                    {
                        PictureBacgorund.SetActive(false);
                    }
                    NextDialogueIndex = 0;
                }
            }
        }

        if (enteredBoundary && readingActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                readingActive = false;
                DialogueBackground.SetActive(false);
                Dialogue[NextDialogueIndex - 1].SetActive(false);
                var Picture_Checker = Dialogue[NextDialogueIndex - 1].GetComponent<HasPictureCheck>();
                if (Picture_Checker.hasPicture)
                {
                    PictureBacgorund.SetActive(false);
                }
                NextDialogueIndex = 0;
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
            if (DialogueBackground)
            {
                DialogueBackground.SetActive(false);
                PictureBacgorund.SetActive(false);
                if (NextDialogueIndex > 0)
                {
                    Dialogue[NextDialogueIndex - 1].SetActive(false);
                }
                readingActive = false;
            }
        }
    }


    void OnGUI()
    {
        if (enteredBoundary && !readingActive)
        {

            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 15 - 10, 270, 20), "Pritisni Enter za čitanje!", style);
        }

        if (enteredBoundary && readingActive)
        {

            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 15 - 10, 270, 45), "Pritisni Enter za dalje!\nPritisni Escape za izlazak!", style);
        }
    }
}
