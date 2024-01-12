using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    //Random.insideUnitCircle*radius
    public int gunDamage = 1;
    public float radius = 2;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;
    public GameObject player;
    public LayerMask hitMask;
    private Vector3 offset;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);

    private AudioSource gunAudio;
    public AudioClip shotgunAudio;

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

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            //Sheep method, instance an object, throw it out in an arc, send out a ball of raycasts from it 
            //- in every direction, to add force to everything hit, and kill it
            //Shot gun method, do more than one raycast, ideally 8
            Debug.DrawRay(rayOrigin, fpsCam.transform.forward * weaponRange, Color.green);

            for (int i = 0; i < 8; i++)
            {
                
                float xOffset = Random.RandomRange(1, -1);
                float yOffset = Random.RandomRange(1, -1);
                offset = new Vector3(xOffset, yOffset, 0);

                if (Physics.SphereCast(rayOrigin, radius, fpsCam.transform.forward+offset, out hit, weaponRange))
                {

                    Debug.Log($"I hit {hit.transform.name}");
                    //Take this out for sniper, pass through
                    laserLine.SetPosition(1, hit.point);

                    //Adjust this to Omid's enemy health system
                    ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                    if (health != null)
                    {
                        health.Damage(gunDamage);
                    }

                    if (hit.rigidbody != null)
                    {
                        //calculate magnitude and direction by distance from a to b
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                }
                else
                {
                    Debug.Log($"I hit nothing :(");
                    laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
                }
            }
            
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.clip = shotgunAudio;
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
