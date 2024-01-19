using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;
    public GameObject player;

   
    public int ammo;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(0.8f);

    private AudioSource gunAudio;
    public AudioClip handgunAudio;
    public AudioClip handgunReloadAudio;
    public float gunPositionY;
    public float reloadPositionY;
    public float gunPositionX;
    public float recoilPositionX;

    private LineRenderer laserLine;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        
        laserLine = player.GetComponent<LineRenderer>();
        laserLine.enabled = true;
        gunAudio = player.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown ("Fire1") && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            ammo--;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
        }
        if (ammo == 0)
        {
            StartCoroutine (Reload());
        }
    }

    private IEnumerator ShotEffect()
    {
        // swap laser line for VFX
        //Recoil effect creates bugs
        //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, recoilPositionX, 0), 10 * Time.deltaTime);
        gunAudio.clip = handgunAudio;
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, gunPositionX, 0), 10 * Time.deltaTime);

    }

    private IEnumerator Reload()
    {
        // small bug with height
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, reloadPositionY, 0), 10 * Time.deltaTime);
        gunAudio.clip = handgunReloadAudio;
        gunAudio.Play();
        yield return reloadDuration;
        ammo = 8;
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3 (0,gunPositionY,0), 10 * Time.deltaTime);
    }
}
