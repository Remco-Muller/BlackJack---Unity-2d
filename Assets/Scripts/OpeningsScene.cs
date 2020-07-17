using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class OpeningsScene : MonoBehaviour
{
    private Image imageSrc;
    private bool opening = false;

    private Vector2 newPos;
    private Vector2 oldPos;


    private float StartTime;

    public float MovingTime;

    void Start()
    {
        imageSrc = GetComponent<Image>();
        StartTime = Time.time;

        newPos = new Vector2(0f,0f);
        oldPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        opening = true;
    }

   
    void Update()
    {
        if (opening && GameObject.Find("_Manager").GetComponent<Manager>().OpeningsScene)
        {
            if ((Time.time - StartTime) / MovingTime < 1)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(oldPos, newPos, (Time.time - StartTime) / MovingTime);            }
            else
            {
                opening = false;
                
                StartCoroutine(DestroyThis());
            }
        }
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        yield return null;
    }
}
