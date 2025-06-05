using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_SoulReaper : Enemy
{
    #region States

    public SoulReaperBattleState battleState { get; private set; }

    public SoulReaperAttackState attackState { get; private set; }
    public SoulReaperIdleState idleState { get; private set; }
    public SoulReaperDeathState deathState { get; private set; }

    public SoulReaperSpellCastState spellCastState { get; private set; }

    public SoulReaperTeleportState teleportState { get; private set; }
    #endregion


    public bool bossFightStart;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;

    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;




    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new SoulReaperIdleState(this, stateMachine, "Idle", this);

        battleState = new SoulReaperBattleState(this, stateMachine, "Move", this);
        attackState = new SoulReaperAttackState(this, stateMachine, "Attack", this);

        deathState = new SoulReaperDeathState(this, stateMachine, "Idle", this);

        spellCastState = new SoulReaperSpellCastState(this, stateMachine, "SpellCast", this);

        teleportState = new SoulReaperTeleportState(this, stateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
           // Debug.Log("Looking for new position");
            FindPosition();
        }

    }


    //public override void Die()
    //{
    //    base.Die();
    //    stateMachine.ChangeState(deathState);
    //}


    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
        GameManager.instance.EndGame();
    }



    //private IEnumerator DelayFade(UI_FadeScreen fade)
    //{
    //    yield return null; 
    //    fade.FadeOut();
    //}


    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * 3;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + 4.5f);
           
        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<SoulReaperSpell_Controller>().SetupSpell(stats);
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }



    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }

        return false;   
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
           
            return true;
        }

        
        return false;
    }

 


}
