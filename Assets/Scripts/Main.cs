using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject Background, Maincanvas, Settingscanvas;

    private Vector3 startPos;
    private Vector3 endPos;

    private float MoveTime = 5f;
    private bool Direction = false;
    private float StartTime;

    void Start()
    {
        startPos = Background.transform.localScale;
        endPos.x = 1.03f;
        endPos.y = 1.03f;
        endPos.z = startPos.z;
        StartTime = Time.time;
    }
    void Update()
    {
        if ((Time.time - StartTime) / MoveTime < 1)
        {
            Background.transform.localScale = Vector3.Lerp(startPos, endPos, (Time.time - StartTime) / MoveTime);
        }
        else
        {
            startPos = Background.transform.localScale;
            endPos.x = Direction ? 1.03f : 1f;
            endPos.y = Direction ? 1.03f : 1f;
            endPos.z = startPos.z;
            StartTime = Time.time;
            Direction = !Direction;
        }
    }
    public void MenuButtonDecider(int i)
    {
        switch(i)
        {
            default:
            case (0):
                SceneManager.LoadScene("Game");
                break;
            case (1):
                Application.Quit();
                break;
            case (2):
                Maincanvas.SetActive(false);
                Settingscanvas.SetActive(true);
                break;
        }
    }
    public void cancelBtn()
    {
        Maincanvas.SetActive(true);
        Settingscanvas.SetActive(false);
    }
    public void saveBtn()
    {
        

    }
}
