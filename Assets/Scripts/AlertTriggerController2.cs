using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTriggerController2 : MonoBehaviour {

    enum State
    {
        WaitInDangerousPlace,
        Alert,
        RunToSafePlace,
        WaitInSafePlace,
        RunToDangerousPlace
    }

    State state;

    [SerializeField]
    Animator animator;
    [SerializeField]
    QuerySDEmotionalController emotionController;
    [SerializeField]
    QuerySDEmotionalController.QueryChanSDEmotionalType normalEmotion = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_DEFAULT;
    [SerializeField]
    QuerySDEmotionalController.QueryChanSDEmotionalType alertEmotion = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_SMILE;

    [SerializeField]
    Transform dangerousPlace;
    [SerializeField]
    Transform safePlace;
    [SerializeField]
    Rigidbody characterBody;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    float alertDuration = 1.0f;
    float alertCounter = 0;
    [SerializeField]
    float speed = 8;

    bool playerInSight;

    private void Start()
    {

    }

    private void Update()
    {
        switch(state){
            case State.WaitInDangerousPlace:
                transform.root.LookAt(safePlace);
                if (playerInSight)
                {                  
                    state = State.Alert;
                    alertCounter = alertDuration;
                    emotionController.ChangeEmotion(alertEmotion);
                }
                break;
            case State.Alert:
                if (alertCounter > 0)
                {
                   transform.root.LookAt(playerTransform);
                   alertCounter -= Time.deltaTime;
                }
                else
                {
                    state = State.RunToSafePlace;              
                    StartCoroutine(RunToSafePlace());
                }
                break;
            case State.RunToSafePlace:
               

                break;
            case State.WaitInSafePlace:
                transform.root.LookAt(dangerousPlace);
                if (playerInSight == false)
                {
                    state = State.RunToDangerousPlace;

                    StartCoroutine(RunToDangerousPlace());
                }
                break;
            case State.RunToDangerousPlace:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerCarBodyCollider")
        {
            playerInSight = true;           
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCarBodyCollider")
        {
            playerInSight = false;
        }
    }

    IEnumerator RunToSafePlace()
    {
        animator.SetBool("running", true);
        while (Vector3.Distance(transform.root.position, safePlace.position) > 0.1)
        {
            characterBody.MovePosition(Vector3.MoveTowards(transform.root.position, safePlace.position, speed * Time.deltaTime));
            transform.root.LookAt(safePlace);
            yield return null;
        }
        animator.SetBool("running", false);

        yield return new WaitForSeconds(0.5f);

        transform.root.LookAt(dangerousPlace);
        emotionController.ChangeEmotion(normalEmotion);

        state = State.WaitInSafePlace;
        
    }

    IEnumerator RunToDangerousPlace()
    {
        animator.SetBool("running", true);
        while (Vector3.Distance(transform.root.position, dangerousPlace.position) > 0.1)
        {
            characterBody.MovePosition(Vector3.MoveTowards(transform.root.position, dangerousPlace.position, speed * Time.deltaTime));
            transform.root.LookAt(dangerousPlace);
            yield return null;
        }
        animator.SetBool("running", false);

        yield return new WaitForSeconds(0.5f);

        transform.root.LookAt(safePlace);

        state = State.WaitInDangerousPlace;
    }
}
