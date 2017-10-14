using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTriggerController : MonoBehaviour {

    [SerializeField]
    Animator animator;
    [SerializeField]
    QuerySDEmotionalController emotionController;
    [SerializeField]
    QuerySDEmotionalController.QueryChanSDEmotionalType normalEmotion = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_DEFAULT;
    [SerializeField]
    QuerySDEmotionalController.QueryChanSDEmotionalType alertEmotion = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_SMILE;

    private void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerCarBodyCollider")
        {
           
            animator.SetBool("alert", true);
            emotionController.ChangeEmotion(alertEmotion);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCarBodyCollider")
        {

            animator.SetBool("alert", false);
            emotionController.ChangeEmotion(normalEmotion);
        }
    }
}
