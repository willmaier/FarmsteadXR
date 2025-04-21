using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooter : MonoBehaviour
{

    public GameObject paintballPrefab;
    public float paintballSpeed;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public float spread, spreadFactor;
    public float reloadTime;
    public int magazineSize;
    int bulletsLeft;
    public static int bulletsShot;
    public TextMeshProUGUI bulletsInMag, magSize;
    bool reloading;
    public bool activeShooter = false;
    public AudioClip shootSFX;
    public Image reticleImage;
    public Color reticleColor, enemyReticleColor;
    public Transform gunTip;
    public static string[] stats;
    public static int statNo = 0;
    public static float timeTaken = 0;

    // pls
    private void Awake()
    {
        // activeShooter = false;
        bulletsLeft = magazineSize;
        // reticleColor = reticleImage.color;
        gunTip = GameObject.FindGameObjectWithTag("GunTip").GetComponent<Transform>();
       // bulletsInMag.text = bulletsLeft.ToString();
       // magSize.text = magazineSize.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && !reloading && bulletsLeft > 0 && activeShooter)
        {
            Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.R) && activeShooter) || (Input.GetButton("Fire1") && Time.time > nextFire && !reloading && bulletsLeft == 0) && activeShooter){
            Reload();
        }

        UpdateUI();
    }

    void Shoot()
    {
        
        nextFire = Time.time + fireRate;
        float x = Random.Range(-spread*spreadFactor, spread*spreadFactor);
        float y = Random.Range(-spread*spreadFactor, spread*spreadFactor);
        AudioSource.PlayClipAtPoint(shootSFX, gunTip.position);
        GameObject projectile = Instantiate(paintballPrefab, gunTip.position + gunTip.forward, gunTip.rotation);
        var _rb = projectile.GetComponent<Rigidbody>();
        Vector3 directionWithSpread = new Vector3(x, y, 0);
        _rb.AddForce(directionWithSpread + gunTip.forward * paintballSpeed, ForceMode.VelocityChange);
        //Debug.Log("Was: " + bulletsLeft);
        bulletsLeft--;
        bulletsShot++;
        //Debug.Log("Now: " + bulletsLeft);
        bulletsInMag.text = bulletsLeft.ToString();

    }

    private void Reload()
    {
      //  Debug.Log("Reloading!");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        bulletsInMag.text = bulletsLeft.ToString();
    }

    public void UpdateUI()
    {
        if (activeShooter)
        {
            bulletsInMag.enabled = true;
            magSize.enabled = true;
            bulletsInMag.text = bulletsLeft.ToString();
            magSize.text = magazineSize.ToString();
        }
        else
        {
            bulletsInMag.enabled = false;
            magSize.enabled = false;
        }
    }

    private void FixedUpdate()
    {
      //  ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;

        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, Mathf.Infinity))
        {
            
            if (hit.collider.CompareTag("Enemy")){
                //Debug.Log("see ya");
                reticleImage.color = Color.Lerp(reticleImage.color, enemyReticleColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, reticleColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, reticleColor, Time.deltaTime * 2);
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
        }
    }

    public static void CollectStats()
    {
        Debug.Log("Shot " + bulletsShot + " bullets and took " + timeTaken + " seconds!");
        statNo++;
        bulletsShot = 0;
        timeTaken = 0;   
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = gunTip.TransformDirection(Vector3.forward) * 50;
        Gizmos.DrawRay(gunTip.position, direction);
    }
}
