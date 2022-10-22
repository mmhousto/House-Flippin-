using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int level = 1;
    public GameObject[] cams;

    public void NextLevel()
    {
        level++;
        cams[level-2].SetActive(false);
        cams[level-1].SetActive(true);
    }


}
