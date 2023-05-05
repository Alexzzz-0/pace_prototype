using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEst : MonoBehaviour
{
    [SerializeField] private GameObject _test;
    [SerializeField] private GameObject _square;
    public Vector2 blendBy;
    public Vector2 blendBy2;
    private void Start()
    {
        Debug.Log("blend");
        _test.transform.DOBlendableRotateBy(new Vector3(0,0,5f), 1f);
        _square.transform.DOBlendableLocalRotateBy(new Vector3(0, 0, -15f), 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(2);
        }
    }
}
