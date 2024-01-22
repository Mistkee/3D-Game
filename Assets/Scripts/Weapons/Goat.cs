using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Goat : MonoBehaviour
{
    public int gunDamage = 10;
    public float fireRate = .05f;
    public float weaponRange = 20f;
    public float hitForce = 0f;
    public Transform gunEnd;
    public GameObject player;
    
    private float BulletSpeed = 100;
    public int ammo = 150;
    
    [SerializeField]
    private ParticleSystem impactParticle;
    
    [SerializeField]
    private GameObject fireStream;
    [SerializeField]
    private GameObject reloadSteam;
    private bool isFiring;
    

    

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(0.01f);
    private Animator animator;
    private bool isReloading = false;

    private AudioSource gunAudio;
    public AudioClip goatAudio;
    public AudioClip goatReloadAudio;



    private float nextFire;
    
    void Start()
    {
        animator = GetComponent<Animator>();

        gunAudio = player.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //MISSING LAYER MASK FOR ENEMIES/WALLS DISTINCTION.
       
        if (Input.GetButton("Fire1") && Time.time > nextFire && ammo > 0 && !isReloading)
        {
            isFiring = true;
            //change this for a fire effect
            //shotParticle.Play();
            nextFire = Time.time + fireRate;
            
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            
            //THIS IS THE BROKEN EFFECT
            fireStream.SetActive(true);
            

            ammo--;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                
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
            
        }
        else
        {
            fireStream.SetActive(false);
            isFiring = false;
        }

        if (isFiring)
        {
            
            animator.SetBool("Firing", true);
            
        }
        if (!isFiring)
        {
            animator.SetBool("Firing", false);
            
        }

            if (ammo <= 0)
        {
            
            StartCoroutine(Reload());
        }
    }

    private IEnumerator ShotEffect()
    {
        
        gunAudio.clip = goatAudio;
        gunAudio.Play();
        yield return shotDuration;
    }
    
    private IEnumerator Reload()
    {
        
        ammo++;
        isReloading = true;

        //OTHER BROKEN ANIMATION AND EFFECT
        animator.SetBool("isReloading", true);
        reloadSteam.SetActive(true);

        gunAudio.clip = goatReloadAudio;
        gunAudio.Play();
        while (ammo <= 150)
        {
            Debug.Log(ammo);
            ammo++;
            yield return reloadDuration;
        }
        
        animator.SetBool("isReloading", false);
        isReloading = false;
        reloadSteam.SetActive(false);
    }
}
