using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int TotalValue = 0;
    private List<CardObject> AllCards;

    public int PlayerID;

    public void initialize()
    {
        AllCards = new List<CardObject>();
    }
    public void initialize(List<CardObject> Sprites)
    {
        AllCards = Sprites;
    }
    public List<CardObject> GetCards
    {
        get { return AllCards; }
    }
    public void SetCards(CardObject x)
    {
        AllCards.Add(x);
        for (int count = 0; count < AllCards.Count; count++)
        {
            AllCards[count].posx -= 50;
        }
    }
    public int Totalvalue
    {
        get { return TotalValue; }
        set { TotalValue += value; }
    }
    public int playerID
    {
        get { return PlayerID; }
        set { PlayerID = value; }
    }
    public int NumberOfCards()
    {
        return AllCards.Count;
    }
    public void ResetCards()
    {
        foreach (CardObject x in AllCards)
        {
            x.gameReset = true;
            x.moveTime = 2.8f;
            switch (PlayerID)
            {
                case (1):
                    x.updateGFX(x.randomSeed1,Math.Abs(x.randomSeed2));
                    break;
                case (2):
                    x.updateGFX(x.randomSeed1, x.randomSeed2);
                    
                    break;
            }
        }
        AllCards.Clear();
        TotalValue = 0;
    }
    public void TurnCard()
    {
        AllCards[0].CardSprite = GameObject.Find("_Manager").GetComponent<Manager>().setSprite(AllCards[0].value - 2, AllCards[0].shape.ToString());
        AllCards[0].UpdateSprite();
    }
    public int Recalculate()
    {
        TotalValue = 0;
        foreach (CardObject x in AllCards)
        {
            if (x.value == 11)
            {
                x.value = 1;
            }
            TotalValue += x.value;
        }
        return TotalValue;
    }
    
}
