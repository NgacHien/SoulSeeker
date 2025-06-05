using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Fire ice effect", menuName = "Data/Item effect/Fire ice")]
public class FireIceEffect : ItemEffect
{
    [SerializeField] private GameObject fireIcePrefab;
    [SerializeField] private float xVelocity;

    public override void ExecuteEffect(Transform _respawnPosition)
    {
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.primaryAttack.comboCounter == 2;

        if (thirdAttack)
        {

            GameObject newFireIce = Instantiate(fireIcePrefab, _respawnPosition.position, player.transform.rotation);

            newFireIce.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir, 0);

            Destroy(newFireIce, 10);
        }

    }


}
