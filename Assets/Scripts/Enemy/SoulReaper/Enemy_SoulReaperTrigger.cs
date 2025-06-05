using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SoulReaperTrigger : Enemy_AnimationTriggers
{

    private Enemy_SoulReaper enemySoulReaper => GetComponentInParent<Enemy_SoulReaper>();

    private void Relocate() => enemySoulReaper.FindPosition();


    private void MakeInvisible() => enemySoulReaper.fx.MakeTransparent(true);

    private void MakeVisible() => enemySoulReaper.fx.MakeTransparent(false);


}
