using UnityEngine;
using System.Collections;

public class ReplenishAmmo : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter(Collision col)
  {//for enemy itself not bullets
    if (col.gameObject.name == "Player")
    {
      Debug.Log("HIT AMMO DROP!!!    ");
      GameObject.Find("spawnPoint").GetComponent<Shoot>().ammo += 20;
      Destroy(gameObject);
    }
  }

}
