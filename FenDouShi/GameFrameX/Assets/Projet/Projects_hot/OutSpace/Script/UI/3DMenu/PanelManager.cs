using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen;

	private int m_OpenParameterId;
	private int m_AppendParameterId;
	private Animator m_Open;
	private Animator m_ChildOpen;
	private GameObject m_PreviouslySelected;

	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	const string k_AppendTransitionName = "Append";

	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);
		m_AppendParameterId = Animator.StringToHash(k_AppendTransitionName);
		if (initiallyOpen == null)
			return;
		m_Open = null;
		OpenPanel(initiallyOpen);
		if (PlayerGun.targetLog != null)
		{
			PlayerGun.targetLog.gameObject.SetActive(false);
		}
		if (OutSpaceCameraManager.Instance.StartInput&& OutSpaceCameraManager.Instance.StartInput.Joysticks)
		{
			OutSpaceCameraManager.Instance.StartInput.Joysticks.SetActive(false);
		}

	}
    public void OnDisable()
    {
		if (PlayerGun.targetLog != null)
		{
			PlayerGun.targetLog.gameObject.SetActive(true);
		}
		//OutSpaceCameraManager.Instance.StartInput.Joysticks.SetActive(true);

	}

    public void OpenChildPanel(Animator anim)
	{
		Debug.Log("OpenChildPanel() anim=" + anim.gameObject.name );
		if (m_ChildOpen)
		{
			Debug.Log("OpenChildPanel()  " + "m_ChildOpen=" + m_ChildOpen.gameObject.name);
		}

		if (m_ChildOpen == anim)
			return;

		CloseChildPanel(m_ChildOpen);
		m_Open.SetBool(m_AppendParameterId, true);
		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

	//	CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;
		m_ChildOpen = anim;

		anim.SetBool(m_OpenParameterId, true);
		

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}
	public void OpenPanel (Animator anim)
	{
		if (m_Open == anim)
			return;
	
		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();
		
		CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;

		m_Open = anim;
		m_Open.SetBool(m_OpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}
	public void CloseChildPanel(Animator anim)
	{
		if (anim == null)
			return;
		if (m_Open == anim)
			return;
		m_Open.SetBool(m_AppendParameterId, false);
		anim.SetBool(m_OpenParameterId, false);

		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		m_Open.transform.SetAsLastSibling();
		//	CloseCurrent();
		m_PreviouslySelected = newPreviouslySelected;
		m_ChildOpen = null;
	}
	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;
        if (m_ChildOpen)
        {
			CloseChildPanel(m_ChildOpen);

		}
		m_Open.SetBool(m_OpenParameterId, false);
		SetSelected(m_PreviouslySelected);
		StartCoroutine(DisablePanelDeleyed(m_Open));
		m_Open = null;
	}

	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
	public void CloseAllView()
    {
		this.transform.parent.gameObject.SetActive(false);
    }
}
