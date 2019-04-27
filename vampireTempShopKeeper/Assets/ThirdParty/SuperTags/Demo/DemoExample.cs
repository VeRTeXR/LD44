using System.Diagnostics;
using Assets.SuperTags;
using Assets.SuperTags.Demo;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SuperTags.Demo
{
	public class DemoExample : MonoBehaviour
	{
		[SerializeField] private GameObject[] _prefabs;
		[SerializeField] private Material[] _materials;
		private string _currentlySelectedTag = string.Empty;
		private Vector3 _rallyPoint = new Vector3(0f, 0f, -2f);
		private const int MapSize = 100;
		private const int ShapesAmount = 1000;

		private void Start ()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			for (var i = 0; i < ShapesAmount; i++)
			{
				var x = Random.Range(0, 100);
				var y = Random.Range(0, 100);
				var randPrefab = Random.Range(0, _prefabs.Length);
				var randMaterial = Random.Range(0, _materials.Length);
				var go = Instantiate(_prefabs[randPrefab]);
				go.GetComponent<MeshRenderer>().material = _materials[randMaterial];
				go.transform.position = new Vector3(x, 0f, y);
				go.AddComponent<ShapeController>();
				var superTags = go.AddComponent<SuperTag>();
				superTags.AddTag(_prefabs[randPrefab].name);
				superTags.AddTag(_materials[randMaterial].name);
			}
			Debug.Log("Scene loading took: " + stopwatch.ElapsedMilliseconds + " miliseconds.");
		}

		private void OnGUI()
		{
			for (var i = 0; i < _prefabs.Length; i++)
			{
				if (GUI.Button(new Rect(50f * i, 0f, 50f, 25f), _prefabs[i].name))
				{
//				Debug.Log("Button " + _prefabs[i].name + " was pressed.");
					ReturnToOriginalPosition(_currentlySelectedTag);
					_currentlySelectedTag = _prefabs[i].name;
					AttractGameObjectWithTag(_currentlySelectedTag);
				}
			}
		
			for (var i = 0; i < _materials.Length; i++)
			{
				if (GUI.Button(new Rect(50f * i, 30f, 50f, 25f), _materials[i].name))
				{		
//				Debug.Log("Button " + _materials[i].name + " was pressed.");
					ReturnToOriginalPosition(_currentlySelectedTag);
					_currentlySelectedTag = _materials[i].name;
					AttractGameObjectWithTag(_currentlySelectedTag);
				}
			}
		}

		private static void ReturnToOriginalPosition(string tagName)
		{
			var objs = SuperTagsSystem.GetObjectsWithTag(tagName);
			if (objs == null) return;
			for (var i  = 0; i < objs.Count; i++)
			{
				objs[i].GetComponent<ShapeController>().ReturnToOriginalPos();
			}
		}

		private void AttractGameObjectWithTag(string tagName)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var objs = SuperTagsSystem.GetObjectsWithTag(tagName);
			Debug.Log("Selection took: " + stopwatch.ElapsedMilliseconds + " miliseconds.");
			for (var i = 0; i < objs.Count; i++)
			{
				_rallyPoint.x = Random.Range(0f, (float) MapSize);
				objs[i].GetComponent<ShapeController>().GoTo(_rallyPoint);
			}
		}
	
	}
}
