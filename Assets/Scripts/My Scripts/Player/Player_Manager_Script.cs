using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller_Script))]
[RequireComponent(typeof(AudioSource))]
public class Player_Manager_Script : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float m_fMaxHealth;
    private float m_fCurrentHealth;

    [Header("References")]
    [SerializeField] private Slider_UI_Script m_sHealthSlider;
    private Player_Controller_Script m_GOPlayerController;
    private AudioSource m_StarPickupSound;
    public event Action Death;

    /// <summary>
    /// Calls the InIt function for the Player Controller and Health Slider scripts.
    /// </summary>
    public void InIt()
    {
        m_fCurrentHealth = m_fMaxHealth;
        m_StarPickupSound = GetComponent<AudioSource>();
        if (m_sHealthSlider != null)
        {
            m_sHealthSlider.InIt(GetCurrentHealth() / m_fMaxHealth);
        }
        m_GOPlayerController = gameObject.GetComponent<Player_Controller_Script>();
        m_GOPlayerController.InIt();
    }

    /// <summary>
    /// Initialise and reset the properties.
    /// Make the Pickup available again.
    /// </summary>
	/// <returns>Current Health of the Player</returns>
    public float GetCurrentHealth()
    {
        return m_fCurrentHealth;
    }

    /// <summary>
    /// Adds new health calue to current health.]
    /// Checks if current healh has dropped below 0, if so calls Death event.
    /// Calls UpdateHealthUI to inform the player through the UI of changes to their health.
    /// </summary>
    public void ChangeHealth(float newValue)
    {
        m_fCurrentHealth += newValue;
        if (m_fCurrentHealth <= 0.0f)
        {
            Death?.Invoke();
        }
        UpdateHealthUI();
    }

    /// <summary>
    /// Checks HealthSlider isn't null.
    /// If it is't then calls its ChangeValue with the current health of the Player.
    /// </summary>
    private void UpdateHealthUI()
    {
        if (m_sHealthSlider != null)
        {
            m_sHealthSlider.ChangeValue(GetCurrentHealth() / m_fMaxHealth);
        }
    }

    /// <summary>
    /// If the Player enters the collider of a Pickup; calls its GetPickedUp function.
    /// If the player enters the collider of a HealthAffector; it calls its AddedPlayerHealthValue function, then destroys the object.
    /// If the player enters the collider of a FinishedArea; it calls its Entered function.
    /// </summary>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Pickup>(out Pickup pick))
        {
            m_StarPickupSound.Play();
            int lol = pick.GetPickedUp();
        }
        else if (collider.gameObject.TryGetComponent<IHealthAffector>(out IHealthAffector healthAffector))
        {
            ChangeHealth(healthAffector.AddedPlayerHealthValue());
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.TryGetComponent<Finished_Area_Script>(out Finished_Area_Script finishedArea))
        {
            finishedArea.Entered();
        }
    }
}