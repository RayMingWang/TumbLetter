using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TumblerCenter : MonoBehaviour {

    public float scale = 0.05f;
    public Vector3 centermass;
    public SimpleHelvetica text;
    public bool random_letter = false;
    public float push_force = 1500f;

    public AudioSource audio; 
    public TumblerManager manager;


    Transform letter;
    MeshCollider letter_collider;
    Rigidbody letter_rigidbody;
    string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    Rigidbody rb;
    Vector3 originposition;
    Vector3 hitmarker;
    bool first_contact = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centermass;

        if (random_letter)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            text.Text = Alphabet[(int)(Random.value * 26f - 0.1f)];
            text.GenerateText();



            letter = text.transform.GetChild(1);

            //letter.localScale = letter.localScale * transform.localScale.x;
            transform.localScale = transform.localScale * scale;

            // rb.centerOfMass = centermass * scale;

            letter_collider = letter.gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            letter_rigidbody = letter.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            letter_collider.convex = true;
            transform.GetComponent<FixedJoint>().connectedBody = letter_rigidbody;
            letter.GetComponent<Renderer>().material.color = //new Color(0.5f, 1, 1);
             GoldenColor.GenerateColor(0.6f, 0.95f, 0f);


        }
        else
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            letter = text.transform.GetChild(1);
            letter_collider = letter.gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            letter_rigidbody = letter.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            letter_collider.convex = true;
            transform.GetComponent<FixedJoint>().connectedBody = letter_rigidbody;

        }


    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (letter_collider.Raycast(ray, out hit, 100.0F))
                hitmarker = hit.point;
            Vector3 Force_apply_point = hit.point + (Camera.main.transform.position - hit.point).normalized * 0.05f;
            Debug.Log("Mouse Push!");
            //letter_rigidbody.AddForceAtPosition(-(Camera.main.transform.position - hit.point).normalized * push_force, Force_apply_point, ForceMode.Impulse);
            //letter_rigidbody.AddForceAtPosition(-(Camera.main.transform.position - hit.point).normalized * push_force, Force_apply_point, ForceMode.Impulse);
            letter_rigidbody.AddExplosionForce(push_force,
                                           hit.point+ - (Camera.main.transform.position - hit.point).normalized * scale*0.01f,
                                           scale * 0.01f,
                                           0, 
                                           ForceMode.Impulse);

        }

    }

    void Update()
    {

    }

    public void selfDestruct()
    {
        transform.GetComponent<Animator>().Play("poof");
        rb.useGravity = false;
        letter_rigidbody.useGravity = false;
        StartCoroutine(ComitDestruct());
        letter_rigidbody.AddExplosionForce( 900f ,
                               transform.position- new Vector3(0, scale * 0.01f, 0),
                               scale * 0.01f,
                               0,
                               ForceMode.Acceleration);
    }

    IEnumerator ComitDestruct()
    {
        
        yield return new WaitForSeconds(audio.clip.length);
        manager.change_Tumbler();
        Destroy(gameObject);
    }


}
