using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulReaperIdleState : EnemyState
{
    private Enemy_SoulReaper enemy;

    private Transform player;

    public SoulReaperIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_SoulReaper enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.PlaySFX(20, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        if(Vector2.Distance(player.transform.position, enemy.transform.position) < 8)
            enemy.bossFightStart = true;

        if (Input.GetKeyDown(KeyCode.V))
            stateMachine.ChangeState(enemy.teleportState);


        if(stateTimer < 0 && enemy.bossFightStart)
            stateMachine.ChangeState(enemy.battleState);
    }
}
