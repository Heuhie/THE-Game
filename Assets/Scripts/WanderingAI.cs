using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballprefab;
    [SerializeField] private Transform shootFrom;
    private GameObject _fireball;
    private Animator _animator;
    private CharacterController _charcontroller;
    private bool _alive;
    //private GameObject _gameobject;

    public float rotSpeed = 8.0f;
    public float moveSpeed = 3.0f;
    public float obstacleRange = 5.0f;
    public int rangeX = -100;
    public int rangeY = 100;

    // Start is called before the first frame update
    void Start()
    {
        //_gameobject = GetComponent<GameObject>;
        _charcontroller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _alive = true;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            gameObject.transform.Translate(0, 0, moveSpeed * Time.deltaTime);

            Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballprefab) as GameObject;
                        _fireball.transform.position = shootFrom.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = shootFrom.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {


                    Quaternion tmp = transform.rotation;
                    float angle = Random.Range(rangeX, rangeY);
                    gameObject.transform.Rotate(0, angle, 0);
                    Quaternion direction = gameObject.transform.rotation;
                    //transform.rotation = tmp;
                    gameObject.transform.rotation = Quaternion.Lerp(tmp, direction, rotSpeed * Time.deltaTime);



                }
            }

            _animator.SetFloat("Speed", moveSpeed);

        } 
    }

    public void SetALive(bool alive)
    {
        _alive = alive;
    }
}
