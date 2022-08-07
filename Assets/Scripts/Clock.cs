using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;

	DateTime time = DateTime.Now;

	void Update()
	{
		TimeSpan time = DateTime.Now.TimeOfDay;
		hoursPivot.localRotation =
			Quaternion.Euler(0f, 0f, hoursToDegrees * (float)time.TotalHours);
		minutesPivot.localRotation =
			Quaternion.Euler(0f, 0f, minutesToDegrees * (float)time.TotalMinutes);
		secondsPivot.localRotation =
			Quaternion.Euler(0f, 0f, secondsToDegrees * (float)time.TotalSeconds);
	}
}