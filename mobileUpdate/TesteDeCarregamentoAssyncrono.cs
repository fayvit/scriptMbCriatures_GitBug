﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TesteDeCarregamentoAssyncrono : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync("Infinity2Teste",LoadSceneMode.Additive);
        }
    }
}
