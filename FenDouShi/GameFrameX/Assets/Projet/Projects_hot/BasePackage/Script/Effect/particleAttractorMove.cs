using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorMove : MonoBehaviour {
	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;
	public Collider target;
	public float speed = 5f;
	int numParticlesAlive;
	void Start () {
		ps = GetComponent<ParticleSystem>();
		if (!GetComponent<Transform>()){
			GetComponent<Transform>();
		}
	}
	void Update () {
		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
		numParticlesAlive = ps.GetParticles(m_Particles);
		float step = speed * Time.deltaTime;
		for (int i = 0; i < numParticlesAlive; i++) {
		//	m_Particles[i].position = Vector3.MoveTowards(m_Particles[i].position, target.transform.position, step);
			
			if (target.bounds.Contains(m_Particles[i].position))
            {
				m_Particles[i].remainingLifetime = 0;

			}
		}
		ps.SetParticles(m_Particles, numParticlesAlive);
		
	}
}
