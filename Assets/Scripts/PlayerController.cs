using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    private Rigidbody rb;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    public bool forceField;
    public GameObject ffSphere;

    private float nextFire;

    public AudioClip weapon_player;
    public AudioSource musicSource;

    public Image circle;
    private float waitTime = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        musicSource.clip = weapon_player;
        forceField = true;
        ffSphere.SetActive(false);
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            musicSource.Play();
        }
        if(Input.GetKey(KeyCode.Space) && forceField == true)
        {
            GameObject clone = Instantiate(ffSphere, transform);
            clone.transform.parent = gameObject.transform;
            forceField = false;
            clone.SetActive(true);
            //StartCoroutine(ForceFieldTimer());
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    public void ffCollision()
    {
        circle.fillAmount = 0;

        StartCoroutine(ForceFieldTimer());
        //circle.fillAmount = (1 / waitTime);
        StartCoroutine(UITimer());
        
    }

    IEnumerator ForceFieldTimer()
    {
        yield return new WaitForSeconds(waitTime);
        forceField = true;
    }

    IEnumerator UITimer()
    {
        while(circle.fillAmount < 1)
        {
            circle.fillAmount += (1.0f / waitTime);
            yield return new WaitForSeconds(1);
        }
    }

}