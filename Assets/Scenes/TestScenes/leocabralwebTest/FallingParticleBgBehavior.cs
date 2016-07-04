using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingParticleBgBehavior : MonoBehaviour {

	public GameObject FallingParticle;
	public GameObject prefab;
	public Transform PontoA;
	public Transform PontoB;
	public Transform SpawnEnd;
	public int NumParticles = 20;

	private List<GameObject> _particles = new List<GameObject> ();

	private float _minY;
	private float _maxY;
	private float _minX;
	private float _maxX;

	private bool _debug = true;

	// Use this for initialization
	void Start () 
	{
		_minY = Mathf.Min(PontoA.transform.localPosition.y, SpawnEnd.transform.localPosition.y);
		_maxY = Mathf.Max(PontoA.transform.localPosition.y, SpawnEnd.transform.localPosition.y);
		_minX = Mathf.Min(PontoA.transform.localPosition.x, PontoB.transform.localPosition.x);
		_maxX = Mathf.Max(PontoA.transform.localPosition.x, PontoB.transform.localPosition.x);
	
//		_particles = new GameObject[NumParticles];
		for (int i = 0; i < NumParticles; i++)
		{
			GameObject fallingParticle = Instantiate(FallingParticle) as GameObject;
			fallingParticle.transform.SetParent(this.transform);
			fallingParticle.transform.localPosition = new Vector3(Random.Range(_minX + 0.2f, _maxX - 0.2f), Random.Range(_minY + 0.2f, _maxY - 0.2f), 0.0f);

			FallingParticleBehavior behavior = fallingParticle.GetComponent ("FallingParticleBehavior") as FallingParticleBehavior;
			behavior.speed = 3;
			behavior.targetObject = prefab;
			behavior.yLimit = PontoB.position.y;

			fallingParticle.SetActive(true);
			_particles.Add (fallingParticle);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		//só o desenho do quadrado verde e do circulo, para facilitar a visualização de onde as moscas podem voar
		if (_debug)
		{
			Vector3 PontoSpawnStart = new Vector3(PontoA.position.x, SpawnEnd.position.y, 0.0f);
			Vector3 PontoSpawnEnd = new Vector3(PontoB.position.x, SpawnEnd.position.y, 0.0f);

			Vector3 ShowAreaA = new Vector3(PontoB.position.x, PontoA.position.y, 0.0f);
			Vector3 PontoA2 = new Vector3(PontoB.position.x, PontoA.position.y, 0.0f);
			Vector3 PontoB2 = new Vector3(PontoA.position.x, PontoB.position.y, 0.0f);


			// Spawn Area
			Gizmos.color = Color.red;
			Gizmos.DrawLine(PontoA.position, PontoA2);
			Gizmos.DrawLine(PontoA2, PontoSpawnEnd);
			Gizmos.DrawLine(PontoSpawnEnd, PontoSpawnStart);
			Gizmos.DrawLine(PontoSpawnStart, PontoA.position);

			// Show Area
			Gizmos.color = Color.green;
			Gizmos.DrawLine(PontoSpawnEnd, PontoB.position);
			Gizmos.DrawLine(PontoB.position, PontoB2);
			Gizmos.DrawLine(PontoB2, PontoSpawnStart);

			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, 0.1f * transform.localScale.magnitude);
		}
	}
}
