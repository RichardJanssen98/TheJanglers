using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
  public List<Pickup> pickupsInRange;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void GrabClosestPickup() {
    Pickup pickupToGrab = null;
    float closestDistance = 99999999999999;
    if (pickupsInRange != null) {
      foreach (Pickup p in pickupsInRange) {
        if (p == Player.Instance.projectileWeapon) {
          continue;
        }

        float distance = Vector2.Distance(this.transform.position, p.transform.position);

        if (distance <= closestDistance) {
          closestDistance = distance;
          pickupToGrab = p;
        }
      }
    }
    if (pickupToGrab != null) {
      //If first weapon
      if (Player.Instance.projectileWeapon != null) {
        Destroy(Player.Instance.projectileWeapon.gameObject);
      }

      pickupToGrab.PickupObject();
      pickupsInRange.Remove(pickupToGrab);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Pickup pickup = collision.gameObject.GetComponent<Pickup>();
    if (pickup != null) {
      if (pickupsInRange.Contains(pickup) == false) {
        pickupsInRange.Add(pickup);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    Pickup pickup = collision.gameObject.GetComponent<Pickup>();
    if (pickup != null) {
      pickupsInRange.Remove(collision.gameObject.GetComponent<Pickup>());
    }
  }
}
