using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Sprite[] Diamonds, Spades, Hearts, Clubs;
    public Sprite BackFacedCard;

    public GameObject _PlayingField, _Player, _PlayerField, _Points, _DealerAI, _DealerField, _HtnBtn, _StayBtn, _ResetBtn, _DealerPoints, _WinText, _SoundManager, _DoubleDown;
    private Player User_Player, Dealer_AI;

    private bool DDown = false;
    public bool OpeningsScene = true;

    void Start()
    {
        User_Player = _Player.GetComponent<Player>();
        Dealer_AI = _DealerAI.GetComponent<Player>();
        User_Player.initialize();
        Dealer_AI.initialize();
        HideUIButtons(false);
        StartCoroutine(WaitingStart());
    }
    IEnumerator WaitingStart()
    {
        if (OpeningsScene)
        {
            SoundManagerScript.Sound("intro");
            yield return new WaitForSeconds(10);
            AfterOpening();
        }
        else
        {
            AfterOpening();
        }
        
        yield return null;
    }
    public void NewCard(int id)
    {
        _DoubleDown.SetActive(false);
        GameObject imgObject = new GameObject("Card");
        CardObject card = imgObject.AddComponent<CardObject>();

        GameObject _CardOwner = id == 1 ? _Player : _DealerAI;
        GameObject _PlaceField = _CardOwner == _Player ? _PlayerField : _DealerField;

        card.playerID = _CardOwner.GetComponent<Player>().playerID;
        card.Initialize(_CardOwner.GetComponent<Player>().NumberOfCards());

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(_PlaceField.transform); 
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(card.posx,card.posy); 
        trans.sizeDelta = new Vector2(150, 200); 

        Image image = imgObject.AddComponent<Image>();
        image.sprite = card.CardSprite;
        imgObject.transform.SetParent(_PlaceField.transform);

        _CardOwner.GetComponent<Player>().SetCards(card);
        _CardOwner.GetComponent<Player>().Totalvalue = card.value;
        Calculate(_CardOwner);

    }
    public Sprite setSprite(int x, string Suit)
    {
        switch (Suit)
        {
            case ("Hearts"):
                return Hearts[x];
            case ("Diamonds"):
                return Diamonds[x];
            case ("Clubs"):
                return Clubs[x];
            case ("Spades"):
                return Spades[x];
        }
        return null;
        
    }
    public Sprite DefaultCard()
    {
        return BackFacedCard;
    }
    IEnumerator OpeningDraws(int AmountOfCards)
    {
        if (AmountOfCards < 4)
        {
            yield return new WaitForSeconds(1);
            NewCard(AmountOfCards % 2 == 1 ? 1:2);
            StartCoroutine(OpeningDraws(AmountOfCards += 1));
        }
        else
        {
            HideUIButtons(true);
            if (User_Player.Totalvalue >= 9 && User_Player.Totalvalue <= 11)
            {
                _DoubleDown.SetActive(true);
                SoundManagerScript.Sound("doubledownbtn");
            }
        }
        yield return null;
    }
    public void Stand()
    {
        _Points.GetComponent<Text>().text = "You " + User_Player.Totalvalue;
        HideUIButtons(false);
        StartCoroutine(StandTime());
    }
    IEnumerator StandTime()
    {
        yield return new WaitForSeconds(1);
        Dealer_AI.TurnCard();
        _DealerPoints.SetActive(true);
        StartCoroutine(DealerMath());
        yield return null;
    }
    IEnumerator DealerMath()
    {
        
        if (Dealer_AI.Totalvalue < 18)
        {
            yield return new WaitForSeconds(1);
            NewCard(2);
            StartCoroutine(DealerMath());
        }
        else
        {
            if (Dealer_AI.Totalvalue == 21)
            {
                otherSounds("dealerblackjack");
                yield return new WaitForSeconds(2);
            }

            if (Dealer_AI.Totalvalue > User_Player.Totalvalue && Dealer_AI.Totalvalue <=  21)
            {
                _WinText.GetComponent<Text>().text = "You lose";
                otherSounds("lose");
            }
            else if (Dealer_AI.Totalvalue < User_Player.Totalvalue || Dealer_AI.Totalvalue > 21)
            {
                _WinText.GetComponent<Text>().text = "You Win";
                otherSounds("win");
            }
            else
            {
                _WinText.GetComponent<Text>().text = "Tie";
                otherSounds("tie");
            }
            _WinText.SetActive(true);
            _ResetBtn.SetActive(true);
        }
        yield return null;
    }
    public int TotalPoints(int id)
    {
        GameObject _CardOwner = id == 1 ? _Player : _DealerAI;
        return _CardOwner.GetComponent<Player>().Totalvalue;
    }
    public void Calculate(GameObject player)
    {
        if (player.GetComponent<Player>().Totalvalue > 21)
        {
            player.GetComponent<Player>().Recalculate();
        }


        if (player.GetComponent<Player>().PlayerID == 1)
        {
            _Points.GetComponent<Text>().text = player.GetComponent<Player>().Totalvalue.ToString();
        }
        else
        {
            _DealerPoints.GetComponent<Text>().text = "AI " + Dealer_AI.Totalvalue;
        }
        if (player.GetComponent<Player>().Totalvalue > 21)
        {
            HideUIButtons(false);
            if (player.GetComponent<Player>().PlayerID == 1)
            {
                _Points.GetComponent<Text>().text = "Busted";
                _WinText.SetActive(true);
                _WinText.GetComponent<Text>().text = "You Lose";
                otherSounds("lose");
            }
            else
            {
                _DealerPoints.GetComponent<Text>().text = "Busted";
            }

            _ResetBtn.SetActive(true);
        } else if (player.GetComponent<Player>().Totalvalue == 21 && player.GetComponent<Player>().PlayerID == 1)
        {
            otherSounds("playerblackjack");
            if (DDown)
            {
                Stand();
            }
        }
        else
        {
            if (DDown && player.GetComponent<Player>().PlayerID == 1)
            {
                Stand();
            }
        }

    }
    public void HideUIButtons(bool x)
    {
        _HtnBtn.SetActive(x);
        _StayBtn.SetActive(x);
        _DoubleDown.SetActive(false);
    }
    public void OnReset()
    {
        StartCoroutine(onReset());
    }
    IEnumerator onReset()
    {
        _WinText.SetActive(false);
        _DealerPoints.SetActive(false);
        _ResetBtn.SetActive(false);
        Dealer_AI.ResetCards();
        User_Player.ResetCards();
        DDown = false;
        SoundManagerScript.Sound("resetsound");
        yield return new WaitForSeconds(2);
        StartCoroutine(OpeningDraws(0));
        yield return null;
    }
    public void cardSound(int rnd)
    {
        SoundManagerScript.Sound("card" + rnd);
    }
    public void otherSounds(string sound)
    {
        SoundManagerScript.Sound(sound);
    }
    public void AfterOpening()
    {
        StartCoroutine(OpeningDraws(0));
        SoundManagerScript.Sound("Background");
    }
    public void DoubleDown()
    {
        DDown = true;
        SoundManagerScript.Sound("doubledown");
        StartCoroutine(DoubleDownAddCard());
    }
    IEnumerator DoubleDownAddCard()
    {
        yield return new WaitForSeconds(0.8f);
        NewCard(1);
        yield return null;
    }

}
