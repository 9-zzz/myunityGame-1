using UnityEngine;
using System.Collections;

public class ReplenishAmmo : MonoBehaviour
{


  // Use this for initialization
  void Start()
  {
  Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), collider);
  }

  // Update is called once per frame
  void Update()
  {
    float distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

    if (distance < 30)
    {
    transform.LookAt(GameObject.Find("Player").transform.position);
    rigidbody.AddRelativeForce(new Vector3(0, 20, 150));
    }
  }

}
