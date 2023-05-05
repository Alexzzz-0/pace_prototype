using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class RodManager : MonoBehaviour
{
    //[SerializeField] private Image _rod;
    //[SerializeField] private GameObject _rodHolder;
    [SerializeField] private GameObject _bone1;
    [SerializeField] private GameObject _bone2;

    [SerializeField] private GameObject _lineHolder;
    //[SerializeField] private Image _line;

    [SerializeField] private GameObject _fishHolder;
    [SerializeField] private SpriteRenderer _fish;
    [SerializeField] private AudioSource _reel;
    [SerializeField] private VideoPlayer _video;

    //[SerializeField] private GameObject _seaHolder;

    //public float preRotAngel = 5f;
    
    public float endValue = 100f;
    private float endX = 477f;
    private float endY = 310f;

    public float _rodTime = 2f;
    private float _initRodTime;
    public float _lineTime = 1f;
    public float _fishTime = 4f;
    public float _changeTime = .2f;

    public float count = 0;

    private bool _canPlay = true; //if it is true, can cast the rod
    private bool _canRestart = false; //if it is true, can restart the pos
    private bool _rodCastDone = false; //use it to check if the function has finished
    private bool _lineexpandDone = false; //same
    private bool _catchfishDone = false; //same

    public Vector2 fishInitial;
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
        _fishHolder.transform.DOMove(fishInitial, .1f);

        _initRodTime = _rodTime;
        
        _reel.pitch = 5f;
        //JitterSea();
    }

    
    
    private void Update()
    {
        
        if (_canPlay)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (count <= 5){
                    //forbid to recall the function
                    _canPlay = false;
                    Debug.Log(count.ToString());

                    //change the time
                    _rodTime += 1f;
                    if (_fishTime > 1f)
                    {
                        _fishTime -= 1f;
                    }

                    _reel.pitch -= 0.7f;
                    Debug.Log(_rodTime.ToString() + _fishTime.ToString());

                    //call the function
                    PreCast();
                    
                    Invoke("ExpandLine", _initRodTime);
                    
                    //count down
                    count += 1;
                }else if (count >= 6 && count < 9)
                {
                    //change the speed
                    _reel.pitch += 0.1f;
                    _rodTime -= 3f;
                    glitchTime += 2f;

                    //call function
                    PreCast();
                    Invoke("GlitchLine", _initRodTime);
                    count += 1;
                }else if (count == 9)
                {
                    EndGame();
                }
            }
            
        }

        //check if the second period is done
        if (_rodCastDone && _catchfishDone)
        {
            _canRestart = true;
        }
        
        //pull the fish
        if (_canRestart)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Restart();
            }
        }
    }

    void PreCast()
    {
        _bone1.transform.DORotate(new Vector3(0, 0, 85f), 0.5f);
        _bone2.transform.DOBlendableLocalRotateBy(new Vector3(0, 0, -12f), 0.5f);
        _bone2.transform.DOScale(new Vector3(1, 1.2f, 1), 0.5f).OnComplete(() =>
        {
            if (count <= 5)
            {
                RotateRod();
            }

            if (count >= 6)
            {
                GlitchRod();
            }
        });
    }
    
    void RotateRod()
    {
        _reel.Play();

        _bone1.transform.DORotate(new Vector3(0, 0, 147f), _rodTime - 0.5f);
        _bone2.transform.DOScale(new Vector3(2.82f, 1f, 1f), _rodTime - 0.5f);
        _bone2.transform.DOBlendableLocalRotateBy(new Vector3(0, 0, 20f), _rodTime - 0.5f).OnComplete(() =>
        {
            
            _rodCastDone = true;
            
        });
    }
    
    float RotAngel;
    private float xScale;
    private float glitchTime;
    
    void GlitchRod()
    {
        //play the audio
        _reel.Play();
        
        switch (count)
        {
            case 6:
                RotAngel = 162.9f;
                xScale = 2.8f;
                break;
            case 7:
                RotAngel = 147.8f;
                xScale = 1.7f;
                break;
            case 8:
                RotAngel = 129.7f;
                xScale = 1.2f;
                break;
        }

        Camera.main.DOShakeRotation(glitchTime);

        _bone1.transform.DORotate(new Vector3(0, 0, 113f), 1f).OnComplete(() =>
        {
            Invoke("GlitchBone",glitchTime);
        });
        
        
    }

    void GlitchBone()
    {
        _bone1.transform.DORotate(new Vector3(0, 0, RotAngel),0.1f);
        _bone2.transform.DOScale(new Vector3(xScale, 1f, 1f), 0.1f).OnComplete(() =>
        {
            _rodCastDone = true;
        });
    }

    void EndGame()
    {
        _video.Play();
        Camera.main.DOOrthoSize(2.59f, 2f);
        Camera.main.transform.DOMove(new Vector3(3.88f, 2.79f), 2f);
        _bone1.transform.DORotate(new Vector3(0,0,85f), 4f);
        _bone2.transform.DOScale(Vector3.one, 2f);
        _bone2.transform.DOLocalRotate(Vector3.zero, 2f);

        Invoke("End",10f);
    }
    
    void ExpandLine()
    {
        _lineHolder.transform.DOScale(new Vector3(1, 1, 1), _lineTime).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CatchFish();
        });
        
    }

    void CatchFish()
    {
        // endValue += count * 5;
        //
        // if (endValue >= 310f)
        // {
        //     endValue = 310f;
        //     _fishTime *= .5f;
        //
        //     endX += (count - 7) * 5;
        //     endY -= (count - 7) * 4;
        //     
        //     _fishHolder.transform.DOMoveY(endValue, _fishTime).OnComplete(() =>
        //     {
        //         _fishHolder.transform.DORotate(new Vector3(0, 0, -120), _fishTime).SetEase(Ease.InBounce).OnComplete(
        //             () =>
        //             {
        //                 _fishHolder.transform.DOMove(new Vector3(endX, endY), _fishTime);
        //             });
        //     });
        // }
        // else
        // {
        //     _fishHolder.transform.DOMoveY(endValue, _fishTime);
        // }
        
        _fishHolder.transform.DOMoveY(endValue, _fishTime).OnComplete(() =>
        {
            _catchfishDone = true;
        });
        
        _video.Play();
        Invoke("PauseVideo",_fishTime);
    }

    void PauseVideo()
    {
        _video.Pause();
    }
    
    // void JitterSea()
    // {
    //     _seaHolder.transform.DOJump(new Vector3(430, 250, 0), count, 1,1f);
    // }

    private float lineX;
    void GlitchLine()
    {
        switch (count)
        {
            case 6:
                lineX = -2f;
                break;
            case 7:
                lineX = 0.16f;
                break;
            case 8:
                lineX = 2.95f;
                break;
        }

        _lineHolder.transform.DOMoveX(lineX, 0.1f);
        _lineHolder.transform.DOScale(new Vector3(1, 1, 1), _lineTime).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _fishHolder.transform.DOMoveX(lineX, 0.1f);
            CatchFish();
        });
    }
    
    void Restart()
    {
        _canRestart = false;
        _rodCastDone = false;
        //_lineexpandDone = false;
        _catchfishDone = false;
        
        //TODO: replace them with DORestart method;
        // _rodHolder.transform.DORestart();
        // _lineHolder.transform.DORestart();
        // _fishHolder.transform.DORestart();
        
        //the animation of ting everything to the initial settings
        _fish.DOFade(0, .1f).OnComplete(() =>
        {
            _fishHolder.transform.DOMove(fishInitial, .5f).OnComplete(() =>
            {
                _fish.DOFade(1, 1f).OnComplete(() =>
                {
                    _canPlay = true;
                });
            });
        });
            //_fishHolder.transform.DORotate(Vector3.zero, .5f);
            
            _lineHolder.transform.DOScale(new Vector3(0, 0, 0), .1f).OnComplete(() =>
            {
                // _rodHolder.transform.DORotate(new Vector3(0, 0, -10f), .5f);
                // _rodHolder.transform.DOScale(new Vector3(0.8f, 0.8f), 0.5f).OnComplete(() =>
                // {
                //     _rodHolder.transform.DORotate(Vector3.zero, 0.5f);
                //     _rodHolder.transform.DOScale(Vector3.one, 0.5f);
                // });
                _bone1.transform.DORotate(new Vector3(0, 0, 70f), 0.5f);
                _bone2.transform.DOScale(Vector3.one, 0.5f);
                _bone2.transform.DOLocalRotate(Vector3.zero, 0.5f);

                if (count < 9)
                {
                    Invoke("AfterRecover",0.5f);
                }
                else
                {
                    Invoke("FishCome",0.5f);
                }
            });
    }

    void AfterRecover()
    {
        _bone1.transform.DORotate(new Vector3(0,0,90f), 0.5f);
    }

    void FishCome()
    {
        _fishHolder.transform.DOMove(new Vector3(6.37f, 4.04f), 0.5f);
    }

    void End()
    {
        SceneManager.LoadScene(0);
    }
}
