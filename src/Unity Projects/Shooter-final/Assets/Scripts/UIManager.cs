using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text references")]
    public Text scoreText;
    public Text healthText;

    [Header("Player reference")]
    public PlayerController playerController;

    // Update is called once per frame
    private void Update()
    {
        // Not optimized, should use a delegate system instead
        scoreText.text = $"Score : {playerController.score}";
        healthText.text = $"Health : {playerController.Health}";
    }
}
