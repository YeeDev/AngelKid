using UnityEngine;
using AK.Collisions;

public class BigGate : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] gemFrames = null;
    [SerializeField] Sprite frameWithGemSprite = null;
    [SerializeField] Animator[] braziers = null;

    int gemToFill;
    Collisioner player;

    private void Awake() { player = GameObject.FindWithTag("Player").GetComponent<Collisioner>(); }

    private void OnEnable() { player.OnGrabItem += RestoreGem; }
    private void OnDisable() { player.OnGrabItem -= RestoreGem; }

    private void RestoreGem()
    {
        gemFrames[gemToFill].sprite = frameWithGemSprite;
        braziers[gemToFill].SetTrigger("Fire");
        gemToFill++;
    }
}
