//------------------------------------------------------------------------------
// <copyright file="GestureResultView.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.DiscreteGestureBasics
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Stores discrete gesture results for the GestureDetector.
    /// Properties are stored/updated for display in the UI.
    /// </summary>
    public sealed class GestureResultView : INotifyPropertyChanged
    {
        /// <summary> Image to show when the 'detected' property is true for a tracked body </summary>
        private readonly ImageSource seatedImage = new BitmapImage(new Uri(@"Images\Seated.png", UriKind.Relative));

        /// <summary> Image to show when the 'detected' property is false for a tracked body </summary>
        private readonly ImageSource notSeatedImage = new BitmapImage(new Uri(@"Images\NotSeated.png", UriKind.Relative));

        /// <summary> Image to show no gesture detected property is false for a tracked body </summary>
        private readonly ImageSource noGesture = new BitmapImage(new Uri(@"Images\no_gesture.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        //private readonly ImageSource HandShake = new BitmapImage(new Uri(@"Images\hand_shake.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        //private readonly ImageSource HandWave = new BitmapImage(new Uri(@"Images\hand_wave.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource HandShake_right = new BitmapImage(new Uri(@"Images\hand_shake_right.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource HandShake_left = new BitmapImage(new Uri(@"Images\hand_shake_left.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource HandWave_right = new BitmapImage(new Uri(@"Images\hand_wave_right.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource HandWave_left = new BitmapImage(new Uri(@"Images\hand_wave_left.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource box_img = new BitmapImage(new Uri(@"Images\box.png", UriKind.Relative));

        /// <summary> Image to show when the hand_shake property is detected for a tracked body </summary>
        private readonly ImageSource cool_img = new BitmapImage(new Uri(@"Images\cool.png", UriKind.Relative));







        /// <summary> Image to show when the body associated with the GestureResultView object is not being tracked </summary>
        private readonly ImageSource notTrackedImage = new BitmapImage(new Uri(@"Images\NotTracked.png", UriKind.Relative));

        /// <summary> Array of brush colors to use for a tracked body; array position corresponds to the body colors used in the KinectBodyView class </summary>
        private readonly Brush[] trackedColors = new Brush[] { Brushes.Red, Brushes.Orange, Brushes.Green, Brushes.Blue, Brushes.Indigo, Brushes.Violet };

        /// <summary> Brush color to use as background in the UI </summary>
        private Brush bodyColor = Brushes.Gray;

        /// <summary> The body index (0-5) associated with the current gesture detector </summary>
        private int bodyIndex = 0;

        /// <summary> Current confidence value reported by the discrete gesture </summary>
        private float confidence = 0.0f;

        
        /// <summary> Image to display in UI which corresponds to tracking/detection state </summary>
        private ImageSource imageSource = null;
        
        /// <summary> True, if the body is currently being tracked </summary>
        private bool isTracked = false;

        /// <summary> True, if the user is attempting to turn left (either 'Steer_Left' or 'MaxTurn_Left' is detected) </summary>
        //private bool hand_shake_detected = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        //private bool hand_wave_detected = false;
          
        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool Hand_wave_right_detected2 = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool Hand_wave_left_detected2 = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool Hand_shake_right_detected2 = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool Hand_shake_left_detected2 = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool box_detected2 = false;

        /// <summary> True, if the user is attempting to turn right (either 'Steer_Right' or 'MaxTurn_Right' is detected) </summary>
        private bool cool_detected2 = false;




        /// <summary>
        /// Initializes a new instance of the GestureResultView class and sets initial property values
        /// </summary>
        /// <param name="bodyIndex">Body Index associated with the current gesture detector</param>
        /// <param name="isTracked">True, if the body is currently tracked</param>
        /// <param name="hand_shake_right_detected2">True, if the 'hand_shake' gesture is currently detected</param>
        /// <param name="hand_shake_left_detected2">True, if the 'hand_shake' gesture is currently detected</param>
        /// <param name="box_detected2">True, if the 'hand_shake' gesture is currently detected</param>
        /// <param name="cool_detected2">True, if the 'wave_hand' gesture is currently detected</param>
        /// <param name="hand_wave_left_detected2">True, if the 'wave_hand' gesture is currently detected</param>
        /// <param name="hand_wave_right_detected2">True, if the 'wave_hand' gesture is currently detected</param>
        /// <param name="confidence">Confidence value for detection of the 'Seated' gesture</param>
        public GestureResultView(int bodyIndex, bool isTracked,bool hand_wave_left_detected,bool hand_wave_right_detected,bool hand_shake_left_detected,bool hand_shake_right_detected,bool box_detected,bool cool_detected, float confidence)
        {
            this.BodyIndex = bodyIndex;
            this.IsTracked = isTracked;
            this.Hand_wave_right_detected2 = hand_wave_right_detected;
            this.Hand_wave_left_detected2 = hand_wave_left_detected;
            this.Hand_shake_right_detected2 = hand_shake_right_detected;
            this.Hand_shake_left_detected2 = hand_shake_left_detected;
            this.box_detected2 = box_detected;
            this.cool_detected2 = cool_detected;
            this.Confidence = confidence;
            this.ImageSource = this.notTrackedImage;


        }

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> 
        /// Gets the body index associated with the current gesture detector result 
        /// </summary>
        public int BodyIndex
        {
            get
            {
                return this.bodyIndex;
            }

            private set
            {
                if (this.bodyIndex != value)
                {
                    this.bodyIndex = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets the body color corresponding to the body index for the result
        /// </summary>
        public Brush BodyColor
        {
            get
            {
                return this.bodyColor;
            }

            private set
            {
                if (this.bodyColor != value)
                {
                    this.bodyColor = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets a value indicating whether or not the body associated with the gesture detector is currently being tracked 
        /// </summary>
        public bool IsTracked 
        {
            get
            {
                return this.isTracked;
            }

            private set
            {
                if (this.IsTracked != value)
                {
                    this.isTracked = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets a value indicating whether or not the discrete gesture has been detected
        /// </summary>
        /// 
        /// <summary> 
        /// Gets a value indicating whether or not the discrete gesture has been detected
        /// </summary>

        //public bool Hand_Shake_detected 
        //{
          //  get
            //  return this.hand_shake_detected;
            //}

            //private set
            //{
              //  if (this.hand_shake_detected != value)
                //{
                  //  this.hand_shake_detected = value;
                    //this.NotifyPropertyChanged();
               // }
            //}
        //}
        /// <summary> 
        /// Gets a value indicating whether or not the discrete gesture has been detected
        /// </summary>
        //public bool Hand_Wave_detected
        //{
          //  get
            //{
              //  return this.hand_wave_detected;
            //}

            //private set
            //{
              //  if (this.hand_wave_detected != value)
                //{
                  //  this.hand_wave_detected = value;
                    //this.NotifyPropertyChanged();
                //}
            //}
        //}

        public bool Hand_wave_right_detected
        {
            get
            {
                return this.Hand_wave_right_detected2;
            }

            private set
            {
                if (this.Hand_wave_right_detected2 != value)
                {
                    this.Hand_wave_right_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool Hand_wave_left_detected
        {
            get
            {
                return this.Hand_wave_left_detected2;
            }

            private set
            {
                if (this.Hand_wave_left_detected2 != value)
                {
                    this.Hand_wave_left_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool Hand_shake_right_detected
        {
            get
            {
                return this.Hand_shake_right_detected2;
            }

            private set
            {
                if (this.Hand_shake_right_detected2 != value)
                {
                    this.Hand_shake_right_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public bool Hand_shake_left_detected
        {
            get
            {
                return this.Hand_shake_left_detected2;
            }

            private set
            {
                if (this.Hand_shake_left_detected2 != value)
                {
                    this.Hand_shake_left_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public bool cool_detected
        {
            get
            {
                return this.cool_detected2;
            }

            private set
            {
                if (this.cool_detected2 != value)
                {
                    this.cool_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool box_detected
        {
            get
            {
                return this.box_detected2;
            }

            private set
            {
                if (this.box_detected2 != value)
                {
                    this.box_detected2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        /// <summary> 
        /// Gets a float value which indicates the detector's confidence that the gesture is occurring for the associated body 
        /// </summary>
        public float Confidence
        {
            get
            {
                return this.confidence;
            }

            private set
            {
                if (this.confidence != value)
                {
                    this.confidence = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets an image for display in the UI which represents the current gesture result for the associated body 
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }

            private set
            {
                if (this.ImageSource != value)
                {
                    this.imageSource = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Updates the values associated with the discrete gesture detection result
        /// </summary>
        /// <param name="isBodyTrackingIdValid">True, if the body associated with the GestureResultView object is still being tracked</param>
        /// <param name="isGesture_HAND_SHAKE_Detected">True, if the discrete gesture is currently detected for the associated body</param>
        /// <param name="isGesture_HAND_WAVE_Detected">True, if the discrete gesture is currently detected for the associated body</param>
        /// <param name="detectionConfidence">Confidence value for detection of the discrete gesture</param>
        //public void UpdateGestureResult(bool isBodyTrackingIdValid, bool isGesture_HAND_SHAKE_Detected,bool isGesture_HAND_WAVE_Detected, float detectionConfidence)
        public void UpdateGestureResult(bool isBodyTrackingIdValid, bool isGesture_BOX_Detected, bool isGesture_COOL_Detected, bool isGesture_HAND_SHAKE_LEFT_Detected, bool isGesture_HAND_SHAKE_RIGHT_Detected, bool isGesture_HAND_WAVE_LEFT_Detected, bool isGesture_HAND_WAVE_RIGHT_Detected, float detectionConfidence)
        {
            this.IsTracked = isBodyTrackingIdValid;
            this.Confidence = 0.0f;

            if (!this.IsTracked)
            {
                this.ImageSource = this.notTrackedImage;
                this.cool_detected = false;
                this.box_detected = false;
                this.Hand_shake_left_detected = false;
                this.Hand_shake_right_detected = false;
                this.Hand_wave_left_detected = false;
                this.Hand_wave_right_detected = false;

                //this.Hand_Shake_detected = false;
                //this.Hand_Wave_detected = false;
                this.BodyColor = Brushes.Gray;
            }
            else
            {

                this.cool_detected = isGesture_COOL_Detected;
                this.box_detected = isGesture_BOX_Detected;
                this.Hand_shake_left_detected = isGesture_HAND_SHAKE_LEFT_Detected;
                this.Hand_shake_right_detected = isGesture_HAND_SHAKE_RIGHT_Detected;
                this.Hand_wave_left_detected = isGesture_HAND_WAVE_LEFT_Detected;
                this.Hand_wave_right_detected = isGesture_HAND_WAVE_RIGHT_Detected;
                //this.Hand_Shake_detected = isGesture_HAND_SHAKE_Detected;
                //this.Hand_Wave_detected = isGesture_HAND_WAVE_Detected;

                this.BodyColor = this.trackedColors[this.BodyIndex];

                //if (this.Hand_Shake_detected)
                //{
                    ///this.Confidence = detectionConfidence;
                  //  this.Confidence = 2;
                    //this.ImageSource = this.HandShake;
                //}

                //else if (this.Hand_Wave_detected)
                //{
                    ///this.Confidence = detectionConfidence;
                  //  this.Confidence = 3;
                    //this.ImageSource = this.HandWave;
                //}

                if (this.Hand_shake_left_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.HandShake_left;
                }

                else if (this.Hand_shake_right_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.HandShake_right;
                }


                else if (this.Hand_wave_left_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.HandWave_left;
                }


                else if (this.Hand_wave_right_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.HandWave_right;
                }

                else if (this.box_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.box_img;
                }


                else if (this.cool_detected)
                {
                    this.Confidence = detectionConfidence;
                    this.ImageSource = this.cool_img;
                }

                else
                {
                    this.ImageSource = this.noGesture;
                }
            }
        }

        /// <summary>
        /// Notifies UI that a property has changed
        /// </summary>
        /// <param name="propertyName">Name of property that has changed</param> 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
