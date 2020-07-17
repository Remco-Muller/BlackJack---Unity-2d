using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    private CardSuits Shape;
    private int Value;
    private int posX = 0;
    private int posY = 0;
    private Sprite sprite;
    private int PlayerID;
    private bool GameReset = false;

    private int RandomSeed1;
    private int RandomSeed2;

    private Vector2 newPos;
    private Vector2 oldPos;

    private float StartTime;
    private float MoveTime = 0.1f;

    private bool MoveReset;

    private GameObject _Manager;
    
    void Start()
    {
       
    }
    void Update()
    {
        if (MoveReset)
        {
            
                if ((Time.time - StartTime) / MoveTime < 1)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(oldPos, newPos, (Time.time - StartTime) / MoveTime);
            }
            else
            {
                MoveReset = false;
                if (GameReset)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }
    public CardObject()
    {
        System.Random rnd = new System.Random();
        Value = rnd.Next(0,13);
        RandomSeed1 = rnd.Next(-1000, 1000);
        RandomSeed2 = rnd.Next(-1000, 0);

        switch (rnd.Next(1,5))
        {
            default:
            case (1):
                Shape = CardSuits.Clubs;
                break;
            case (2):
                Shape = CardSuits.Diamonds;
                break;
            case (3):
                Shape = CardSuits.Hearts;
                break;
            case (4):
                Shape = CardSuits.Spades;
                break;
        }
        

    }
    public CardSuits shape
    {
        get { return Shape; }
    }
    public int value
    {
        get { return Value; }
        set { Value = value; }
    }
    public int randomSeed1
    {
        get { return RandomSeed1; }
    }
    public int randomSeed2
    {
        get { return RandomSeed2; }
    }
    public float moveTime
    {
        get { return MoveTime; }
        set { MoveTime = value; }
    }
    public int posx
    {
        get { return posX; }
        set 
        {
            oldPos = new Vector2(posX, posY);
            posX = value;
            updateGFX(posX, posY);
        }
    }
    public int posy
    {
        get { return posY; }
        set {
            oldPos = new Vector2(posX, posY);
            posY = value;
            updateGFX(posX, posY);
        }
    }
    public void updateGFX(int x, int y)
    {
        newPos = new Vector2((float)x, (float)y);
        MoveReset = true;
        StartTime = Time.time;
    }
    public Sprite CardSprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
    public int playerID
    {
        get { return PlayerID; }
        set { PlayerID = value; }
    }
    public bool gameReset
    {
        get { return GameReset; }
        set { GameReset = value; }
    }
    

    public void Initialize(int NumberOfCards)
    {
        _Manager = GameObject.Find("_Manager");
        System.Random rnd = new System.Random();
        _Manager.GetComponent<Manager>().cardSound(rnd.Next(1, 4));
        if ((PlayerID == 1) || (NumberOfCards != 0))
        {
            CardSprite = _Manager.GetComponent<Manager>().setSprite(Value, Shape.ToString());
        }
        else
        {
            CardSprite = _Manager.GetComponent<Manager>().DefaultCard();
        }
        
        posX = (NumberOfCards * 50);

        int val = (Value + 2);
        if ((val <= 10) || (val == 11 && val + _Manager.GetComponent<Manager>().TotalPoints(PlayerID) <= 21))
        {
            Value = val;
        }
        else if (val > 11)
        {
            Value = 10;
        }
        else if (val == 11)
        {
            Value = 1;
        }
    }
    public void UpdateSprite()
    {
        gameObject.GetComponentInParent<Image>().sprite = sprite;
    }

}
public enum CardSuits
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

