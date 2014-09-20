using UnityEngine;
using System.Collections;

public class tobiasAi : MonoBehaviour
{
  public bool hitwithbeam = false;
  public GameObject ammoPickupPF;
  GameObject ammoPickup;

  void Update()
  {
    if (hitwithbeam)
      iTween.ShakePosition(gameObject, iTween.Hash("x", 10f, "y", 10f, "z", 10f, "time", 0.25f));
  }

  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "PlayerBullet")
      GameObject.Find("spawnPoint").GetComponent<Shoot>().special += 6;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "sword")
      iTween.ShakePosition(gameObject, iTween.Hash("x", 0.8f, "y", 1f, "z", 1f, "time", 0.25f));

    if (other.gameObject.tag == "specialbeam")
    {
      hitwithbeam = true;
      ammoPickup = Instantiate(ammoPickupPF, transform.position, transform.rotation) as GameObject;
      ammoPickup.rigidbody.AddRelativeForce(0,900,0);
      StartCoroutine("Example");
    }
  }

  IEnumerator Example()
  {
    print(Time.time);
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
    print(Time.time);
  }

}