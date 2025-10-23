using System.Collections;
using UnityEngine;

internal class UVTextureAnimator : MonoBehaviour
{
	public Material[] AnimatedMaterialsNotInstance;

	public int Rows = 4;

	public int Columns = 4;

	public int StartFrame;

	public int EndFrame;

	public float Fps = 20f;

	public int OffsetMat;

	public Vector2 SelfTiling;

	public bool IsLoop = true;

	public bool IsReverse;

	public bool IsRandomOffsetForInctance;

	public bool IsBump;

	public bool IsHeight;

	public Event<UVTextureAnimator> FinishedEvent;

	private bool isInitialised;

	private int index;

	private int count;

	private int allCount;

	private bool isVisible;

	private bool isCoroutineStarted;

	private Renderer currentRenderer;

	private Material instanceMaterial;

	private WaitForSeconds coroutineWaiter;

	public bool IsPlaying => isCoroutineStarted;

	public void Reset()
	{
		index = 0;
		allCount = 0;
	}

	private void Awake()
	{
		InitDefaultVariables();
		isInitialised = true;
	}

	public void SetInstanceMaterial(Material mat, Vector2 offsetMat)
	{
		instanceMaterial = mat;
		InitDefaultVariables();
	}

	private void InitDefaultVariables()
	{
		currentRenderer = GetComponent<Renderer>();
		allCount = 0;
		float seconds = 1f / Fps;
		coroutineWaiter = new WaitForSeconds(seconds);
		count = Rows * Columns;
		if (EndFrame != StartFrame)
		{
			count = Mathf.Min(EndFrame - StartFrame, count);
		}
		else
		{
			StartFrame = 0;
			EndFrame = count - 1;
		}
		index = StartFrame;
		Vector2 value = new Vector2((float)index / (float)Columns - (float)(index / Columns), 1f - (float)(index / Columns) / (float)Rows);
		OffsetMat = ((!IsRandomOffsetForInctance) ? (OffsetMat - OffsetMat / count * count) : Random.Range(0, count));
		Vector2 value2 = ((SelfTiling == Vector2.zero) ? new Vector2(1f / (float)Columns, 1f / (float)Rows) : SelfTiling);
		if (AnimatedMaterialsNotInstance.Length != 0)
		{
			Material[] animatedMaterialsNotInstance = AnimatedMaterialsNotInstance;
			foreach (Material material in animatedMaterialsNotInstance)
			{
				material.SetTextureScale("_MainTex", value2);
				material.SetTextureOffset("_MainTex", Vector2.zero);
				if (IsBump)
				{
					material.SetTextureScale("_BumpMap", value2);
					material.SetTextureOffset("_BumpMap", Vector2.zero);
				}
				if (IsHeight)
				{
					material.SetTextureScale("_HeightMap", value2);
					material.SetTextureOffset("_HeightMap", Vector2.zero);
				}
			}
		}
		else if (instanceMaterial != null)
		{
			instanceMaterial.SetTextureScale("_MainTex", value2);
			instanceMaterial.SetTextureOffset("_MainTex", value);
			if (IsBump)
			{
				instanceMaterial.SetTextureScale("_BumpMap", value2);
				instanceMaterial.SetTextureOffset("_BumpMap", value);
			}
			if (IsBump)
			{
				instanceMaterial.SetTextureScale("_HeightMap", value2);
				instanceMaterial.SetTextureOffset("_HeightMap", value);
			}
		}
		else if (currentRenderer != null)
		{
			currentRenderer.material.SetTextureScale("_MainTex", value2);
			currentRenderer.material.SetTextureOffset("_MainTex", value);
			if (IsBump)
			{
				currentRenderer.material.SetTextureScale("_BumpMap", value2);
				currentRenderer.material.SetTextureOffset("_BumpMap", value);
			}
			if (IsBump)
			{
				currentRenderer.material.SetTextureScale("_HeightMap", value2);
				currentRenderer.material.SetTextureOffset("_HeightMap", value);
			}
		}
	}

	private void OnEnable()
	{
		if (isInitialised)
		{
			InitDefaultVariables();
		}
		isVisible = true;
		if (!isCoroutineStarted)
		{
			StartCoroutine(UpdateCoroutine());
		}
	}

	private void OnDisable()
	{
		isCoroutineStarted = false;
		isVisible = false;
		StopAllCoroutines();
	}

	private void OnBecameVisible()
	{
		isVisible = true;
		if (!isCoroutineStarted)
		{
			StartCoroutine(UpdateCoroutine());
		}
	}

	private void OnBecameInvisible()
	{
		isVisible = false;
	}

	private IEnumerator UpdateCoroutine()
	{
		isCoroutineStarted = true;
		while (isVisible && (IsLoop || allCount != count))
		{
			UpdateCoroutineFrame();
			if (!IsLoop && allCount == count)
			{
				break;
			}
			yield return coroutineWaiter;
		}
		isCoroutineStarted = false;
		FinishedEvent.Send(this);
	}

	private void UpdateCoroutineFrame()
	{
		if (currentRenderer == null && instanceMaterial == null && AnimatedMaterialsNotInstance.Length == 0)
		{
			return;
		}
		allCount++;
		if (IsReverse)
		{
			index--;
		}
		else
		{
			index++;
		}
		if (index > EndFrame)
		{
			index = StartFrame;
		}
		if (index < StartFrame)
		{
			index = EndFrame;
		}
		if (AnimatedMaterialsNotInstance.Length != 0)
		{
			for (int i = 0; i < AnimatedMaterialsNotInstance.Length; i++)
			{
				int num = i * OffsetMat + index;
				num -= num / count * count;
				Vector2 value = new Vector2((float)num / (float)Columns - (float)(num / Columns), 1f - (float)(num / Columns) / (float)Rows);
				AnimatedMaterialsNotInstance[i].SetTextureOffset("_MainTex", value);
				if (IsBump)
				{
					AnimatedMaterialsNotInstance[i].SetTextureOffset("_BumpMap", value);
				}
				if (IsHeight)
				{
					AnimatedMaterialsNotInstance[i].SetTextureOffset("_HeightMap", value);
				}
			}
			return;
		}
		Vector2 value2;
		if (IsRandomOffsetForInctance)
		{
			int num2 = index + OffsetMat;
			value2 = new Vector2((float)num2 / (float)Columns - (float)(num2 / Columns), 1f - (float)(num2 / Columns) / (float)Rows);
		}
		else
		{
			value2 = new Vector2((float)index / (float)Columns - (float)(index / Columns), 1f - (float)(index / Columns) / (float)Rows);
		}
		if (instanceMaterial != null)
		{
			instanceMaterial.SetTextureOffset("_MainTex", value2);
			if (IsBump)
			{
				instanceMaterial.SetTextureOffset("_BumpMap", value2);
			}
			if (IsHeight)
			{
				instanceMaterial.SetTextureOffset("_HeightMap", value2);
			}
		}
		else if (currentRenderer != null)
		{
			currentRenderer.material.SetTextureOffset("_MainTex", value2);
			if (IsBump)
			{
				currentRenderer.material.SetTextureOffset("_BumpMap", value2);
			}
			if (IsHeight)
			{
				currentRenderer.material.SetTextureOffset("_HeightMap", value2);
			}
		}
	}
}
