using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A weapon that can be held by players and enemies, or can be dropped on the ground.
 */
public class Weapon : MonoBehaviour
{
    [Tooltip("Model of the weapon.")]
    public GameObject weaponModel;
    [Tooltip("Amount of shots a weapon can fire before being empty.")]
    public int capacity;
    [Tooltip("Max angle a bullet is allowed to deviate from its forward path in degrees.")]
    public float bulletDeviation = 0;
    [Tooltip("Amount of bullets a weapon can fire per second.")]
    public float shotsPerSecond;
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
    [Tooltip("The time the recoil animation takes to finish.")]
    public float recoilTime;
    [Tooltip("How inputs will interact with the weapon.")]
    public FireMode mode;
    [Tooltip("The initial position of the weapon.")]
    public Vector3 basePosition = new Vector3(1.03f, 0.09f, 0.86f);

    private bool permissionToFire = true;
    public Vector3 baseRotation;



    public enum FireMode {
        Auto, SemiAuto 
    };

    public enum WeaponMode
    {
        InPlayerHand, InEnemyHand, Dropped, Thrown
    }

    // Start is called before the first frame update
    void Start()
    {
        permissionToFire = true;
        //basePosition = weaponModel.transform.localPosition;
        baseRotation = weaponModel.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Attempts to fire the weapon.
     * TODO: Add functionality for the accuracy modifier.
     */
    public void FireWeapon()
    {
        float timeBetweenShots = 1 / shotsPerSecond;
        if (permissionToFire && capacity > 0)
        {
            Debug.Log("Fire!");
            Instantiate(fireEffect, firePosition.transform);
            Instantiate(projectile,firePosition.transform);
            Instantiate(casing, casingPosition.transform);
            capacity -= 1;
            StartCoroutine(PlayRecoilAnimation());
            StartCoroutine(RevokePermissionToFire(timeBetweenShots));
        }
    }

    public void ThrowWeapon()
    {

    }
    
    private void PlayFireEffect()
    {

    }

    /**
     * performs an animation of the weapon moving from its local original position and rotation to target position and rotation
     * TODO: Currently broken, rotates around the player, and ends with new rotation instead of base rotation.
     */
    private IEnumerator PlayRecoilAnimation() {
        Vector3 targetPosition = recoilPosition;
        Vector3 targetRotation = recoilRotation;
        float lerpValue = 0;

        //Move weapon to assigned position and rotation in recoiltime seconds.
        while(lerpValue < 1)
        {
            weaponModel.transform.localPosition = Vector3.LerpUnclamped(basePosition, targetPosition, lerpValue);
            weaponModel.transform.localRotation = Quaternion.LerpUnclamped(Quaternion.Euler(baseRotation), Quaternion.Euler(targetRotation), lerpValue);
            lerpValue += Time.deltaTime/recoilTime;
            yield return new WaitForEndOfFrame();
        }
        weaponModel.transform.localPosition = basePosition;
        weaponModel.transform.localRotation = Quaternion.Euler(baseRotation);
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
}
