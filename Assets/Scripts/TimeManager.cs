using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float slowDownFactor = 0.05f;

    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void updateFixedDeltaTime()
    {
        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 frames per real-time second
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void StartSlowMotion ()
    {
        Time.timeScale = slowDownFactor;
        updateFixedDeltaTime();
    }

    public void StopSlowMotion ()
    {
        Time.timeScale = 1;
        updateFixedDeltaTime();
    }

}
