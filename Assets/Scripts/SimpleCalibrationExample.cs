using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NextMind.Calibration;
using NextMind;
using NextMind.Devices;

public class SimpleCalibrationExample : MonoBehaviour
{
    [SerializeField] private CalibrationManager calibrationManager;

    [SerializeField] private Text resultsText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCalibrationWhenReady());
    }

    private IEnumerator StartCalibrationWhenReady()
    {
        // Waiting for the NeuroManager to be ready
        yield return new WaitUntil(NeuroManager.Instance.IsReady);

        // Actually start the calibration process.
        calibrationManager.StartCalibration();

        // Listen to the incoming results
        calibrationManager.onCalibrationResultsAvailable.AddListener(OnReceivedResults);
    }

    private void OnReceivedResults(Device device, CalibrationResults.CalibrationGrade grade)
    {
        resultsText.text = $"Received results for {device.Name} with a grade of {grade}";
    }
}