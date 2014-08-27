using UnityEngine;
using System.Collections;

public class hexrotateysrot : MonoBehaviour {
  float rotationsPerSecond = 0.1f;
  GameObject player;
  public float distance;
  public bool doLoop = false;
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    distance = Vector3.Distance(transform.position, player.transform.position);
  }
  void Update()
  {
    distance = Vector3.Distance(transform.position, player.transform.position);
    Vector3 euler = transform.localEulerAngles;
    euler.z += (rotationsPerSecond * (distance / 20) * 360f * Time.deltaTime);
    transform.localEulerAngles = euler;
  }
}
