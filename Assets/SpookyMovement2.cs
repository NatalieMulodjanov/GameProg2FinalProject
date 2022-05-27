using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.Events;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;
using System.IO;
using UnityEngine.SceneManagement;

public class SpookyMovement2 : MonoBehaviour
{
    public int score;
    private int bananas = 0;
    private int apples = 0;
    private int grapes = 0;
    public double timeRemaining;
    public TMP_Text text;
    public TMP_Text level;
    public GameObject collectibleSound;
    AudioSource audioSource;
    public GameObject damageSound;
    public GameObject collisionSound;
    AudioSource audioSource2;
    AudioSource audioSource3;
    public GameObject spooky;
    public TMP_Text timer;
    public TMP_Text bananaCounter;
    public TMP_Text appleCounter;
    public TMP_Text grapeCounter;
    public TMP_Text speedboostTimer;
    public GameObject door;
    public GameObject vfx;
    public GameObject screenDamage;
    public Slider slider;
    GameObject healthbar;
    public CharacterController controller;
    Animator animator;
    public bool speedBoost = false;
    public float speedBoostTimer = 0f;
    public bool healthBoost = false;

    public float ShakeDuration;          // Time the Camera Shake effect will last
    public float ShakeAmplitude;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Stopwatch globalStopwatch;
    private Stopwatch localLevelStopwatch;

    TimeSpan ts = new TimeSpan(0,5,0);

    TimeSpan speedBoostTime = new TimeSpan(0, 0, 10);
    Stopwatch speedBoostStopWatch = new Stopwatch();

    void Awake()
    {
        animator = GetComponent<Animator>();
        healthbar = GameObject.Find("healthbar");
        slider = healthbar.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = SpookyMovement.score;
        bananas = SpookyMovement.bananas;
        apples = SpookyMovement.apples;
        grapes = SpookyMovement.grapes;
        text.text = "Score: " + score;
        level.text = "Level 2";
        timer.text = "Time Remaining: ";
        this.globalStopwatch = new Stopwatch();
        this.localLevelStopwatch = new Stopwatch();
        if (File.Exists("stopwatch.json"))
        {
            Debug.Log("Read stopwatch from file");
            this.globalStopwatch = FileHelper.ReadFromFile<Stopwatch>("stopwatch.json");
        }
        // Resume global stopwatch
        this.globalStopwatch.Start();
        // Start local stopwatch
        this.localLevelStopwatch.Start();

        InvokeRepeating("UpdateTimer", 1, 1);
        //spooky = GameObject.Find("Spooky");
        collectibleSound = GameObject.Find("CollectibleSound");
        audioSource = collectibleSound.GetComponent<AudioSource>();
        damageSound = GameObject.Find("DamageSound");
        audioSource2 = damageSound.GetComponent<AudioSource>();
        collisionSound = GameObject.Find("CollisionSound");
        audioSource3 = collisionSound.GetComponent<AudioSource>();
        InvokeRepeating("ScreenNormal", 1, 2);
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool("isJumping");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jump");
            animator.SetBool("isJumping", true);
        }

        if (isGrounded && isJumping)
        {
            animator.SetBool("isJumping", false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (spooky.GetComponent<Transform>().position.y < -20)
        {
            Debug.Log("Spooky is dead!");
            MainMenu.GameOver();
        }

        if (slider.value == 0)
        {
            Debug.Log("Spooky is dead!");
            MainMenu.GameOver();
        }

        if (score < 5000 && this.ts.Subtract(globalStopwatch.Elapsed).TotalSeconds <= 0)
        {
            Debug.Log("Spooky is dead!");
            MainMenu.GameOver();
        }

        if (score > 5000 && SceneManager.GetActiveScene().name == "Level2")
        {
            Debug.Log("Level Complete!");
            LeaderboardHelper.SaveRecord(new PlayerRecord() { Name = MainMenu.nameInputValue ?? "", TimeElapsed = this.globalStopwatch.Elapsed.ToString() });
            MainMenu.Victory();
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }

        if (speedBoost == true)
        {
            speedboostTimer.text = "Speed Boost: " + speedBoostTime.Subtract(speedBoostStopWatch.Elapsed).Seconds;
            if (speedBoostTime.Subtract(speedBoostStopWatch.Elapsed).TotalSeconds <= 0)
            {
                // Debug.Log("Speed is now 12");
                speed = 12f;
                speedBoost = false;
                speedBoostStopWatch.Stop();
                speedBoostStopWatch.Reset();
                speedboostTimer.text = "";
            }
        }

        if (healthBoost == true && slider.value <= 0.95f)
        {
            slider.value += 0.05f;
            healthBoost = false;
        }
    }

    private void UpdateTimer()
    {
        var currentTime = this.ts.Subtract(globalStopwatch.Elapsed);
        string secondsString = "";
        if (currentTime.Seconds < 10)
        {
            secondsString += "0";
        }
        secondsString += currentTime.Seconds;
        timer.text = "Time Remaining: " + string.Format("{0}:{1}", currentTime.Minutes, secondsString);
    }

    public void ScreenNormal()
    {
        screenDamage.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            Debug.Log("Key Collected!");
            collider.gameObject.SetActive(false);
            vfx = GameObject.Find("Particle System");
            vfx.SetActive(false);
            door = GameObject.Find("Door");
            door.SetActive(false);
        }

        if (collider.gameObject.tag == "Banana")
        {
            Debug.Log("Banana Collected!");
            audioSource.Play();
            Destroy(collider.gameObject);
            score += 50;
            text.text = "Score: " + score;
            bananas++;
            bananaCounter.text = "" + bananas;
        }

        if (collider.gameObject.tag == "Apple")
        {
            Debug.Log("Apple Collected!");
            audioSource.Play();
            Destroy(collider.gameObject);
            score += 100;
            text.text = "Score: " + score;
            apples++;
            appleCounter.text = "" + apples;
        }

        if (collider.gameObject.tag == "Grape")
        {
            Debug.Log("Grape Collected!");
            audioSource.Play();
            Destroy(collider.gameObject);
            score += 250;
            text.text = "Score: " + score;
            grapes++;
            grapeCounter.text = "" + grapes;
        }

        if (collider.gameObject.tag == "Trap")
        {
            Debug.Log("Taking Damage!");
            audioSource2.Play();
            slider.value -= 0.05f;
            screenDamage.SetActive(true);
            ShakeElapsedTime = ShakeDuration;
        }

        if (collider.gameObject.tag == "Car")
        {
            Debug.Log("Accident!");
            audioSource3.Play();
            slider.value -= 0.05f;
            screenDamage.SetActive(true);
            ShakeElapsedTime = ShakeDuration;
        }

        if (collider.gameObject.tag == "SpeedBoost")
        {
            // Debug.Log("SpeedBoost Activated!");
            audioSource.Play();
            Destroy(collider.gameObject);
            speedBoost = true;
            this.speedBoostStopWatch.Start();
            speed = 18f;
        }

        if (collider.gameObject.tag == "HealthBoost")
        {
            // Debug.Log("HealthBoost Activated!");
            audioSource.Play();
            Destroy(collider.gameObject);
            healthBoost = true;
        }
    }
}

public class PlayerRecord
{
    public string Name { get; set; }
    public string TimeElapsed { get; set; }
}

public class Leaderboard
{
    public List<PlayerRecord> records { get; set; }
}
