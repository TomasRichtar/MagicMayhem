
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ProjectileGunTutorial : MonoBehaviour
{
    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public float Cooldown;
    public float CooldownTimer;
    public float CastTime;
    public float CastTimeTimer;
    [SerializeField] private Slider CastTimeSlider;
    [SerializeField] private Slider CooldownSlider;
    public bool IsAbilityReady=true;

    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing :D
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
   
    IEnumerator AbilityCooldownTimer(float cooldown)
    {
        IsAbilityReady = false;
        //yield return new WaitForSeconds(castTime);
        CooldownTimer = 0f; // Reset elapsed time

        // Loop until the cast time is complete
        while (CooldownTimer < cooldown)
        {
            CooldownTimer += Time.deltaTime; // Increment elapsed time
            UpdateTMPSliderValue(); // Update slider value
            yield return null; // Wait for the next frame
        }

        IsAbilityReady = true;
    }
    IEnumerator AbilityCastTimer(float castTime)
    {
        //yield return new WaitForSeconds(castTime);
        CastTimeTimer = 0f; // Reset elapsed time
        CastTimeSlider.transform.localScale = Vector3.one;
        // Loop until the cast time is complete
        while (CastTimeTimer < castTime)
        {
            CastTimeTimer += Time.deltaTime; // Increment elapsed time
            UpdateSliderValue(); // Update slider value
            yield return null; // Wait for the next frame
        }
        Shoot();
        CastTimeSlider.transform.localScale = Vector3.zero;
    }
    void UpdateTMPSliderValue()
    {
        // Calculate the slider value based on the elapsed time and cast time
        float sliderValue = CooldownTimer / Cooldown;
        // Set the slider value
        CooldownSlider.value = sliderValue;
    }
    void UpdateSliderValue()
    {
        // Calculate the slider value based on the elapsed time and cast time
        float sliderValue = CastTimeTimer / CastTime;
        // Set the slider value
        CastTimeSlider.value = sliderValue;
    }
    public void AbilityCast()
    {
        if (IsAbilityReady == false) return;
        StartCoroutine(AbilityCooldownTimer(Cooldown));
        StartCoroutine(AbilityCastTimer(CastTime));
    }
    private void Shoot()
    {
        
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.GetComponent<ProjectileAddon>().Casted();
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player (should only be called once)
           // playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
}
