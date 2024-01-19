using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Goat : MonoBehaviour
{
    public int gunDamage = 1;
    public float weaponRange = 20f;
    public Transform gunEnd;
    public GameObject player;
    public float ammo;

    public Camera fpsCam;

    private AudioSource gunAudio;
    public AudioClip fireAudio;

    private LineRenderer laserLine;
    private float nextFire;
    private Collider flame;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = player.GetComponent<LineRenderer>();
        laserLine.enabled = true;
        gunAudio = player.GetComponent<AudioSource>();
        flame = GetComponent<BoxCollider>();
        flame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton("Fire1") && ammo > 0)
        {
            
            FireEffect();
            //Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            //RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);
            flame.enabled = true;
            

            //Sheep method, instance an object, throw it out in an arc, send out a ball of raycasts from it 
            //- in every direction, to add force to everything hit, and kill it

            /*if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                //Take this out for sniper, pass through
                laserLine.SetPosition(1, hit.point);
                Debug.Log($"I hit {hit.transform.name}");
                //Adjust this to Omid's enemy health system
                

                
            }

            else
            {
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }*/
        }
        else
        {
            flame.enabled = false;
            laserLine.enabled = false;
        }
        
    }

    private void FireEffect()
    {
        gunAudio.clip = fireAudio;
        gunAudio.Play();
        //Replace for fire effect
        laserLine.enabled = true;
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        Enemy health = collision.collider.GetComponent<Enemy>();

        if (health != null)
        {
            health.Damage(gunDamage);
        }
    }
}
