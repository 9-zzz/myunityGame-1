using UnityEngine;
using System.Collections;

public class dashPickUp: MonoBehaviour
{
  // Use this for initialization
  void Start()
  {
    Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), collider);
  }

  // Update is called once per frame
  void Update()
  {

    if (transform.position.y < -200)
      Destroy(gameObject);

    float distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

    if (distance < 50)
    {
      transform.LookAt(GameObject.Find("Player").transform.position);
      //rigidbody.AddRelativeForce(new Vector3(0, 0, 170));
      transform.Translate(Vector3.forward * (Time.deltaTime * 50), Space.Self);
    }
  }

}
