using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public Vector2 timeBetweenSpawns;
    float currentSpawnTimer;

    public int SpawnCap;
    public int currentSpawned;

    public Transform SpawnPoint;
    public Transform[] spawnPointTargets;
    public float Speed;


    void Start()
    {
        //CreateShadeBall();
       var Character = CreateCharacter();
        Character.transform.position = new Vector3(65, 10, 45);
        Character.transform.localScale = new Vector3(3, 3, 3);
        Character.transform.Rotate(55, 0, 0);
    }


    GameObject CreateShadeBall()
    {

        System.Type[] specs = new System.Type[4] { typeof(MeshRenderer), typeof(SphereCollider), typeof(MeshFilter), typeof(Rigidbody) };
        GameObject shadeBall = new GameObject("ShadeBall", specs);

        Material SphereMaterial = new Material(Shader.Find("Standard (Specular setup)"));
        shadeBall.GetComponent<MeshRenderer>().material = SphereMaterial;
        SphereMaterial.SetColor("_Color", new Color(0, 0, 0, 1));
        SphereMaterial.SetColor("_SpecColor", new Color(0.65f, 0.65f, 0.65f, 1));



        //Two ways
        //1) 
        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shadeBall.GetComponent<MeshFilter>().mesh = primitive.GetComponent<MeshFilter>().mesh;
        Destroy(primitive);

        //2)
        //shadeBall.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("3D Objects/FmPlane");

        return shadeBall;
    }

    GameObject CreateCharacter()
    {

        System.Type[] specs = new System.Type[3] { typeof(MeshRenderer), typeof(MeshCollider), typeof(MeshFilter) };
        GameObject character = new GameObject("Character", specs);

        Material charMaterial = new Material(Shader.Find("Unlit/Transparent"));
        character.GetComponent<MeshRenderer>().material = charMaterial;
        charMaterial.SetTexture("_MainTex", Resources.Load<Texture>("Textures/CharTex")); 


        character.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("3D Objects/FmPlane");

        return character;
    }

    void MoveSpawnPoint()
    {
        SpawnPoint.position = Vector3.Lerp(spawnPointTargets[0].position, spawnPointTargets[1].position, Mathf.Sin(Time.time * Speed) * 0.5f + 0.5f);
    }

    private void Update()
    {

        MoveSpawnPoint();

        if(currentSpawned < SpawnCap)
        {
            currentSpawnTimer -= Time.deltaTime;
            if(currentSpawnTimer <= 0)
            {
                var ShadeBall = CreateShadeBall();
                ShadeBall.transform.position = SpawnPoint.position;

                ShadeBall.transform.localScale = new Vector3(4, 4, 4);
                ShadeBall.transform.parent = transform;
                ShadeBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Random.Range(5, 10)), ForceMode.Impulse);
                currentSpawnTimer = Random.Range(timeBetweenSpawns.x, timeBetweenSpawns.y);

            }
        }
    }

}
