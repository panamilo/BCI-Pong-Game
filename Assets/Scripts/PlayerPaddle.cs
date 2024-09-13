using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brainflow;
using brainflow.math;
using System.Linq;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;

    //BRAINTEST
    public GameObject gameobject1;
    public float thres1 = 1000f;
    public int frame_cnt = 0;

    private List<double> rightArmMeasurements = new List<double>();
    private List<double> leftArmMeasurements = new List<double>();

    private List<double> rightArmMedians = new List<double>();
    private List<double> leftArmMedians = new List<double>();

    private int calibrationCounter = 0;
    private double rightArmAverage = 0.0;
    private double leftArmAverage = 0.0;
    private bool isCalibrated = false;

    private BoardShim board_shim = null;
    private int sampling_rate = 0;

    void Start()
    {
        try
        {
            BoardShim.set_log_file("brainflow_log.txt");
            BoardShim.enable_dev_board_logger();

            BrainFlowInputParams input_params = new BrainFlowInputParams();
            input_params.serial_port = "COM3"; //CYTON
            int board_id = (int)BoardIds.CYTON_BOARD;
            board_shim = new BoardShim(board_id, input_params);
            board_shim.prepare_session();

            board_shim.config_board("x1060100Xx2060100X");
            board_shim.start_stream();
            sampling_rate = BoardShim.get_sampling_rate(board_id);
            Debug.Log("Brainflow streaming was started");
            System.Threading.Thread.Sleep(5000);
        }
        catch (BrainFlowError e)
        {
            Debug.Log(e);
        }
    }

    private void Update()
    {
        frame_cnt = frame_cnt + 1;

        if (board_shim == null)
        {
            return;
        }

        int board_id = (int)BoardIds.CYTON_BOARD;
        int nfft = DataFilter.get_nearest_power_of_two(sampling_rate);
        int number_of_data_points = sampling_rate * 1;
        int[] eeg_channels = BoardShim.get_eeg_channels(board_id);
        int left_arm = eeg_channels[0]; //first channel 
        int right_arm = eeg_channels[1]; //second channel

        double[,] data = board_shim.get_current_board_data(number_of_data_points);

        //LEFT ARM
        double[] detrend_left = DataFilter.detrend(data.GetRow(left_arm), (int)DetrendOperations.LINEAR);
        Tuple<double[], double[]> psd_left = DataFilter.get_psd_welch(detrend_left, nfft / 2, nfft / 4, sampling_rate, (int)WindowOperations.HANNING);
        double band_power_left = DataFilter.get_band_power(psd_left, 20.0, 30.0);
        if(!isCalibrated)
        {
         leftArmMeasurements.Add(band_power_left);
        }

        // RIGHT ARM
        double[] detrend_right = DataFilter.detrend(data.GetRow(right_arm), (int)DetrendOperations.LINEAR);
        Tuple<double[], double[]> psd_right = DataFilter.get_psd_welch(detrend_right, nfft / 2, nfft / 4, sampling_rate, (int)WindowOperations.HANNING);
        double band_power_right = DataFilter.get_band_power(psd_right, 20.0, 30.0);
        if(!isCalibrated)
        {
        rightArmMeasurements.Add(band_power_right);
        }

        if (band_power_left > leftArmAverage)
        {
            _direction = Vector2.right;
        }
        else if (band_power_right > rightArmAverage)
        {
            _direction = Vector2.left;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0)
        {
            _rigidbody.AddForce(_direction * this.speed);
        }
    }

    public void OnDestroy()
    {
        if (board_shim != null)
        {
            try
            {
                board_shim.release_session();
            }
            catch (BrainFlowError e)
            {
                Debug.Log(e);
            }
            Debug.Log("Brainflow streaming was stopped");
        }
    }

    public void CollectRightArmData()
    {
        calibrationCounter++;
        if (rightArmMeasurements.Count > 0)
        {
            // Calculate the median of the current measurements
            double rightArmMedian = CalculateMedian(rightArmMeasurements);
            rightArmMedians.Add(rightArmMedian);
            rightArmMeasurements.Clear(); // Clear measurements for the next session
        }

        if (calibrationCounter == 3)
        {
            // Calculate the average of the three medians
            rightArmAverage = rightArmMedians.Average();
            Debug.Log("Right Arm Calibration Average: " + rightArmAverage);

            // Reset for the next calibration
            rightArmMedians.Clear();
            calibrationCounter = 0;
        }
    }

    public void CollectLeftArmData()
    {
        calibrationCounter++;
        if (leftArmMeasurements.Count > 0)
        {
            // Calculate the median of the current measurements
            double leftArmMedian = CalculateMedian(leftArmMeasurements);
            leftArmMedians.Add(leftArmMedian);
            leftArmMeasurements.Clear(); // Clear measurements for the next session
        }

        if (calibrationCounter == 3)
        {
            // Calculate the average of the three medians
            leftArmAverage = leftArmMedians.Average();
            Debug.Log("Left Arm Calibration Average: " + leftArmAverage);

            // Reset for the next calibration
            leftArmMedians.Clear();
            calibrationCounter = 0;
            isCalibrated = true;
        }
    }

    private double CalculateMedian(List<double> values)
    {
        if (values == null || values.Count == 0)
            return 0.0;

        var sortedValues = values.OrderBy(x => x).ToList();
        int count = sortedValues.Count;

        if (count % 2 == 0)
        {
            // Average of the two middle elements for even count
            return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2.0;
        }
        else
        {
            // Middle element for odd count
            return sortedValues[count / 2];
        }
    }
}
