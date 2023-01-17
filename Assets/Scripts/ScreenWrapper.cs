using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool _wrappingX = false;
    [SerializeField] private bool _wrappingY = false;
	[SerializeField] private bool _visibility = false;
	// Start is called before the first frame update
	void Start()
    {
        _renderer= GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _visibility = _renderer.isVisible;
        var wrappingCheck = !_visibility;
        if (!_visibility)
        {
            Vector2 newPosition= transform.position;
            if (math.abs(newPosition.x) > 1 && !_wrappingX)
            {
                newPosition.x *= -1;
                _wrappingX = true;
            }
            
            if(math.abs(newPosition.y) > 1 && !_wrappingY) { 
                newPosition.y *= -1;
				_wrappingY = true;
			}
            transform.position = newPosition;
		}
        else
            _wrappingX = _wrappingY = false;
    }
}
