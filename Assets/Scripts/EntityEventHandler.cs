using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all events that used by Entities
public class EntityEventHandler : MonoBehaviour {

	public Action<Vector2> OnMove;
	public Action<Vector2> OnLook;
	public Action OnRun;
	public Action OnRunEnd;
	public Action<Vector2> OnDash;
	public Action OnDashEnd;
	public Action OnPreAttack;
	public Action OnPreAttackEnd;
    public Action OnAttack;
	public Action OnAttackEnd;
	public Action OnHit;
	public Action OnDeath;
	public Action OnSelect;
	public Action OnSelectEnd;
	public Action<int> OnCollect;
	public Action OnFall;
	public Action OnLand;
	public Action OnSpecialEvent;
	public Action OnSpecialEventEnd;

	public void Move(Vector2 movement) {
		OnMove?.Invoke(movement);
	}
	public void Look(Vector2 look) {
		OnLook?.Invoke(look);
	}
	public void Run() {
		OnRun?.Invoke();
	}
	public void RunEnd() {
		OnRunEnd?.Invoke();
	}
	public void Dash(Vector2 dash) {
		OnDash?.Invoke(dash);
	}
	public void DashEnd() {
		OnDashEnd?.Invoke();
	}
	public void PreAttack() {
		OnPreAttack?.Invoke();
	}
	public void PreAttackEnd() {
		OnPreAttackEnd?.Invoke();
	}
	public void Attack() {
		OnAttack?.Invoke();
	}
	public void AttackEnd() {
		OnAttackEnd?.Invoke();
	}
	public void Hit() {
		OnHit?.Invoke();
	}
	public void Death() {
		OnDeath?.Invoke();
	}
	public void Select() {
		OnSelect?.Invoke();
	}
	public void SelectEnd() {
		OnSelectEnd?.Invoke();
	}
	public void Collect(int value) {
		OnCollect?.Invoke(value);
	}
	public void Fall() {
		OnFall?.Invoke();
	}
	public void Land() {
		OnLand?.Invoke();
	}
	public void SpecialEvent() {
		OnSpecialEvent?.Invoke();
	}
	public void SpecialEventEnd() {
		OnSpecialEventEnd?.Invoke();
	}


}
