using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _rod;
    [SerializeField] private GameObject _bubble;
    [SerializeField] private TextMeshPro _display;
    [SerializeField] private GameObject _redBubble;
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _goodFish;
    [SerializeField] private GameObject _badFish;
    
    private bool onMove = false;
    public float rotTime = 2f;
    public float delayTime = 0;

    private GameObject Bubbles;

    private string displayText;

    private int count = 0;

    private void Start()
    {
        
    }

    private GameObject leftArrow;
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle = angleNum;
            Invoke("RotateRod",delayTime);
            
            
            
            //RotateRod();
        }

        if (!IsInvoking("RotateRod"))
        {
            RodUp();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            delayTime += .5f;
            angle += .2f;
            Debug.Log(delayTime.ToString());
            leftArrow = Instantiate(_leftArrow);
        }

        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     if (count <= 4)
        //     {
        //         delayTime += count;
        //         Invoke("SpawnBubble", 2f);
        //         Invoke("DestroyBubble", 5f);
        //         count += 1;
        //         displayText = "You Caught A Fish!";
        //         Invoke("DisplayOutcome", 5f);
        //     }
        //
        //     if (count == 5)
        //     {
        //         Invoke("SpawnRedBubble",2f);
        //         Invoke("DestroyBubble",5f);
        //         displayText = "!!!!!!";
        //         Invoke("DisplayOutcome", 5f);
        //     }
        // }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //RodUp();
           
        }

        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     angle = -1 * angleNum;
        //     Invoke("RotateRod",delayTime);
        // }
    }

    void RodUp()
    {
        _rod.DORotate(new Vector3(0, 0, 0), 0.5f * rotTime);
        _rod.DOMove(new Vector3(3.1f, 1.61f, 0), 0.5f * rotTime);
        Destroy(leftArrow);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        if (col.gameObject.tag == "Player")
        {
            if (count <= 4)
            {
                count += 1;
                SpawnBubble();
                
            }

            if (count >= 5)
            {
                SpawnRedBubble();
                
            }
        }
        
    }

    public float angleNum = 0.2f;
    private float angle;
    
    void RotateRod()
    {
        
        _rod.RotateAround(new Vector3(7.3f,-0.37f),new Vector3(0,0,1),angle);
        if (transform.rotation.z >= 63)
        {
            return;
        }
        // _rod.DORotate(new Vector3(0, 0, 23), rotTime).SetDelay(delayTime);
        // _rod.DOMove(new Vector3(2.41f, -0.14f, 0), rotTime).SetDelay(delayTime);
         // _rod.DORotate(new Vector3(0, 0, 0), 0.5f * rotTime);
         // _rod.DOMove(new Vector3(3.1f, 1.61f, 0), 0.5f * rotTime);
    }

    void SpawnBubble()
    {
        Bubbles = new GameObject("Bubbles");
        for (int i = 0; i <20; i++)
        {
            GameObject bubble = Instantiate(_bubble);
            bubble.transform.position = new Vector3(-0.82f+Random.Range(-0.5f,2f), -6.2f+Random.Range(-1f,1.5f));
            bubble.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-0.3f, -0.1f);
            bubble.transform.parent = Bubbles.transform;
        }
        
        Invoke("DestroyBubble",2f);
        Invoke("DisplayOutcome",2f);
        //Invoke("spawnGood",fishDelayTime);
    }

    public float fishDelayTime = 1f;
    
    private GameObject goodFish;
    private GameObject badFish;
    void spawnGood()
    {
        goodFish = Instantiate(_goodFish);
    }

    void spawnBad()
    {
        badFish = Instantiate(_badFish);
    }
    void SpawnRedBubble()
    {
        Bubbles = new GameObject("Bubbles");
        for (int i = 0; i <40; i++)
        {
            GameObject bubble = Instantiate(_redBubble);
            bubble.transform.position = new Vector3(-0.82f+Random.Range(-2.5f,2f), -6.2f+Random.Range(-3f,1.5f));
            bubble.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-0.3f, -0.1f);
            bubble.transform.parent = Bubbles.transform;
        }
        
        Invoke("DestroyBubble",2f);
        Invoke("DisplayOutcome",2f);
        //Invoke("spawnBad",fishDelayTime);
    }

    void DestroyBubble()
    {
        Destroy(Bubbles);
    }

    public void DisplayOutcome()
    {
        if (count <= 4)
        {
            displayText = "You Caught A Fish!";
        }
        else
        {
            displayText = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
        }
        
        _display.text = displayText;
        Invoke("DestroyDisplay",.5f);
    }

    private void DestroyDisplay()
    {
        _display.text = null;
    }
}
