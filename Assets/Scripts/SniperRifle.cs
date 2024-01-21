using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : MonoBehaviour
{
    public int gunDamage = 100;
    public float fireRate = .4f;
    public float weaponRange = 100f;
    public float hitForce = 400f;
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


    public int ammo = 6;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(2f);
    private Animator animator;
    private bool isReloading = false;

    private AudioSource gunAudio;
    public AudioClip sniperAudio;
    public AudioClip sniperReloadAudio;

    public RaycastHit[] hits;

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

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo > 0 && !isReloading)
        {
            shotParticle.Play();
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            ammo--;

            Ray ray = new Ray(rayOrigin, fpsCam.transform.forward);
            hits = Physics.RaycastAll(ray, weaponRange);
            foreach (RaycastHit hit in hits)
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
            TrailRenderer trail = Instantiate(bulletTrail, gunEnd.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, gunEnd.position + transform.forward * weaponRange, Vector3.zero, false));
        }
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator ShotEffect()
    {
        animator.SetTrigger("Recoil");
        gunAudio.clip = sniperAudio;
        gunAudio.Play();
        yield return shotDuration;
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        //Doesnt make a hole imprint on what it shoots
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
        
        isReloading = true;
        animator.SetBool("isReloading", true);
        gunAudio.clip = sniperReloadAudio;
        gunAudio.Play();
        yield return reloadDuration;
        ammo = 5;
        animator.SetBool("isReloading", false);
        isReloading = false;
    }
}
