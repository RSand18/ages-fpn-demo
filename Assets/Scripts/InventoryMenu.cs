using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryMenuItemTogglePrefab;

    [Tooltip("Content of the scrollview for the list of inventory items.")]
    [SerializeField]
    private Transform inventoryListContentArea;

    [Tooltip("Place in the UI for displaying the name of the selected inventory item.")]
    [SerializeField]
    private TextMeshProUGUI itemLabelText;

    [Tooltip("Place in the UI for displaying info about the selected inventory item.")]
    [SerializeField]
    private TextMeshProUGUI descriptionAreaText;


    private static InventoryMenu instance;
    private CanvasGroup canvasGroup;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;
    private AudioSource audioSource;
    private ToggleGroup toggleGroup;
    public static InventoryMenu Instance
    {
        get
        {
            if (instance == null)
            {
                throw new System.Exception("There is currently no Inventory Menu instance, " +
                    "but you are trying to access it! Make sure the Inventory Menu script is applied to a GameObject in your scene!");
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private bool IsVisible => canvasGroup.alpha > 0;

    public void ExitMenuButtonClicked()
    {
        HideMenu();
    }

    /// <summary>
    /// Instantiates a new InventoryMenuItemToggle prefab and adds it to the menu. 
    /// </summary>
    /// <param name="inventoryObjectToAdd"></param>
    public void AddItemToMenu(InventoryObject inventoryObjectToAdd)
    {
        GameObject clone = Instantiate(inventoryMenuItemTogglePrefab, inventoryListContentArea);
        InventoryMenuItemToggle toggle = clone.GetComponent<InventoryMenuItemToggle>();
        toggle.AssociatedInventoryObject = inventoryObjectToAdd;
    }

    private void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        rigidbodyFirstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Play();
    }

    private void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidbodyFirstPersonController.enabled = true;
        audioSource.Play();
    }

    /// <summary>
    /// This is the event handler for InventoryMenuItemSelected
    /// </summary>
    private void OnInventoryMenuItemSelected(InventoryObject inventoryItemThatWasSelected)
    {
        itemLabelText.text = inventoryItemThatWasSelected.ObjectName;
        descriptionAreaText.text = inventoryItemThatWasSelected.Description;
    }

    private void OnEnable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected += OnInventoryMenuItemSelected;
    }

    private void OnDisable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected -= OnInventoryMenuItemSelected;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("ShowInventory"))
        {
            if ((IsVisible))
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       else
        {
            throw new System.Exception("There is already an instance of InventoryMenu and there can only be one.");
        }
        canvasGroup = GetComponent<CanvasGroup>();
        rigidbodyFirstPersonController = FindObjectOfType<RigidbodyFirstPersonController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        HideMenu();
        StartCoroutine(WaitForAudioClip());
        Debug.Log("We're not done waiting.");
    }

    private IEnumerator WaitForAudioClip()
    {
        float originalVolume = audioSource.volume;
        audioSource.volume = 0;
        Debug.Log("Start waiting.");
        yield return new WaitForSeconds(audioSource.clip.length);
        Debug.Log("Done'Waiting.");
        audioSource.volume = originalVolume;
    }
}
