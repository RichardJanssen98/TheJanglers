using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  public float upperBound;
  public float lowerBound;
  public float leftBound;
  public float rightBound;

  public float minDistance = 0.1f;
  public float camMoveSpeed;
  public Vector3 camOffset;

  private Camera cam;

  // Start is called before the first frame update
  void Start() {
    cam = GetComponent<Camera>();
  }

  // Update is called once per frame
  void Update() {
    Vector3 targetPosition = Player.Instance.transform.position + camOffset;
    Vector3 dir = targetPosition - transform.position;
    Vector3 newPosition = transform.position + (dir.normalized * camMoveSpeed) * Time.deltaTime;

    if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
      return;

    if (newPosition.x + cam.orthographicSize > rightBound)
      newPosition.x = rightBound - cam.orthographicSize;
    if (newPosition.y + cam.orthographicSize > upperBound)
      newPosition.y = upperBound - cam.orthographicSize;
    if (newPosition.x - cam.orthographicSize < leftBound)
      newPosition.x = leftBound + cam.orthographicSize;
    if (newPosition.y - cam.orthographicSize < lowerBound)
      newPosition.y = lowerBound + cam.orthographicSize;

    transform.position = newPosition;
  }
}
