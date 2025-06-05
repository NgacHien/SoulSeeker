using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    protected float cooldownTimer;

    protected Player player;

  

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        Invoke("CheckUnlock", .1f);
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }



    protected virtual void CheckUnlock()
    {

    }




    public virtual bool CanUseSkill()
    {
        if (cooldownTimer <= 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        player.fx.CreatePopUpText("Cooldown");
        return false;
    }


    public virtual void UseSkill()
    {
        
    }

}
