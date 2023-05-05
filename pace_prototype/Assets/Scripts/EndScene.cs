using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] private TextMeshPro _end;
    [SerializeField] private GameObject _black;
    
    public float stayTime = 1f;

    private void Start()
    {
        _black.transform.position = new Vector3(-4.5f, 0, 0);
        _end.transform.position = new Vector3(-4.5f, 0, 0);
        _end.text = "Made by Alex";
        Invoke("Devinne",stayTime);
    }

    void Devinne()
    {
        _black.transform.position = new Vector3(4.5f, 0, 0);
        _end.transform.position = new Vector3(4.5f, 0, 0);
        _end.text = "Audio by Devinne";
        Invoke("Instructor",stayTime);
    }

    void Instructor()
    {
        _black.transform.position = new Vector3(-4.5f, 0, 0);
        _end.transform.position = new Vector3(-4.5f, 0, 0);
        _end.text = "Instructor: Jeff & Lawra";
        Invoke("Playtesting",stayTime);
    }

    void Playtesting()
    {
        _black.transform.position = new Vector3(4.5f, 0, 0);
        _end.transform.position = new Vector3(4.5f, 0, 0);
        _end.text = "Thx for playtesting and advice."+"\n"+"Thx Team Schist."+"\n"+"\n"+"Background video from youtube @Drew Medina";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(0);
        }
    }
}
