using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private float attackMultiplier;
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]

    [Header("Clone Attack")]
    [SerializeField] private UI_SkillTreeSlot cloneAttackUnlockButton;
    [SerializeField] private float cloneAttackMultiplier;
    [SerializeField] private bool canAttack;

    [Header("Clone Phantom Strike")]
    [SerializeField] private UI_SkillTreeSlot phantomCloneUnlockButton;
    [SerializeField] private float clonePhantomMultiplier;
    public bool canApplyOnHitEffect { get; private set; }

    [Header("Multiple CLone")]
    [SerializeField] private UI_SkillTreeSlot multipleUnlockButton;
    [SerializeField] private float multipleCloneAttackMultiplier;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;


    protected override void Start()
    {
        base.Start();

        cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        phantomCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockClonePhantomStrike);
        multipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultipleClone);



    }

    #region UnlockRegion

    protected override void CheckUnlock()
    {
        UnlockCloneAttack();
        UnlockClonePhantomStrike();
        UnlockMultipleClone();
    }

    private void UnlockCloneAttack()
    {
        if(cloneAttackUnlockButton.unlocked)
        {
            canAttack = true;
            attackMultiplier = cloneAttackMultiplier;
        }
    }



    private void UnlockClonePhantomStrike()
    {
        if(phantomCloneUnlockButton.unlocked)
        {
            canApplyOnHitEffect = true;
            attackMultiplier = clonePhantomMultiplier;
        }
    }

    private void UnlockMultipleClone()
    {
        if (multipleUnlockButton.unlocked)
        {
            canDuplicateClone = true;
            attackMultiplier = multipleCloneAttackMultiplier;

        }
    }


    #endregion


    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, canAttack, _offset, canDuplicateClone, chanceToDuplicate,player, attackMultiplier);
    }

   


    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
       
            StartCoroutine(CloneDelayCoroutine(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    private IEnumerator CloneDelayCoroutine(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, _offset);

    }

}
