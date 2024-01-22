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
    [SerializeField]
    private ParticleSystem shotParticle;
    [SerializeField]
    private ParticleSystem impactParticle;
    [SerializeField]
    private TrailRenderer bulletTrail;
    [SerializeField]
    private float BulletSpeed = 100;
    private bool isFiring;
    private int fireTime;

    public int ammo = 150;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(0.01f);
    private Animator animator;
    private bool isReloading = false;

    private AudioSource gunAudio;
    public AudioClip goatAudio;
    public AudioClip goatReloadAudio;



    private float nextFire;
    // Start is called before the first frame update
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
            shotParticle.Play();
            nextFire = Time.time + fireRate;
            
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            //while ammo>=0 play fire effect
            
            
            

            ammo--;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                //make trail invisible and add a new fire impact effect
                TrailRenderer trail = Instantiate(bulletTrail, gunEnd.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
                

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
                
                TrailRenderer trail = Instantiate(bulletTrail, gunEnd.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, gunEnd.position + transform.forward * weaponRange, Vector3.zero, false));
            }
        }
        else
        {
            isFiring = false;
        }

        if (isFiring && fireTime == 0)
        {
            
            animator.SetBool("Firing", true);
            fireTime++;
        }
        if (!isFiring)
        {
            animator.SetBool("Firing", false);
            fireTime = 0;
        }

            if (ammo <= 0)
        {
            animator.SetBool("isReloading", true);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator ShotEffect()
    {
        //change this for a fire effect
        gunAudio.clip = goatAudio;
        gunAudio.Play();
        yield return shotDuration;
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }

        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(impactParticle, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
    private IEnumerator Reload()
    {
        
        ammo++;
        isReloading = true;
        
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
    }
}
