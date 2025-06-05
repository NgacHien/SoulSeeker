using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string checkpointId;
    public bool activationStatus;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("Generate checkpoint Id")]
    private void GenerateId()
    {
        checkpointId = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            ActivedCheckpoint();
        }
    }

    public void ActivedCheckpoint()
    {
        if(activationStatus == false) 
            AudioManager.instance.PlaySFX(5, transform);

        activationStatus = true;
        anim.SetBool("active", true);
    }


  
}
