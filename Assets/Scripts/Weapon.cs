using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A weapon that can be held by players and enemies, or can be dropped on the ground.
 */
public class Weapon : MonoBehaviour
{

    [Header("Required Attached gameobjects")]
    [Tooltip("Model of the weapon.")]
    public GameObject weaponModel;
    [Tooltip("Projectile that will be fired from the muzzle.")]
    public GameObject projectile;
    [Tooltip("Effect that will be played from the muzzle.")]
    public GameObject fireEffect;
    [Tooltip("Position effects and projectiles will be spawned from. Effectively a muzzle.")]
    public GameObject firePosition;
    [Tooltip("Position bullet casings will launch from after firing.")]
    public GameObject casingPosition;
    [Tooltip("Object that will be spawned from casingposition.")]
    public GameObject casing;
    [Tooltip("Final relative position of the recoil animation.")]
    public Vector3 recoilPosition;
    [Tooltip("Final relative rotation of the recoil animation.")]
    public Vector3 recoilRotation;
    [Space(15)]

    [Header("Optional value overrides (has default values)")]
    [Tooltip("The initial position of the weapon.")]
    public Vector3 modelOffset = new Vector3(1.03f, 0.09f, 0.86f);
    [Tooltip("The time the recoil animation takes to finish.")]
    public float recoilTime;
    public Vector3 basePosition;
    public Vector3 baseRotation;
    [Space(15)]

    [Header("Weapon function configurations")]
    [Tooltip("How the player will interact with the fireweapon method")]
    public FireMode mode;
    [Tooltip("Amount of shots a weapon can fire before being empty.")]
    public int magazineSize;
    [Tooltip("Max angle a bullet is allowed to deviate from its forward path in degrees.")]
    [Range(0, 90)]
    public float bulletDeviation = 0;
    [Tooltip("Amount of times a weapon can fire per second.")]
    public float shotsPerSecond;
    [Tooltip("Amount of fired projectiles per shot")]
    [Min(1)]
    public int projectilesPerShot = 1;
    [Tooltip("The mode the weapon starts with, whether it is held by an enemy, player, or dropped on the ground.")]
    public WeaponMode initialWeaponMode;

    private Rigidbody rbody;
    private Collider hitBox;
    private int ammoCount;
    private bool permissionToFire = true;
    private bool infiniteAmmo;
    private TrailRenderer trailRenderer;

    public enum FireMode {
        FullAuto, SemiAuto 
    };

    public enum WeaponMode
    {
        InPlayerHand, InEnemyHand, Dropped, Thrown
    }

    public void SetMode(WeaponMode mode)
    {
        switch(mode)
        {
            case WeaponMode.InEnemyHand:
                infiniteAmmo = true;
                rbody.isKinematic = true;
                hitBox.enabled = false;
                trailRenderer.enabled = false;
                break;

            case WeaponMode.Dropped:
                hitBox.enabled = true;
                rbody.isKinematic = false;
                gameObject.transform.parent = null;
                trailRenderer.enabled = false;
                break;

            case WeaponMode.Thrown:
                hitBox.enabled = true;
                rbody.isKinematic = false;
                trailRenderer.enabled = true;
                gameObject.transform.parent = null;

                //Enables scripts that only applies to the weapon when thrown
                gameObject.GetComponent<SpinRigidBody>().enabled = true;
                gameObject.GetComponent<Damage>().enabled = true;
                gameObject.GetComponent<DamageCollision>().enabled = true;
                gameObject.GetComponent<Interactable>().pickUpable = false;
                break;

            case WeaponMode.InPlayerHand:
                infiniteAmmo = false;
                rbody.isKinematic = true;
                trailRenderer.enabled = false;
                hitBox.enabled = false;
                break;        
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        permissionToFire = true;
        basePosition = weaponModel.transform.localPosition + modelOffset;
        baseRotation = weaponModel.transform.localRotation.eulerAngles;
        rbody = gameObject.GetComponent<Rigidbody>();
        hitBox = gameObject.GetComponent<Collider>();
        ammoCount = magazineSize;
        trailRenderer = GetComponent<TrailRenderer>();
        SetMode(initialWeaponMode);
    }

    /**
     * Attempts to fire the weapon. Only fires if it has permission to fire (not on cooldown), and has enough ammo (or infinite ammo).
     */
    public void FireWeapon()
    {
        float timeBetweenShots = 1 / shotsPerSecond;
        if (permissionToFire && ammoCount > 0)
        {
            Debug.Log("Fire!");
            Instantiate(fireEffect, firePosition.transform);
            //Fires projectilesPerShot amount of bullets within a "circle" defined by bulletdeviation angle.
            for (int i = 0; i<projectilesPerShot; i++) {
                float randomYspread = Random.Range(transform.localRotation.y - bulletDeviation, transform.localRotation.y + bulletDeviation);
                float xBound = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(bulletDeviation, 2) - Mathf.Pow(randomYspread, 2)));
                float randomXspread = Random.Range(transform.localRotation.x - xBound, transform.localRotation.x + xBound);
                GameObject launchedProjectile = Instantiate(projectile, firePosition.transform);
                launchedProjectile.transform.Rotate(randomXspread, randomYspread, 0);
            }

            //Decrements current ammunition
            Instantiate(casing, casingPosition.transform);
            if (!infiniteAmmo)
            {
                ammoCount -= 1;
            }

            //Recoil and time before next shot
            StartCoroutine(PlayRecoilAnimation());
            StartCoroutine(RevokePermissionToFire(timeBetweenShots));
        }
    }
    
    /**
     * performs an animation of the weapon moving from its local original position and rotation to target position and rotation in a set amount of time.
     */
    private IEnumerator PlayRecoilAnimation() {
        Vector3 targetPosition = recoilPosition;
        Vector3 targetRotation = recoilRotation;
        float lerpValue = 0;

        //Move weapon to assigned position and rotation in recoiltime seconds.
        while(lerpValue < 1)
        {
            weaponModel.transform.localPosition = Vector3.LerpUnclamped(Vector3.zero, targetPosition, lerpValue);
            weaponModel.transform.localRotation = Quaternion.LerpUnclamped(Quaternion.Euler(baseRotation), Quaternion.Euler(targetRotation), lerpValue);
            lerpValue += Time.deltaTime/recoilTime;
            yield return new WaitForEndOfFrame();
        }
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.Euler(baseRotation);
    }

    public void resetBasePosition()
    {
        basePosition = weaponModel.transform.localPosition + modelOffset;
    }

    /**
     * Takes away the weapons permission to fire for a set amound of seconds
     */
    private IEnumerator RevokePermissionToFire(float seconds)
    {
        permissionToFire = false;
        yield return new WaitForSeconds(seconds);
        permissionToFire = true;
    }

    public int getRemainingAmmo()
    {
        return ammoCount;
    }
}
