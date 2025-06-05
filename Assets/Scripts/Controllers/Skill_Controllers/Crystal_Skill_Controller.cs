using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    private float crystalExistTimer;

   

    private void Start()
    {
      
    }

    public void SetupCrystal(float _crystalDuration)
    {
        crystalExistTimer = _crystalDuration;
       
        

    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;    

        if (crystalExistTimer < 0)
        {
            FinishCrystal();
        }
    }

    public void FinishCrystal()
    {
       SelfDestroy();
    }


    public void SelfDestroy() => Destroy(gameObject);
}
