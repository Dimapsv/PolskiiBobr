using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Transform[] brevnoSpawnPoints;
    public int brevnoIsDelivered;
    public TextMeshProUGUI brevnoCountText;
    public GameObject brevno;
    private Transform spawnPointBrevno;
    public bool brevnoIsTaked;


    private void Awake()
    {
        brevnoIsDelivered = 0;
        SpawnBrevno();
        
    }


    public void SpawnBrevno()
    {
        spawnPointBrevno = brevnoSpawnPoints[Random.Range(0, brevnoSpawnPoints.Length)];
        Instantiate(brevno, spawnPointBrevno);
        brevnoIsTaked = false;
        TextBrevnoUpdate();
    }


    public void TextBrevnoUpdate()
    {
        brevnoCountText.text = "" + brevnoIsDelivered;
    }
    



}
