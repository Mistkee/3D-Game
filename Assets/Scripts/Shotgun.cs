using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int gunDamage = 20;
    public float fireRate = .25f;
    public float weaponRange = 30f;
    public float hitForce = 100f;
    public Transform gunEnd;
    public GameObject player;
    private Vector3 bulletSpread = new Vector3(.1f,.1f,.1f);
    private ParticleSystem shotParticle;
    private ParticleSystem impactParticle;
    private TrailRenderer bulletTrail;

    public int ammo;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(1f);
    private Animator animator;
    private bool isReloading = false;

    private AudioSource gunAudio;
    public AudioClip shotgunAudio;
    public AudioClip shotgunReloadAudio;


    private LineRenderer laserLine;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        laserLine = player.GetComponent<LineRenderer>();
        laserLine.enabled = true;
        gunAudio = player.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo > 0 && !isReloading)
        {
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            RaycastHit hit2;
            RaycastHit hit3;
            RaycastHit hit4;
            RaycastHit hit5;
            RaycastHit hit6;
            RaycastHit hit7;
            RaycastHit hit8;
            laserLine.SetPosition(0, gunEnd.position);
            ammo--;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(0,0.5f,0), out hit, weaponRange))
            {
                StartCoroutine(ShotEffect());
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
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(0, -0.5f, 0), out hit2, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit2.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit2.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit2.rigidbody != null)
                {
                    hit2.rigidbody.AddForce(-hit2.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(0.5f, 0.5f, 0), out hit3, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit3.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit3.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit3.rigidbody != null)
                {
                    hit3.rigidbody.AddForce(-hit3.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(-0.5f, 0.5f, 0), out hit4, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit4.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit4.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit4.rigidbody != null)
                {
                    hit4.rigidbody.AddForce(-hit4.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(0.5f, -0.5f, 0), out hit5, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit5.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit5.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit5.rigidbody != null)
                {
                    hit5.rigidbody.AddForce(-hit5.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(-0.5f, -0.5f, 0), out hit6, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit6.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit6.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit6.rigidbody != null)
                {
                    hit6.rigidbody.AddForce(-hit6.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(0.5f, 0, 0), out hit7, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit7.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit7.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit7.rigidbody != null)
                {
                    hit7.rigidbody.AddForce(-hit7.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward + new Vector3(-0.5f, 0, 0), out hit8, weaponRange))
            {
                StartCoroutine(ShotEffect());
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit8.point);

                //Adjust this to Omid's enemy health system
                Enemy health = hit8.collider.GetComponent<Enemy>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit8.rigidbody != null)
                {
                    hit8.rigidbody.AddForce(-hit8.normal * hitForce);
                }
            }
            else
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
        }
        if (ammo == 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator ShotEffect()
    {
        // swap laser line for VFX
        animator.SetTrigger("Recoil");
        gunAudio.clip = shotgunAudio;
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;

    }

    private IEnumerator Reload()
    {
        // small bug with height
        // do with animation
        isReloading = true;
        animator.SetBool("isReloading", true);
        gunAudio.clip = shotgunReloadAudio;
        gunAudio.Play();
        yield return reloadDuration;
        ammo = 8;
        animator.SetBool("isReloading", false);
        isReloading = false;
    }

   
}
