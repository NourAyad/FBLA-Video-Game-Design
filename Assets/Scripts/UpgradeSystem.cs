using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour {

    private float baseAttackSpeed;
    private float baseMovementSpeed;

    public int currentAttackSpeedLevel = 0;
    public int currentMovementSpeedLevel = 0;

    public float movementSpeedIncrement;
    public float attackSpeedIncrement;

    public Button attackSpeedButton;
    public Button movementSpeedButton;
    public Slider attackSpeedSlider;
    public Slider movementSpeedSlider;

    public int maxLevel;
    public GameController gameController;
    private PlayerController player;

    public bool upgradeAvailable;
    public bool upgradeInitiated;
    public int targetScore;
    public int currentScore;
    public int upgradeScoreIncrement;

    public float flashRate;
    private float flashTime;
    private float unflashTime;
    public float flashDuration;
    public Text upgradeAvailableText;
    public Text pressZ;
    public Text pressX;
    
	// Use this for initialization
	void Start ()
    {
        FindPlayer();

        attackSpeedSlider.maxValue = maxLevel;
        movementSpeedSlider.maxValue = maxLevel;

        targetScore = currentScore + upgradeScoreIncrement;
        upgradeInitiated = false;
        upgradeAvailable = false;
        HideUpgradeText();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player != null)
        {
            if (upgradeAvailable && Input.GetKeyUp(KeyCode.Z))
            {
                attackSpeedButton.onClick.Invoke();
            }

            if (upgradeAvailable && Input.GetKeyUp(KeyCode.X))
            {
                movementSpeedButton.onClick.Invoke();
            }

            SetMovementSpeed();
            SetAttackSpeed();
            UpdateSliders();
            CheckUpgradeAvailable();
        }
	}

    public void FindPlayer()
    {
        Debug.Log("Looking");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            baseMovementSpeed = player.speed;
            
            baseAttackSpeed = player.fireRate;
            
        } 
           
    }

    void SetMovementSpeed()
    {
        player.speed = baseMovementSpeed * (Mathf.Pow(movementSpeedIncrement, currentMovementSpeedLevel));
    }

    void SetAttackSpeed()
    {
        player.fireRate = baseAttackSpeed * (Mathf.Pow(attackSpeedIncrement, currentAttackSpeedLevel));;
    }

    void UpdateSliders()
    {
        movementSpeedSlider.value = currentMovementSpeedLevel;
        attackSpeedSlider.value = currentAttackSpeedLevel;

        if (currentAttackSpeedLevel >= maxLevel)
        {
            attackSpeedButton.interactable = false;
        }

        if (currentMovementSpeedLevel >= maxLevel)
        {
            movementSpeedButton.interactable = false;
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (currentAttackSpeedLevel < maxLevel)
        {
            currentAttackSpeedLevel += 1;
            UpgradeComplete();
        }
    }

    public void UpgradeMovementSpeed()
    {
        if (currentMovementSpeedLevel < maxLevel)
        {
            currentMovementSpeedLevel += 1;
            UpgradeComplete();
        }
    }

    public void CheckUpgradeAvailable()
    {
        if (currentScore >= targetScore && (currentAttackSpeedLevel < maxLevel || currentMovementSpeedLevel < maxLevel) && !upgradeInitiated && !gameController.playerDead)
        {
            UpgradeAvailable();
            upgradeInitiated = true;
            targetScore += upgradeScoreIncrement;
        }
    }

    public void UpgradeAvailable()
    {
        upgradeAvailable = true;
        attackSpeedButton.interactable = true;
        //movementSpeedButton.interactable = true;
        StartCoroutine(FlashText());
    }

    public void UpgradeComplete()
    {
        upgradeAvailable = false;
        upgradeInitiated = false;
        HideUpgradeText();
        StopCoroutine(FlashText());
    }

    public void ShowUpgradeText()
    {
        upgradeAvailableText.enabled = true;
        pressZ.enabled = true;
        pressX.enabled = true;
    }

    public void HideUpgradeText()
    {
        upgradeAvailableText.enabled = false;
        pressZ.enabled = false;
        pressX.enabled = false;
    }

    IEnumerator FlashText()
    {
        while (upgradeAvailable)
        {
            yield return new WaitForSeconds(flashRate);
            ShowUpgradeText();
            yield return new WaitForSeconds(flashDuration);
            HideUpgradeText();
        }
    }
}
