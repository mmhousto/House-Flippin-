using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance;} }

    public int level = 1;
    public GameObject[] cams;
    public bool[] housesFlipped;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    public void NextLevel()
    {
        level++;
        cams[level-2].SetActive(false);
        cams[level-1].SetActive(true);
    }


}
