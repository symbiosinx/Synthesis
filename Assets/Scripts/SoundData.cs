using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName="SoundData", menuName="Sound Data", order=1)]
public class SoundData : ScriptableObject {

	[HorizontalLine(2, EColor.Gray)]

	[ValidateInput("WaveformsValidInInspector", "Must contain at least one waveform")]
	public Waveform[] possibleWaveforms = new Waveform[] { Waveform.Sine };
	bool WaveformsValidInInspector(Waveform[] wfs) {
		return wfs.Length > 0;
	}
	[EnableIf("ShowDutyInInspector"), MinMaxSlider(0.01f, .5f)] public Vector2 duty = new Vector2(0.5f, 0.5f);
	bool ShowDutyInInspector() {
		foreach (Waveform w in possibleWaveforms) {
			if (w==Waveform.Pulse) return true;
		} return false;
	}
	[MinMaxSlider(0f, 1f)] public Vector2 phaseShift =  new Vector2(0f, 0f);
	[HorizontalLine(2, EColor.Gray)]
	[MinMaxSlider(0, 1000)] public Vector2 frequency =  new Vector2(500f, 500f);
	[MinMaxSlider(-200, 200)] public Vector2 speed =  new Vector2(0f, 0f);
	[MinMaxSlider(-200, 200)] public Vector2 acceleration =  new Vector2(0, 0f);
	[MinMaxSlider(-200, 200)] public Vector2 jerk =  new Vector2(0, 0f);
	[HorizontalLine(2, EColor.Gray)]
	[MinMaxSlider(0.01f, 1f)] public Vector2 attackLength =  new Vector2(0.01f, 0.01f);
	[MinMaxSlider(0.01f, 1f)] public Vector2 decayLength =  new Vector2(0.25f, 0.25f);
	[MinMaxSlider(0f, 1f)] public Vector2 sustainLevel =  new Vector2(.5f, .5f);
	[MinMaxSlider(0f, 1f)] public Vector2 sustainLength =  new Vector2(.1f, .1f);
	[MinMaxSlider(0.01f, 1f)] public Vector2 releaseLength =  new Vector2(.1f, .1f);


	[Button] void Mutate() {
		Vector2 mutateValue(Vector2 value, float range) {
			value.x = Random.Range(value.x-range*.5f, value.x+range*.5f);
			value.y = Random.Range(value.y-range*.125f, value.y+range*.125f);
			return value;
		}
		frequency = mutateValue(frequency, 100f);
	}

}

public enum Waveform {
	Sine,
	Pulse,
	Sawtooth,
	Triangle,
	Tangent,
	Secant,
	WhiteNoise,
	SoftNoise,
	PureWhiteNoise,

};