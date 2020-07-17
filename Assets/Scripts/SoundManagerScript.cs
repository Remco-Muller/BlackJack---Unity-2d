using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip CardPlace1, CardPlace2, CardPlace3, BackgroundAudio, lose, win, tie, welcome, openingSound, playerblackjack, dealerblackjack, playerhas, dealerhas, resetsound, doubledownbutton, doubledown;
    static AudioSource Audiosrc,BackGroundAudioSrc;
    void Start()
    {
        CardPlace1 = Resources.Load<AudioClip>("cardsound1");
        CardPlace2 = Resources.Load<AudioClip>("cardsound2");
        CardPlace3 = Resources.Load<AudioClip>("cardsound3");
        win = Resources.Load<AudioClip>("youwin");
        lose = Resources.Load<AudioClip>("youlose");
        tie = Resources.Load<AudioClip>("tie");
        resetsound = Resources.Load<AudioClip>("resetSound");
        openingSound = Resources.Load<AudioClip>("intro");
        BackgroundAudio = Resources.Load<AudioClip>("backgroundsound");
        playerblackjack = Resources.Load<AudioClip>("playerblackjack");
        dealerblackjack = Resources.Load<AudioClip>("dealerblackjack");
        doubledownbutton = Resources.Load<AudioClip>("doubledownbtn");
        doubledown = Resources.Load<AudioClip>("doubledown");

        Audiosrc = GetComponent<AudioSource>();
        BackGroundAudioSrc = GameObject.Find("PlayingField").GetComponent<AudioSource>();
    }
    public static void Sound(string sound)
    {
        switch (sound)
        {
            case ("card1"):
                Audiosrc.PlayOneShot(CardPlace1);
                break;
            case ("card2"):
                Audiosrc.PlayOneShot(CardPlace2);
                break;
            case ("card3"):
                Audiosrc.PlayOneShot(CardPlace3);
                break;
            case ("win"):
                Audiosrc.PlayOneShot(win);
                break;
            case ("lose"):
                Audiosrc.PlayOneShot(lose);
                break;
            case ("resetsound"):
                Audiosrc.PlayOneShot(resetsound);
                break;
            case ("tie"):
                Audiosrc.PlayOneShot(tie);
                break;
            case ("intro"):
                Audiosrc.PlayOneShot(openingSound);
                break;
            case ("playerblackjack"):
                Audiosrc.PlayOneShot(playerblackjack);
                break;
            case ("dealerblackjack"):
                Audiosrc.PlayOneShot(dealerblackjack);
                break;
            case ("doubledownbtn"):
                Audiosrc.PlayOneShot(doubledownbutton);
                break;
            case ("doubledown"):
                Audiosrc.PlayOneShot(doubledown);
                break;



            case ("Background"):
                BackGroundAudioSrc.loop = true;
                BackGroundAudioSrc.Play();
                break;
        }
    }
}
