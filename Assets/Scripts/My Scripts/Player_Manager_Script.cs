using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller_Script))]
public class Player_Manager_Script : MonoBehaviour
{
    private Player_Controller_Script m_GOPlayerController;
    [SerializeField] private float m_fMaxHealth;
    private float m_fCurrentHealth;
    public event Action Death;
    [SerializeField] private Slider_UI_Script m_sHealthSlider;

    public void InIt()
    {
        m_fMaxHealth = 100.0f;
        m_fCurrentHealth = m_fMaxHealth;

        if (m_sHealthSlider != null)
        {
            m_sHealthSlider.InIt(GetHealth() / 100.0f);
        }
        m_GOPlayerController = gameObject.GetComponent<Player_Controller_Script>();
        m_GOPlayerController.InIt();
    }

    public float GetHealth()
    {
        return m_fCurrentHealth;
    }

    public void ChangeHealth(float newValue)
    {
        m_fCurrentHealth += newValue;
        if (m_fCurrentHealth <= 0.0f)
        {
            Death?.Invoke();
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        m_sHealthSlider.ChangeValue(GetHealth() / 100.0f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Pickup>(out Pickup pick))
        {
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