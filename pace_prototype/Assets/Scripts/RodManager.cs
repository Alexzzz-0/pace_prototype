using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RodManager : MonoBehaviour
{
    //[SerializeField] private Image _rod;
    [SerializeField] private GameObject _rodHolder;

    [SerializeField] private GameObject _lineHolder;
    //[SerializeField] private Image _line;

    [SerializeField] private GameObject _fishHolder;
    public float endValue = 100f;

    
    private void Start()
    {
        // _rodHolder.GetComponent<RectTransform>().DOAnchorPos(new Vector2(160f, 25f), .1f).OnComplete(() =>
        // {
        //     _rodHolder.transform.DORotate(new Vector3(0, 0, 30), 2f);
        // });
        
        
        _lineHolder.transform.DOScale(new Vector3(0, 0, 0), .1f).OnComplete(() =>
        {
            _rodHolder.transform.DORotate(new Vector3(0, 0, 50), 2f).OnComplete(() =>
            {
                _lineHolder.transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete(() =>
                {
                    _fishHolder.transform.DOMoveY(endValue, 2f);
                });
            });
        });

        //_lineHolder.transform.DOMove(new Vector3(5, 5,0), 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
