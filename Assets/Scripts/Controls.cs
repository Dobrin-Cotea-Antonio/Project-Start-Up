using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviourWithPause
{
    public static Controls controls { get; private set; }

    public Dictionary<string, KeyCode> keyList { get; set; }

    void Awake()
    {
        if (controls != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            controls = this;

            keyList = new Dictionary<string, KeyCode>();
            //movement
            keyList.Add("dash", KeyCode.Space);//new key
            //interaction
            keyList.Add("interact", KeyCode.F);
            keyList.Add("interact2", KeyCode.G);
            keyList.Add("pause", KeyCode.Tab);
            //combat
            keyList.Add("shoot", KeyCode.Mouse0);
            keyList.Add("aim", KeyCode.Mouse1);
            keyList.Add("ability1", KeyCode.Q);
            keyList.Add("ability2", KeyCode.E);
        }

    }

}