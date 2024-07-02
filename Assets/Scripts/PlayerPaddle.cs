using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using brainflow;
using brainflow.math;

public class PlayerPaddle : Paddle
{
   private Vector2 _direction;

    //BRAINTEST
     //public float speed = 0.01f; 
    public GameObject gameobject1; 
    public float thres1 = 1000f; 
    public int frame_cnt = 0 ; 
    public float thres2 = 2000f; 
    public double band_power_right_old = 100.0 ; 
        


    private BoardShim board_shim = null;
    private int sampling_rate = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60; 
        try
        {
            BoardShim.set_log_file("brainflow_log.txt");
            BoardShim.enable_dev_board_logger();

            BrainFlowInputParams input_params = new BrainFlowInputParams();
            //input_params.serial_port="/dev/cu.usbmodem11" ; //GANGLION
            input_params.serial_port="COM3" ; //CYTON
            // int board_id = (int)BoardIds.SYNTHETIC_BOARD;
            //int board_id = (int)BoardIds.GANGLION_BOARD;
            int board_id = (int)BoardIds.CYTON_BOARD;
            board_shim = new BoardShim(board_id, input_params);
            board_shim.prepare_session();

            board_shim.config_board("x1060100Xx2060100X");

            //board_shim.start_stream(450000, "file://brainflow_data.csv:w");
            board_shim.start_stream();
            sampling_rate = BoardShim.get_sampling_rate(board_id);
            Debug.Log("Brainflow streaming was started");
        }
        catch (BrainFlowError e)
        {
            Debug.Log(e);
        }
    }

    //BRAINTEST END
   private void Update() 
   {
        //BRAINTEST

         frame_cnt = frame_cnt+1 ; 
        
        if (board_shim == null)
        {
            return;
        }

        // int board_id = (int)BoardIds.SYNTHETIC_BOARD;
        //int board_id = (int)BoardIds.GANGLION_BOARD;
        int board_id = (int)BoardIds.CYTON_BOARD;
        int nfft = DataFilter.get_nearest_power_of_two(sampling_rate);
        int number_of_data_points = sampling_rate*1;
        int[] eeg_channels = BoardShim.get_eeg_channels (board_id);
        int left_arm = eeg_channels[0]; //first channel 
        int right_arm = eeg_channels[1]; //second channel
            
        double[,] data = board_shim.get_current_board_data(number_of_data_points);

        //foreach (var index in eeg_channels)
        //        Debug.Log (data.GetRow (index));

        //LEFT ARM
        // check https://brainflow.readthedocs.io/en/stable/index.html for api ref and more code samples
        double[] detrend_left = DataFilter.detrend(data.GetRow(left_arm), (int)DetrendOperations.LINEAR);
        Tuple<double[], double[]> psd_left = DataFilter.get_psd_welch(detrend_left, nfft/2, nfft/4, sampling_rate, (int)WindowOperations.HANNING);
        double band_power_left = DataFilter.get_band_power(psd_left, 20.0, 30.0);
        Debug.Log("Left Arm: " + (band_power_left));
    

        // RIGHT ARM
        // check https://brainflow.readthedocs.io/en/stable/index.html for api ref and more code samples
        double[] detrend_right = DataFilter.detrend(data.GetRow(right_arm), (int)DetrendOperations.LINEAR);
        Tuple<double[], double[]> psd_right = DataFilter.get_psd_welch(detrend_right, nfft/2, nfft/4, sampling_rate, (int)WindowOperations.HANNING);
        double band_power_right = DataFilter.get_band_power(psd_right, 20.0, 30.0);
        Debug.Log("Right Arm: " + (band_power_right));


        //BRAINTEST END


        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
        if (band_power_left > thres1 ){    
            _direction = Vector2.right;
        } 
        //else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
        else if (band_power_right > thres1){ 
            _direction = Vector2.left;
        } else {
             _direction = Vector2.zero;
        }
   }
    private void FixedUpdate() 
    {
        if (_direction.sqrMagnitude != 0 ){
            _rigidbody.AddForce(_direction * this.speed);
        }    
    }
}

 