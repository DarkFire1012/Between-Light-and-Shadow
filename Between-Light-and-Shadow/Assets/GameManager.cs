using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject NormalWorld;
    public GameObject InvertedWorld;
    public bool Inverted = false;

    public void InvertWorlds()
    {
        if (Inverted)
        {
            NormalWorld.SetActive(false);
            InvertedWorld.SetActive(true);
        }
        else
        {
            NormalWorld.SetActive(true);
            InvertedWorld.SetActive(false);
        }

    }








}
