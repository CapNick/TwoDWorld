using System;
using System.Collections.Generic;
using System.Text;


class PerlinNoise
{ 
	static Random random = new Random();


	public static float[][] GenerateWhiteNoise(int width, int height)
	{            
		float[][] noise = GetEmptyArray<float>(width, height);

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				noise[i][j] = (float)random.NextDouble() % 1;
			}
		}

		return noise;
	}

	public static float Interpolate(float x0, float x1, float alpha)// Now Cosine for testing
	{
		float mu2 = (1 - (float)Math.Cos (alpha * (float)Math.PI)) / 2;
		return (x0 * (1 - mu2) + x1 * mu2);
	}
		

	public static T[][] GetEmptyArray<T>(int width, int height)
	{
		T[][] image = new T[width][];

		for (int i = 0; i < width; i++)
		{
			image[i] = new T[height];
		}

		return image;
	}

	public static float[][] GenerateSmoothNoise(float[][] baseNoise, int octave, float frequency)
	{
		int width = baseNoise.Length;
		int height = baseNoise[0].Length;

		float[][] smoothNoise = GetEmptyArray<float>(width, height);

		int samplePeriod = 1 << octave; // calculates 2 ^ k
		frequency /= samplePeriod;

		for (int i = 0; i < width; i++)
		{
			//calculate the horizontal sampling indices
			int sample_i0 = (i / samplePeriod) * samplePeriod;
			int sample_i1 = (sample_i0 + samplePeriod) % width; //wrap around
			float horizontal_blend = (i - sample_i0) * frequency;

			for (int j = 0; j < height; j++)
			{
				//calculate the vertical sampling indices
				int sample_j0 = (j / samplePeriod) * samplePeriod;
				int sample_j1 = (sample_j0 + samplePeriod) % height; //wrap around
				float vertical_blend = (j - sample_j0) * frequency;

				//blend the top two corners
				float top = Interpolate(baseNoise[sample_i0][sample_j0],
					baseNoise[sample_i1][sample_j0], horizontal_blend);

				//blend the bottom two corners
				float bottom = Interpolate(baseNoise[sample_i0][sample_j1],
					baseNoise[sample_i1][sample_j1], horizontal_blend);

				//final blend
				smoothNoise[i][j] = Interpolate(top, bottom, vertical_blend);                    
			}
		}

		return smoothNoise;
	}

	public static float[][] GeneratePerlinNoise(float[][] baseNoise, int octaveCount, float frequency, float persistance)
	{
		int width = baseNoise.Length;
		int height = baseNoise[0].Length;

		float[][][] smoothNoise = new float[octaveCount][][]; //an array of 2D arrays containing



		//generate smooth noise
		for (int i = 0; i < octaveCount; i++)
		{
			smoothNoise[i] = GenerateSmoothNoise(baseNoise, i, frequency);
		}

		float[][] perlinNoise = GetEmptyArray<float>(width, height); //an array of floats initialised to 0

		float amplitude = 1.0f;
		float totalAmplitude = 0.0f;

		//blend noise together
		for (int octave = octaveCount - 1; octave >= 0; octave--)
		{
			amplitude *= persistance;
			totalAmplitude += amplitude;

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;
				}
			}
		}

		//normalisation
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				perlinNoise[i][j] /= totalAmplitude;
			}
		}        

		return perlinNoise;
	}

	public static float[][] GeneratePerlinNoise(int width, int height, int octaveCount, float frequency, float persistance)
	{
		float[][] baseNoise = GenerateWhiteNoise(width, height);

		return GeneratePerlinNoise(baseNoise, octaveCount, frequency, persistance);
	}
}
