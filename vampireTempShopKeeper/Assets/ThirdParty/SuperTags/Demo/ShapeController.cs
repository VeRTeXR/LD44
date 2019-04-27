using UnityEngine;

namespace Assets.SuperTags.Demo
{
	public class ShapeController : MonoBehaviour
	{
		private Vector3 _originalPos;
		private Vector3 _selectedPos;
		private float _interpolation = 1f;
		private bool _returningToOriginalPos = true;
		private bool _isMoving;

		private void Awake()
		{
			_originalPos = transform.position;
			_isMoving = false;
		}

		public void GoTo(Vector3 targetPos)
		{
			_isMoving = true;
			_returningToOriginalPos = false;
			_selectedPos = targetPos;
		}

		public void ReturnToOriginalPos()
		{
			_isMoving = true;
			_returningToOriginalPos = true;
			transform.position = _originalPos; 
		}

		void Update()
		{
			if (_isMoving)
			{
				MoveTowardsDestination();
			}
		}

		private void MoveTowardsDestination()
		{
			if (!_returningToOriginalPos)
			{
				_interpolation = Mathf.Clamp01(_interpolation + Time.deltaTime);
				transform.position = Vector3.Lerp(_originalPos, _selectedPos, _interpolation);
			}
			else
			{
				_interpolation = Mathf.Clamp01(_interpolation - Time.deltaTime);
				transform.position = Vector3.Lerp(_originalPos, _selectedPos, _interpolation);
			}

			if (_interpolation == 1f)
			{
				_isMoving = false;
			}
		}
	}
}
