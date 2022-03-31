using UnityEngine;
using AK.Collisions;

public class BigGate : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] gemFrames = null;
    [SerializeField] Sprite frameWithGemSprite = null;
    [SerializeField] Animator[] braziers = null;

    int gemToFill;
    bool canOpen;
    Animator anm;
    Collisioner player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Collisioner>();
        anm = GetComponent<Animator>();
    }

    private void OnEnable() { player.OnGrabItem += RestoreGem; }
    private void OnDisable() { player.OnGrabItem -= RestoreGem; }

    private void RestoreGem()
    {
        gemFrames[gemToFill].sprite = frameWithGemSprite;
        braziers[gemToFill].SetTrigger("Fire");
        gemToFill++;
        canOpen = gemToFill == gemFrames.Length;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hello there!");
        if (canOpen && !anm.GetBool("DoorOpened") && collider.CompareTag("Player"))
        {
            anm.SetBool("DoorOpened", true);
        }
    }
}
