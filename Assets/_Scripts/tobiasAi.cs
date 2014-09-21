using UnityEngine;
using System.Collections;

public class tobiasAi : MonoBehaviour
{
  public bool hitwithbeam = false;
  public GameObject ammoPickupPF;
  GameObject ammoPickup;
  Color dangerColor;

  void Update()
  {
    dangerColor = new Color(Random.Range(0.0f, 1.0f), 0.0f, 0.0f);//rgb

    if (hitwithbeam)
    {
      iTween.ShakePosition(gameObject, iTween.Hash("x", 1f, "y", 0.3f, "z", 4f, "time", 0.25f));
      transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
      renderer.material.SetColor("_OutlineColor", dangerColor);
    }
  }

  void shootOutAmmo()
  {
    ammoPickup = Instantiate(ammoPickupPF, transform.position, transform.rotation) as GameObject;
    ammoPickup.rigidbody.AddRelativeForce(0, 1200, 0);
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
      StartCoroutine("Example");
    }
  }

  IEnumerator Example()
  {
    shootOutAmmo();
    shootOutAmmo();
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
    shootOutAmmo();
    shootOutAmmo();
  }

}