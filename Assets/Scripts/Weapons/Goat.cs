using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
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
    public TextMeshProUGUI ammoText;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(0.05f);
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
        gunAudio.clip = goatAudio;
        gunAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //MISSING LAYER MASK FOR ENEMIES/WALLS DISTINCTION.
        ammoText.text = ammo + " / 150 ";
        if (Input.GetButton("Fire1") && Time.time > nextFire && ammo > 0 && !isReloading)
        {
            StartCoroutine(ShotEffect());
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire && ammo > 0 && !isReloading)
        {
            
            
            nextFire = Time.time + fireRate;
            
            
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            
            
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
        else if (Input.GetButtonUp("Fire1") || ammo <= 0 || isReloading)
        {
            fireStream.SetActive(false);

            if (ammo <= 0)
            {
                StartCoroutine(Reload());
            }
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
        reloadSteam.SetActive(true);

        gunAudio.clip = goatReloadAudio;
        gunAudio.Play();
        while (ammo < 150)
        {
            Debug.Log(ammo);
            ammo++;
            yield return reloadDuration;
        }

        isReloading = false;
        
        reloadSteam.SetActive(false);
    }
}
