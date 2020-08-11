using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesizer : MonoBehaviour {
    
	[SerializeField] ComputeShader compute;
	public static Synthesizer instance;
	const int sampleRate = 44100;

	void Awake() {
		instance = this;
	}

    public AudioClip GenerateSignal(SoundData soundData) {
		int signalKernel = compute.FindKernel("GenerateSignal");

		float GetRandomValue(Vector2 s) {
			return Random.Range(s.x, s.y);
		}

		float A = GetRandomValue(soundData.attackLength);
		float D = GetRandomValue(soundData.decayLength);
		float L = GetRandomValue(soundData.sustainLength);
		float R = GetRandomValue(soundData.releaseLength);

		compute.SetFloat("frequency", GetRandomValue(soundData.frequency));
		compute.SetFloat("speed", GetRandomValue(soundData.speed));
		compute.SetFloat("acceleration", GetRandomValue(soundData.acceleration));
		compute.SetFloat("jerk", GetRandomValue(soundData.jerk));
		compute.SetFloat("attackLength", A);
		compute.SetFloat("decayLength", D);
		compute.SetFloat("sustainLevel", GetRandomValue(soundData.sustainLevel));
		compute.SetFloat("sustainLength", L);
		compute.SetFloat("releaseLength", R);
		compute.SetInt("waveformIndex", (int)soundData.possibleWaveforms[Random.Range(0, soundData.possibleWaveforms.Length)]);
		compute.SetFloat("duty", GetRandomValue(soundData.duty));
		compute.SetFloat("phaseShift", GetRandomValue(soundData.phaseShift));
		compute.SetFloat("seed", Random.Range(1f, 10f));

		float signalDuration = A + D + L + R;
		int samples = Mathf.CeilToInt(signalDuration * sampleRate);

		float[] signal = new float[samples];
		ComputeBuffer signalBuffer = new ComputeBuffer(signal.Length, sizeof(float));
		signalBuffer.SetData(signal);
		compute.SetBuffer(signalKernel, "signal", signalBuffer);

		compute.Dispatch(signalKernel, samples/1024, 1, 1);
		signalBuffer.GetData(signal);
		signalBuffer.Release();

		AudioClip audioClip = AudioClip.Create(soundData.name, signal.Length, 1, sampleRate, false);
		audioClip.SetData(signal, 0);

		return audioClip;
    }

	public Vector2[] Fourier(float[] signal) {
		int fourierKernel = compute.FindKernel("Fourier");

		ComputeBuffer signalBuffer = new ComputeBuffer(signal.Length, sizeof(float));
		signalBuffer.SetData(signal);
		compute.SetBuffer(fourierKernel, "signal", signalBuffer);

		Vector2[] frequencies = new Vector2[5000];
		ComputeBuffer frequenciesBuffer = new ComputeBuffer(frequencies.Length, sizeof(float)*2);
		frequenciesBuffer.SetData(frequencies);
		compute.SetBuffer(fourierKernel, "frequencies", frequenciesBuffer);

		compute.SetInt("inputSize", signal.Length);
		compute.SetInt("outputSize", frequencies.Length);
		compute.SetFloat("outputRange", 1000f);

		// compute.Dispatch(fourierKernel, Mathf.CeilToInt(signal.Length * frequencies.Length / 1024f), 1, 1);
		compute.Dispatch(fourierKernel, Mathf.CeilToInt(signal.Length / 1024f), 1, 1);
		frequenciesBuffer.GetData(frequencies);
		signalBuffer.Release();
		frequenciesBuffer.Release();

		// float[] frequencies = new float[512];
		// audioSource.GetSpectrumData(frequencies, 0, FFTWindow.Rectangular);

		return frequencies;
	}

	// void OnAudioFilterRead(float[] data, int channels) {

	// 	for(int i = 0; i < data.Length; i += channels) {

	// 		timeIndex++;

	// 		if (timeIndex >= signal.Length) {
	// 			// timeIndex = 0;
	// 			return;
	// 		}

	// 		float wave = signal[timeIndex] + signal2[timeIndex];

	// 		for (int channel = 0; channel < channels; channel++) {
    //         	data[i + channel] = wave;
	// 		}  
	// 	}
	// }

}