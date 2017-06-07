//------------------------------------------------------------------------------
// <copyright file="GestureDetector.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.DiscreteGestureBasics
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Kinect;
    using Microsoft.Kinect.VisualGestureBuilder;

    /// <summary>
    /// Gesture Detector class which listens for VisualGestureBuilderFrame events from the service
    /// and updates the associated GestureResultView object with the latest results for the 'Seated' gesture
    /// </summary>
    public class GestureDetector : IDisposable
    {
        /// <summary> Path to the gesture database that was trained with VGB </summary>
        //private readonly string gestureDatabase = @"Database\vizzy.gbd";
        private readonly string gestureDatabase = @"Database\vizzy_final.gbd";

        /// <summary> Name of the discrete gesture in the database that we want to track </summary>
        

        ///private readonly string hand_shakeGestureName = "hand_shake";

        ///private readonly string wave_handGestureName = "wave_hand";

        private readonly string box_GestureName = "box";

        private readonly string cool_GestureName = "cool";

        private readonly string handshake_left_GestureName = "handshake_Left";

        private readonly string handshake_right_GestureName = "handshake_Right";

        private readonly string handwave_left_GestureName = "handwave_Left";

        private readonly string handwave_right_GestureName = "handwave_Right";




        /// <summary> Gesture frame source which should be tied to a body tracking ID </summary>
        private VisualGestureBuilderFrameSource vgbFrameSource = null;

        /// <summary> Gesture frame reader which will handle gesture events coming from the sensor </summary>
        private VisualGestureBuilderFrameReader vgbFrameReader = null;

        ///public bool handshakedone;

        ///public bool handwave;

        public bool handwave_right;

        public bool handwave_left;

        public bool handshake_right;

        public bool handshake_left;

        public bool box;

        public bool cool;

        public float confi;






        /// <summary>
        /// Initializes a new instance of the GestureDetector class along with the gesture frame source and reader
        /// </summary>
        /// <param name="kinectSensor">Active sensor to initialize the VisualGestureBuilderFrameSource object with</param>
        /// <param name="gestureResultView">GestureResultView object to store gesture results of a single body to</param>
        public GestureDetector(KinectSensor kinectSensor, GestureResultView gestureResultView)
        {
            if (kinectSensor == null)
            {
                throw new ArgumentNullException("kinectSensor");
            }

            if (gestureResultView == null)
            {
                throw new ArgumentNullException("gestureResultView");
            }
            
            this.GestureResultView = gestureResultView;
            
            // create the vgb source. The associated body tracking ID will be set when a valid body frame arrives from the sensor.
            this.vgbFrameSource = new VisualGestureBuilderFrameSource(kinectSensor, 0);
            this.vgbFrameSource.TrackingIdLost += this.Source_TrackingIdLost;

            
            

            // open the reader for the vgb frames
            this.vgbFrameReader = this.vgbFrameSource.OpenReader();
            if (this.vgbFrameReader != null)
            {
                this.vgbFrameReader.IsPaused = true;
                this.vgbFrameReader.FrameArrived += this.Reader_GestureFrameArrived;
            }

            // load the 'Seated' gesture from the gesture database
            // load all gestures from the gesture database
            using (var database = new VisualGestureBuilderDatabase(this.gestureDatabase))
            {
                this.vgbFrameSource.AddGestures(database.AvailableGestures);
            }

            
            //using (VisualGestureBuilderDatabase database = new VisualGestureBuilderDatabase(this.gestureDatabase))
            //{
                // we could load all available gestures in the database with a call to vgbFrameSource.AddGestures(database.AvailableGestures), 
                // but for this program, we only want to track one discrete gesture from the database, so we'll load it by name
                //foreach (Gesture gesture in database.AvailableGestures)
                //{
                    //if (gesture.Name.Equals(this.hand_shakeGestureName) || gesture.Name.Equals(this.wave_handGestureName))
                    //{
                        //this.vgbFrameSource.AddGesture(gesture);
                    //}
                //}
            //}
        }

        /// <summary> Gets the GestureResultView object which stores the detector results for display in the UI </summary>
        public GestureResultView GestureResultView { get; private set; }

        /// <summary>
        /// Gets or sets the body tracking ID associated with the current detector
        /// The tracking ID can change whenever a body comes in/out of scope
        /// </summary>
        public ulong TrackingId
        {
            get
            {
                return this.vgbFrameSource.TrackingId;
            }

            set
            {
                if (this.vgbFrameSource.TrackingId != value)
                {
                    this.vgbFrameSource.TrackingId = value;
                }
            }
        }




        public bool gesture_done_cool
        {
            get
            {
                return this.cool;

            }
            set
            {
                if (this.cool != value)
                {
                    this.cool = value;
                }
            }

        }

        public bool gesture_done_box
        {
            get
            {
                return this.box;

            }
            set
            {
                if (this.box != value)
                {
                    this.box = value;
                }
            }

        }


        public bool gesture_done_handshake_right
        {
            get
            {
                return this.handshake_right;

            }
            set
            {
                if (this.handshake_right != value)
                {
                    this.handshake_right = value;
                }
            }

        }

        public bool gesture_done_handshake_left
        {
            get
            {
                return this.handshake_left;

            }
            set
            {
                if (this.handshake_left != value)
                {
                    this.handshake_left = value;
                }
            }

        }

        public bool gesture_done_handwave_left
        {
            get
            {
                return this.handwave_left;

            }
            set
            {
                if (this.handwave_left != value)
                {
                    this.handwave_left = value;
                }
            }

        }

        public bool gesture_done_handwave_right
        {
            get
            {
                return this.handwave_right;

            }
            set
            {
                if (this.handwave_right != value)
                {
                    this.handwave_right = value;
                }
            }

        }

        ///public bool gesture_done_handwave
        //{
            //get
            //{
              //  return this.handwave;

            //}
            //set
            //{
              //  if (this.handwave != value)
                //{
                  //  this.handwave = value;
                //}
            //}

       //}
       
        

        ///public bool gesture_done_handshake
        //{
            //get
            //{
             //   return this.handshakedone;
            //}
            //set
            //{
                //if (this.handshakedone != value)
               // {
                //    this.handshakedone = value;
              //  }
            //}
        ///}



        /// <summary>
        /// Gets or sets a value indicating whether or not the detector is currently paused
        /// If the body tracking ID associated with the detector is not valid, then the detector should be paused
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return this.vgbFrameReader.IsPaused;
            }

            set
            {
                if (this.vgbFrameReader.IsPaused != value)
                {
                    this.vgbFrameReader.IsPaused = value;
                }
            }
        }

        /// <summary>
        /// Disposes all unmanaged resources for the class
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the VisualGestureBuilderFrameSource and VisualGestureBuilderFrameReader objects
        /// </summary>
        /// <param name="disposing">True if Dispose was called directly, false if the GC handles the disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.vgbFrameReader != null)
                {
                    this.vgbFrameReader.FrameArrived -= this.Reader_GestureFrameArrived;
                    this.vgbFrameReader.Dispose();
                    this.vgbFrameReader = null;
                }

                if (this.vgbFrameSource != null)
                {
                    this.vgbFrameSource.TrackingIdLost -= this.Source_TrackingIdLost;
                    this.vgbFrameSource.Dispose();
                    this.vgbFrameSource = null;
                }
            }
        }

        /// <summary>
        /// Handles gesture detection results arriving from the sensor for the associated body tracking Id
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_GestureFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            VisualGestureBuilderFrameReference frameReference = e.FrameReference;
            using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())

            {
                if (frame != null)
                {
                    // get the discrete gesture results which arrived with the latest frame
                    IReadOnlyDictionary<Gesture, DiscreteGestureResult> discreteResults = frame.DiscreteGestureResults;

                    if (discreteResults != null)
                    {
                        


                        //bool hand_shake = this.GestureResultView.Hand_Shake_detected;
                        //bool hand_wave = this.GestureResultView.Hand_Wave_detected;

                        bool handwave_right1 = this.GestureResultView.Hand_wave_right_detected;
                        bool handwave_left1 = this.GestureResultView.Hand_wave_left_detected;
                        bool handshake_right1 = this.GestureResultView.Hand_shake_right_detected;
                        bool handshake_left1 = this.GestureResultView.Hand_shake_left_detected;
                        bool box1 = this.GestureResultView.box_detected;
                        bool cool1 = this.GestureResultView.cool_detected;


                        // we only have one gesture in this source object, but you can get multiple gestures
                        foreach (var gesture in this.vgbFrameSource.Gestures)
                        {
                            if (gesture.GestureType == GestureType.Discrete)
                            {
                                DiscreteGestureResult result = null;
                                discreteResults.TryGetValue(gesture, out result);
                                
                                if (result != null)
                                {
                                                                                                            
                                    if (gesture.Name.Equals(this.box_GestureName))
                                    {
                                        box1 = result.Detected;
                                        box = result.Detected;
                                    
                                    }
                                    if (gesture.Name.Equals(this.cool_GestureName))
                                    {
                                        cool = result.Detected;
                                        cool1 = result.Detected;
                                    }

                                    if (gesture.Name.Equals(this.handshake_left_GestureName))
                                    {
                                        handshake_left = result.Detected;
                                        handshake_left1= result.Detected;
                                        
                                    }
                                    if (gesture.Name.Equals(this.handshake_right_GestureName))
                                    {
                                        handshake_right = result.Detected;
                                        handshake_right1 = result.Detected;
                                        
                                    }


                                    if (gesture.Name.Equals(this.handwave_left_GestureName))
                                    {
                                        handwave_left = result.Detected;
                                        handwave_left1 = result.Detected;
                                        
                                    }


                                    if (gesture.Name.Equals(this.handwave_right_GestureName))
                                    {
                                        handwave_right = result.Detected;
                                        handwave_right1 = result.Detected;
                                        
                                    }


                                    if (result.Detected == true)
                                    {
                                        confi = result.Confidence;
                                    }

                                    //if (gesture.Name.Equals(this.wave_handGestureName))
                                    //{
                                        //hand_wave = result.Detected;
                                        //handwave = result.Detected;

                                        //System.Diagnostics.Debug.Write("I handwave to");
                                        //System.Diagnostics.Debug.Write(result.Detected);
                                        /// HERE SHOULD BE THE POSE OF PERSON'S HEAD!!!
                                        ///hand_wave_conf = result.Confidence;
                                    //}
                                    //if (gesture.Name.Equals(this.hand_shakeGestureName))
                                    //{
                                        //this.handshakedone = result.Detected;
                                        //hand_shake = result.Detected;
                                        //System.Diagnostics.Debug.Write("I enter in hand shake and changed to");
                                        //System.Diagnostics.Debug.Write(result.Detected);
                                        ///hand_shake_conf = result.Confidence;
                                    //}
                                }
                            }
                        }
                        this.GestureResultView.UpdateGestureResult(true,box,cool,handshake_left,handshake_right,handwave_left,handwave_right, confi);
                    }
                }
            }
        }
        /// <summary>
        /// Handles the TrackingIdLost event for the VisualGestureBuilderSource object
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Source_TrackingIdLost(object sender, TrackingIdLostEventArgs e)
        {
            // update the GestureResultView object to show the 'Not Tracked' image in the UI
            this.GestureResultView.UpdateGestureResult(false, false, false, false,false, false, false, 0.0f);
        }
    }
}
