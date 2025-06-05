using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulReaperSpellCastState : EnemyState
{
    private Enemy_SoulReaper enemy;

    private int amountOfSpells;
    
    private float spellTimer;

    public SoulReaperSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_SoulReaper _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(12, enemy.transform);

        amountOfSpells = enemy.amountOfSpells;

        spellTimer = .5f;
    }

    public override void Update()
    {
        base.Update();

        

        spellTimer -= Time.deltaTime;
        

        if (CanCast())
        {
            enemy.CastSpell();
        }


        if (amountOfSpells <= 0)
            stateMachine.ChangeState(enemy.teleportState);
    }


    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeCast = Time.time;
    }

    private bool CanCast()
    {
        if(amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }

        return false;
    }
}
