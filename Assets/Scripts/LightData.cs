using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LightData : MonoBehaviour
{
	private Light mainLight;

	void Start()
	{
		this.mainLight = this.GetComponent<Light>();
	}

	void Update ()
	{
		if(this.mainLight == null)
		{
			Debug.LogWarning("There is no light component on this gameObject" + this.gameObject.name);
			return;
		}

		if (this.mainLight.type == LightType.Directional)
		{
			float wComponent = 0.0f;
			Vector4 mainLight = new Vector4(this.mainLight.transform.forward.x,
											this.mainLight.transform.forward.y,
											this.mainLight.transform.forward.z, 
											wComponent);

			Shader.SetGlobalVector("WorldSpaceLight", mainLight);
        }
		else if (this.mainLight.type == LightType.Point)
		{
			float wComponent = 1.0f;
			Vector4 mainLight = new Vector4(this.mainLight.transform.position.x,
											this.mainLight.transform.position.y,
											this.mainLight.transform.position.z,
											wComponent);

			Shader.SetGlobalVector("WorldSpaceLight", mainLight);
		}

		Shader.SetGlobalVector("LightColor", this.mainLight.color * this.mainLight.intensity);
		//Shader.SetGlobalVector("LightIntenis", );
	}
}
