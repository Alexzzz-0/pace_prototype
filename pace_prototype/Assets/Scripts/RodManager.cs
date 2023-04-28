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
    [SerializeField] private Image _fish;
    public float endValue = 100f;

    public float _rodTime = 2f;
    public float _lineTime = 2f;
    public float _fishTime = 4f;
    public float _changeTime = .2f;

    public float count = 0;

    private bool _canPlay = true; //if it is true, can cast the rod
    private bool _canRestart = false; //if it is true, can restart the pos
    private bool _rodCastDone = false; //use it to check if the function has finished
    private bool _lineexpandDone = false; //same
    private bool _catchfishDone = false; //same
    
    private void Start()
    {
        #region test
        
        // _rodHolder.GetComponent<RectTransform>().DOAnchorPos(new Vector2(160f, 25f), .1f).OnComplete(() =>
        // {
        //     _rodHolder.transform.DORotate(new Vector3(0, 0, 30), 2f);
        // });
        
        //_lineHolder.transform.DOMove(new Vector3(5, 5,0), 2f);

        #endregion

        #region working loop

        // _lineHolder.transform.DOScale(new Vector3(0, 0, 0), .1f).OnComplete(() =>
        // {
        //     _rodHolder.transform.DORotate(new Vector3(0, 0, 50), 2f).OnComplete(() =>
        //     {
        //         _lineHolder.transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete(() =>
        //         {
        //             _fishHolder.transform.DOMoveY(endValue, 2f);
        //         });
        //     });
        // });

        #endregion
        
        //set everything to initial settings
        _lineHolder.transform.DOScale(new Vector3(0, 0, 0), .1f);
        _changeTime = 0;
        _fishHolder.transform.DOMoveY(90, .1f);
    }

    
    
    private void Update()
    {
        
        if (_canPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canPlay = false;
                Debug.Log(count.ToString());

                _changeTime = .2f * count;

                RotateRod();
                Invoke("ExpandLine", _rodTime + _changeTime);
                Invoke("CatchFish", _rodTime + _lineTime - _changeTime);

                count += 1;
            }
        }

        //if there is no executing play, can restart
        if (_rodCastDone && _lineexpandDone && _catchfishDone)
        {
            _canRestart = true;
        }
        
        if (_canRestart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    void RotateRod()
    {
        _rodHolder.transform.DORotate(new Vector3(0, 0, 50), _rodTime);
        _rodCastDone = true;
    }

    void ExpandLine()
    {
        _lineHolder.transform.DOScale(new Vector3(1, 1, 1), _lineTime);
        _lineexpandDone = true;
    }

    void CatchFish()
    {
        _fishHolder.transform.DOMoveY(endValue, _fishTime);
        _catchfishDone = true;
    }

    void Restart()
    {
        //TODO: replace them with DORestart method;
        // _rodHolder.transform.DORestart();
        // _lineHolder.transform.DORestart();
        // _fishHolder.transform.DORestart();
        
        //the animation of ting everything to the initial settings
        _fish.DOFade(0, .1f).OnComplete(() =>
        {
            _lineHolder.transform.DOScale(new Vector3(0, 0, 0), .1f).OnComplete(() =>
            {
                _rodHolder.transform.DORotate(new Vector3(0, 0, 0), .1f).OnComplete(() =>
                {
                    _fishHolder.transform.DOMoveY(90, .5f).OnComplete(() =>
                    {
                        _fish.DOFade(1, 1f).OnComplete(() =>
                        {
                            _canPlay = true;
                        });
                    });
                });
            });
        });

        _canRestart = false;
        _rodCastDone = false;
        _lineexpandDone = false;
        _catchfishDone = false;
    }
}
