﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour {

  public Slider VisualAmmoBar;
  public Slider SpecialAttackBar;
  public AudioClip shootSound;
  public GameObject spawnPoint;
  public GameObject bulletPF;
  public GameObject Character;
  private GameObject DangerZone;
  private GameObject projectile;
  public bool isSomeState = false;
  public int ammo = 120;
  public int special = 0;
  float shootForce = 99;
  float fireRate = 0.07f;
  float timer = 0;
  float RTVal;

  void Fire(){

    if (ammo > 0)
    {

      audio.PlayOneShot(shootSound);

      RaycastHit hit;

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//straight ray to mouse position
      //Ray ray = Camera.main.ScreenPointToRay( new Vector2(Screen.width/2, Screen.height/2));

      Vector3 aimPoint;//hit point placeholder

      if (Physics.Raycast(ray, out hit, 100))
      {//where the ray hits, so draw from anywhere to THIS = raycast from new THAT 

        aimPoint = hit.point;

      }
      else
      {                                        //if  ray doesn't hit anything, just make a point 100 units out into ray, to referece

        aimPoint = ray.origin + (ray.direction * 100);//aimPoint is some point 100 unitys into ray line

      }

      projectile = Instantiate(bulletPF, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

      projectile.transform.LookAt(aimPoint);          //fixes when hit point was = (0,0,0);

      Debug.DrawLine(spawnPoint.transform.position, aimPoint);

      Physics.IgnoreCollision(projectile.collider, Character.collider);

      projectile.rigidbody.velocity = projectile.transform.forward * shootForce;//this plus the LookAt aimpoint sends a bullet on the correct ray

      timer = 0;

      ammo--;

    }

  }

  void Update() {
  
    RTVal = Input.GetAxis("RightTrigger");

    if (VisualAmmoBar != null)
      VisualAmmoBar.value = ammo;

    if (SpecialAttackBar!= null)
      SpecialAttackBar.value = special;

    if (special >= 100)
    {
      ammo += (120-ammo);//refill ammo
      //bar's color
    }

    timer += Time.deltaTime;

    if (Input.GetButton("Fire1") && (timer > fireRate) ||
      ((RTVal > 0) && (timer > fireRate)))
    {

      Fire();

    }

  }//end update

}//end of class
