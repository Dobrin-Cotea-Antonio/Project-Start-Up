using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourWithPause{
    public static GameManager gameManager { get; private set; }

    public static bool gameIsPaused { get; set; }

    public GameObject player { get; set; }

    public int permanentCash { get; set; }
    public int levelCash { get; set; }

    private void Awake(){
        if (gameManager != null)
            Destroy(gameObject);
        else{
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
    }

    private void Start(){
        levelCash = 1000;
    }
}
