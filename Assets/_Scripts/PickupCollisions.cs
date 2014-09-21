using UnityEngine;
using System.Collections;

public class PickupCollisions : MonoBehaviour
{

  public AudioClip ammoPickUpSound;
  public ParticleSystem ammoPUParticles;

  // Use this for initialization
  void Start()
  {

  }

  void ammoFX(Vector3 position)
  {
    ParticleSystem explInstance = Instantiate(ammoPUParticles,
          position,
          transform.rotation) as ParticleSystem;
    Destroy(explInstance, 2);
  }


  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("ammodrop"))
    {
      audio.PlayOneShot(ammoPickUpSound);
      ammoFX(other.gameObject.transform.position);
      GameObject.Find("spawnPoint").GetComponent<Shoot>().ammo += 10;
      GameObject.Find("Score").GetComponent<ShowScoreValue>().score2 += 2;
      Destroy(other.gameObject);
    }
  }


  /*void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "ammodrop")
    {
      audio.PlayOneShot(ammoPickUpSound);
      ammoFX(collision.gameObject.transform.position);
      GameObject.Find("spawnPoint").GetComponent<Shoot>().ammo += 20;
      GameObject.Find("Score").GetComponent<ShowScoreValue>().score2 += 10;
      Destroy(collision.gameObject);
    }
  }*/

}
