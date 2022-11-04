using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A weapon that can be held by players and enemies, or can be dropped on the ground.
 */
public class Weapon : MonoBehaviour
{
    public GameObject weaponModel;
    public bool infiniteForEnemies;
    public int capacity;
    public float accuracyPercentage = 100;
    public float shotsPerSecond;
    public int damage;
    public GameObject projectile;
    public bool permissionToFire = true;
    public GameObject fireEffect;
    public GameObject fireLocation;
    public Vector3 recoilPosition;
    public Vector3 recoilRotation;
    public float recoilTime;
    public bool canBePickedUp;
    public FireMode mode;



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
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Attempts to fire the weapon
     */
    public void FireWeapon()
    {
        float timeBetweenShots = 1 / shotsPerSecond;
        if (permissionToFire)
        {
            Debug.Log("Fire!");
            Instantiate(fireEffect, fireLocation.transform);
            Instantiate(projectile,fireLocation.transform);
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
        Vector3 basePosition = weaponModel.transform.localPosition;
        Vector3 targetPosition = recoilPosition;
        Vector3 baseRotation = weaponModel.transform.rotation.eulerAngles;
        Vector3 targetRotation = recoilRotation;
        float lerpValue = 0;

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


    private void OnCollisionEnter(Collision other)
    {

    }

    private void SetState(FireMode fireMode) {
        mode = fireMode;
    }
}
