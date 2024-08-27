using Mirror;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == NetworkClient.localPlayer.gameObject)
        {
            int Bounce = Random.Range(5, 11);
            NetworkClient.localPlayer.GetComponent<Rigidbody>().AddForce(Vector3.up * Bounce, ForceMode.Impulse);
        }
    }
}
