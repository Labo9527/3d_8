using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour {

    public class Position
    {
        public float radius;
        public float angle1;
        public float angle2;
        public Position(float r, float a,float b)
        {
            radius = r;
            angle1 = a;
            angle2 = b;
        }
    }

    private ParticleSystem sys;              
    private ParticleSystem.Particle[] particals; 
    private Position[] positions;             

    public int num = 10000;       
    public float size = 0.3f;  
    public bool vary=true;

    void Start () {
        sys = this.GetComponent<ParticleSystem>();
        particals = new ParticleSystem.Particle[num];
        positions = new Position[num];

        var main = sys.main;
        main.startSpeed = 0;
        main.startSize = size;
        main.loop = false;
        main.maxParticles = num;
        sys.Emit(num);
        sys.GetParticles(particals);

        for (int i = 0; i < num; i++)
        {
            float radius=3f;
            float angle1 = Random.Range(0f, 360f);
            float radian1 = angle1 * 180 / Mathf.PI;
            float angle2 = Random.Range(0f, 360f);
            float radian2 = angle2 * 180 / Mathf.PI;
            
            positions[i] = new Position(radius, angle1,angle2);
            particals[i].position =
                new Vector3(positions[i].radius * Mathf.Cos(radian2) * Mathf.Cos(radian1),
                            positions[i].radius * Mathf.Sin(radian2),
                            positions[i].radius * Mathf.Cos(radian2) * Mathf.Sin(radian1));
        }
        sys.SetParticles(particals, particals.Length);
	}
	
	void Update () {
        if(vary==true)
        {
            for(int i=0;i<num;i++)
            {
                positions[i].radius-=0.01f;
            }
            if(positions[0].radius<=0.5){
                vary=false;
            }
        }
        else{
            for(int i=0;i<num;i++)
            {
                positions[i].radius+=0.01f;
            }
            if(positions[0].radius>=3){
                vary=true;
            }
        }

        for (int i = 0; i < num; i++)
        {
            positions[i].angle1 = (positions[i].angle1 - Random.Range(0.5f,3f)) % 360f;
            positions[i].angle2 = (positions[i].angle2 - Random.Range(0.5f,3f) + 180) % 360f - 180;             
            float radian1 = positions[i].angle1 / 180 * Mathf.PI;
            float radian2 = positions[i].angle2 / 180 * Mathf.PI;
            particals[i].position = new Vector3(positions[i].radius * Mathf.Cos(radian2) * Mathf.Cos(radian1),
                            positions[i].radius * Mathf.Sin(radian2),
                            positions[i].radius * Mathf.Cos(radian2) * Mathf.Sin(radian1));
        }
        sys.SetParticles(particals, particals.Length);
    }

}