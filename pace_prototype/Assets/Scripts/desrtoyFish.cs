using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desrtoyFish : MonoBehaviour
{
    [SerializeField] private GameManager _gamemanager;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            Destroy(col.gameObject);
            _gamemanager.DisplayOutcome();
        }
    }
}
