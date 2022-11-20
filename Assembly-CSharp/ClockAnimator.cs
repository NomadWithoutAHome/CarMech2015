using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class ClockAnimator : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
	private void Update()
	{
		DateTime now = DateTime.Now;
		TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
		this.hours.localRotation = Quaternion.Euler((float)timeOfDay.TotalHours * 30f, 0f, 0f);
		this.minutes.localRotation = Quaternion.Euler((float)now.Minute * 6f, 0f, 0f);
		this.seconds.localRotation = Quaternion.Euler((float)now.Second * 6f, 0f, 0f);
	}

	// Token: 0x04000001 RID: 1
	private const float hoursToDegrees = 30f;

	// Token: 0x04000002 RID: 2
	private const float minutesToDegrees = 6f;

	// Token: 0x04000003 RID: 3
	private const float secondsToDegrees = 6f;

	// Token: 0x04000004 RID: 4
	public Transform hours;

	// Token: 0x04000005 RID: 5
	public Transform minutes;

	// Token: 0x04000006 RID: 6
	public Transform seconds;
}
