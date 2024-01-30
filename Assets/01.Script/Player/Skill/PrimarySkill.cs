using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimarySkill : BaseSkill
{
    public GameObject PlayerBullet;
    public float BulletSpeed;

    private Weapon[] Weapons = new Weapon[4];

    private void Start()
    {
        Weapons[0] = new Level1Weapon();
        Weapons[1] = new Level2Weapon();
        Weapons[2] = new Level3Weapon();
        Weapons[3] = new Level4Weapon();
    }

    public override void Activate()
    {
        base.Activate();
        Weapons[GameInstance.Instance.WeaponLevel].Activate(transform.position, this);
    }


    public void ShootProjectile(Vector3 position, float speed, Vector3 direction, float Size = 1)
    {
        GameObject instance = Instantiate(PlayerBullet, position, Quaternion.identity);

        instance.transform.localScale *= Size;

        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }
    public interface Weapon
    {
        public void Activate(Vector3 playerPos, PrimarySkill primarySkill);
    }

    public class Level1Weapon : Weapon
    {
        public void Activate(Vector3 playerPos, PrimarySkill primarySkill)
        {
            primarySkill.ShootProjectile(playerPos, primarySkill.BulletSpeed, Vector3.up);
        }
    }
    public class Level2Weapon : Weapon
    {
        public void Activate(Vector3 playerPos, PrimarySkill primarySkill)
        {
            Vector3 BulletGap = new Vector3(0.15f,0,0);
            primarySkill.ShootProjectile(playerPos + BulletGap, primarySkill.BulletSpeed, Vector3.up, 1.1f);
            primarySkill.ShootProjectile(playerPos - BulletGap, primarySkill.BulletSpeed, Vector3.up, 1.1f);
        }
    }
    public class Level3Weapon : Weapon
    {
        public void Activate(Vector3 playerPos, PrimarySkill primarySkill)
        {
            primarySkill.ShootProjectile(playerPos, primarySkill.BulletSpeed, Vector3.up, 1.3f);

            Vector3 BulletGap = new Vector3(0.4f, -0.4f, 0);
            primarySkill.ShootProjectile(playerPos + BulletGap, primarySkill.BulletSpeed, Vector3.up);

            BulletGap.x -= 0.8f;
            primarySkill.ShootProjectile(playerPos + BulletGap, primarySkill.BulletSpeed, Vector3.up);
        }
    }
    public class Level4Weapon : Weapon
    {
        public void Activate(Vector3 playerPos, PrimarySkill primarySkill)
        {

        }
    }
}
