using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;


public class XR_PlayerInput : MonoBehaviour
{
    public bool debug;

    private List<InputDevice> hmdDevices;
    private List<InputDevice> leftHandDevices;
    private List<InputDevice> rightHandDevices;

    private InputDevice hmdDevice;
    protected InputDevice leftHandDevice;
    protected InputDevice rightHandDevice;

    public static Vector2 rightHandAxisDirection;
    public static Vector2 leftHandAxisDirection;

    private bool rightHandTriggerValue = false;
    private bool rightHandGripValue = false;
    private bool rightHandPrimaryButtonValue = false;
    private bool rightHandSecondaryButtonValue = false;

    private bool leftHandTriggerValue;
    private bool leftHandGripValue;
    private bool leftHandPrimaryButtonValue;
    private bool leftHandSecondaryButtonValue;
    private bool menuButtonValue;

    private bool isUserPresent;

    // Right Hand Events
    public UnityEvent RightHandTriggerDown;
    public UnityEvent RightHandTriggerUp;
    public UnityEvent RightHandGripDown;
    public UnityEvent RightHandGripUp;
    public UnityEvent RightHandPrimaryButtonTouched;
    public UnityEvent RightHandPrimaryButtonDown;
    public UnityEvent RightHandPrimaryButtonUp;
    public UnityEvent RightHandSecondaryButtonTouched;
    public UnityEvent RightHandSecondaryButtonDown;
    public UnityEvent RightHandSecondaryButtonUp;
    public UnityEvent RightHandAxisTouched;
    public UnityEvent RightHandAxisDown;
    
    // Left Hand Events
    public UnityEvent LeftHandTriggerDown;
    public UnityEvent LeftHandTriggerUp;
    public UnityEvent LeftHandGripDown;
    public UnityEvent LeftHandGripUp;
    public UnityEvent LeftHandPrimaryButtonTouched;
    public UnityEvent LeftHandPrimaryButtonDown;
    public UnityEvent LeftHandPrimaryButtonUp;
    public UnityEvent LeftHandSecondaryButtonTouched;
    public UnityEvent LeftHandSecondaryButtonDown;
    public UnityEvent LeftHandSecondaryButtonUp;
    public UnityEvent LeftHandAxisTouched;
    public UnityEvent LeftHandAxisDown;

    public UnityEvent MenuDown;
    public UnityEvent MenuUp;

    private bool isHMDFound = false;
    private bool isLeftHandFound = false;
    private bool isRightHandFound = false;

    float periodicalTimer = 0;

    private void Start()
    {
        //GetHMD();
        //GetLeftHand();
        //GetRightHand();
    }

    private void Awake()
    {
        if (!isHMDFound) GetHMD();
        if (!isLeftHandFound) GetLeftHand();
        if (!isRightHandFound) GetRightHand();
    }
    
    private void OnEnable()
    {
        if (!isHMDFound) GetHMD();
        if (!isLeftHandFound) GetLeftHand();
        if (!isRightHandFound) GetRightHand();
    }

    protected void Update()
    {
        CheckForHardware();

        // Check if the User is Present
        bool outValue;
        hmdDevice.TryGetFeatureValue(CommonUsages.isTracked, out outValue);
        UserPresenceStateChange(isUserPresent, outValue);


        // If user is Present and controller are found, then get the users input, or find the controllers
        if (isUserPresent)
        {
            if (isLeftHandFound && leftHandDevice.isValid) LeftHandUserInput();
            if (isRightHandFound && rightHandDevice.isValid) RightHandUserInput();
        }
    }

    // Check for HMD, Left, Right Controllers every so often
    private void CheckForHardware()
    {
        periodicalTimer += 1 * Time.deltaTime;
        if (periodicalTimer >= 3)
        {
            if (!isHMDFound) GetHMD();
            if (!isLeftHandFound) GetLeftHand();
            if (!isRightHandFound) GetRightHand();
            periodicalTimer = 0;
        }
    }

    // Changes the state of UserPresent only if the value differs
    private void UserPresenceStateChange(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }
        else if (oldValue != newValue)
        {
            isUserPresent = newValue;

            if (isUserPresent)
            {
                Debug.Log("User is Present");
                GetLeftHand();
                GetRightHand();
            }
            if (!isUserPresent)
            {
                Debug.Log("User is NOT Present");
            }
        }
    }

    private void GetHMD()
    {
        //Get HMD Devices
        hmdDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.Head, hmdDevices);

        if (hmdDevices.Count == 1)
        {
            isHMDFound = true;
            hmdDevice = hmdDevices[0];
            if (debug) Debug.Log(string.Format("Device name '{0}' with role '{1}'", hmdDevice.name, hmdDevice.role.ToString()));
        }
        else if (hmdDevices.Count > 1)
        {
            isHMDFound = true;
            hmdDevice = hmdDevices[0];
            if (debug) Debug.Log("Found more than one HMD!");
        }
        else if (hmdDevices.Count == 0)
        {
            isHMDFound = false;
            if (debug) Debug.Log("No HMD Devices Found");
        }
        else if (!hmdDevices[0].isValid)
        {
            isHMDFound = false;
            if (debug) Debug.Log("HMD is NOT Valid");
        }
    }

    private void GetLeftHand()
    {
        // Get Left Hand Devices
        leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);


        if (leftHandDevices.Count == 1)
        {
            isLeftHandFound = true;
            leftHandDevice = leftHandDevices[0];
            if (debug) Debug.Log(string.Format("Device name '{0}' with role '{1}'", leftHandDevice.name, leftHandDevice.role.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            isLeftHandFound = true;
            leftHandDevice = leftHandDevices[0];
            if (debug) Debug.Log("Found more than one left hand!");
        }
        else if (leftHandDevices.Count == 0)
        {
            isLeftHandFound = false;
            if (debug) Debug.Log("No Left Hand Devices Found");
        }
        else if (!leftHandDevices[0].isValid)
        {
            isLeftHandFound = false;
            if (debug) Debug.Log("Left Hand Device is NOT Valid");
        }
    }

    private void GetRightHand()
    {
        // Get Right Hand Devices
        rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            isRightHandFound = true;
            rightHandDevice = rightHandDevices[0];
            if (debug) Debug.Log(string.Format("Device name '{0}' with role '{1}'", rightHandDevice.name, rightHandDevice.role.ToString()));
        }
        else if (rightHandDevices.Count > 1)
        {
            isRightHandFound = true;
            rightHandDevice = rightHandDevices[0];
            if (debug) Debug.Log("Found more than one Right hand!");
        }
        else if (rightHandDevices.Count == 0)
        {
            isRightHandFound = false;
            if (debug) Debug.Log("No Right Hand Devices Found");
        }
        else if (!rightHandDevices[0].isValid)
        {
            isRightHandFound = false;
            if (debug) Debug.Log("Right Hand Device is NOT Valid");
        }
    }


    private void RightHandTrigger()
    {
        // Trigger Pressed  
        bool rightHandTriggerTemp;
        rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out rightHandTriggerTemp);
        if (rightHandTriggerTemp == rightHandTriggerValue) return;
        else if (rightHandTriggerTemp != rightHandTriggerValue) rightHandTriggerValue = rightHandTriggerTemp;
        if (rightHandTriggerValue) RightHandTriggerDown.Invoke();
        if (!rightHandTriggerValue) RightHandTriggerUp.Invoke();
    }


    private void RightHandGrip()
    {
        // Grip Pressed
        bool rightHandGripTemp;
        rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out rightHandGripTemp);
        if (rightHandGripTemp == rightHandGripValue) return;
        else if (rightHandGripTemp != rightHandGripValue) rightHandGripValue = rightHandGripTemp;
        if (rightHandGripValue) RightHandGripDown.Invoke();
        if (!rightHandGripValue) RightHandGripUp.Invoke();
    }

    private void RightHandPrimaryButton()
    {
        // Primary Button Pressed
        bool rightHandPrimaryButtonTemp;
        rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out rightHandPrimaryButtonTemp);
        if (rightHandPrimaryButtonTemp == rightHandPrimaryButtonValue) return;
        else if (rightHandPrimaryButtonTemp != rightHandPrimaryButtonValue) rightHandPrimaryButtonValue = rightHandPrimaryButtonTemp;
        if (rightHandPrimaryButtonValue) RightHandPrimaryButtonDown.Invoke();
        if (!rightHandPrimaryButtonValue) RightHandPrimaryButtonUp.Invoke();        
    }

    private void RightHandSecondaryButton()
    {
        // Secondary Button Pressed
        bool rightHandSecondaryButtonTemp;
        rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out rightHandSecondaryButtonTemp);
        if (rightHandSecondaryButtonTemp == rightHandSecondaryButtonValue) return;
        else if (rightHandSecondaryButtonTemp != rightHandSecondaryButtonValue) rightHandSecondaryButtonValue = rightHandSecondaryButtonTemp;
        if (rightHandSecondaryButtonValue) RightHandSecondaryButtonDown.Invoke();
        if (!rightHandSecondaryButtonValue) RightHandSecondaryButtonUp.Invoke();
    }

    private void RightHandUserInput()
    {

        RightHandTrigger();
        RightHandGrip();
        RightHandPrimaryButton();
        RightHandSecondaryButton();

        // PrimaryButton Touched
        bool rightHandPrimaryButtonTouchValue;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out rightHandPrimaryButtonTouchValue) && rightHandPrimaryButtonTouchValue) RightHandPrimaryButtonTouched.Invoke();

        // Secondary Button Touched
        bool rightHandSecondaryButtonTouchValue;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out rightHandSecondaryButtonTouchValue) && rightHandSecondaryButtonTouchValue) RightHandSecondaryButtonTouched.Invoke();

        // Joysick Touched
        bool rightHandPrimaryAxisTouchValue;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out rightHandPrimaryAxisTouchValue) && rightHandPrimaryAxisTouchValue) RightHandAxisTouched.Invoke(); ;

        // Joystick Pressed
        bool rightHandPrimaryAxisValue;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightHandPrimaryAxisValue) && rightHandPrimaryAxisValue) RightHandAxisDown.Invoke();

        // Joystick Direction
        rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightHandAxisDirection);

    }


    private void LeftHandTrigger()
    {
        // Trigger Pressed
        bool leftHandTriggerTemp;
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out leftHandTriggerTemp);
        if (leftHandTriggerTemp == leftHandTriggerValue) return;
        else if (leftHandTriggerTemp != leftHandTriggerValue) leftHandTriggerValue = leftHandTriggerTemp;
        if (leftHandTriggerValue) LeftHandTriggerDown.Invoke();
        if (!leftHandTriggerValue) LeftHandTriggerUp.Invoke();
    }

    private void LeftHandGrip()
    {
        // Grip Pressed
        bool leftHandGripTemp;
        leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out leftHandGripTemp);
        if (leftHandGripTemp == leftHandGripValue) return;
        else if (leftHandGripTemp != leftHandGripValue) leftHandGripValue = leftHandGripTemp;
        if (leftHandGripValue) LeftHandGripDown.Invoke();
        if (!leftHandGripValue) LeftHandGripUp.Invoke();
    }

    private void LeftHandPrimaryButton()
    {
        // Primary Button Pressed
        bool leftHandPrimaryButtonTemp;
        leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out leftHandPrimaryButtonTemp);
        if (leftHandPrimaryButtonTemp == leftHandPrimaryButtonValue) return;
        else if (leftHandPrimaryButtonTemp != leftHandPrimaryButtonValue) leftHandPrimaryButtonValue = leftHandPrimaryButtonTemp;
        if (leftHandPrimaryButtonValue) LeftHandPrimaryButtonDown.Invoke();
        if (!leftHandPrimaryButtonValue) LeftHandPrimaryButtonUp.Invoke();
    }

    private void LeftHandSecondaryButton()
    {
        // Secondary Button Pressed
        bool leftHandSecondaryButtonTemp;
        leftHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out leftHandSecondaryButtonTemp);
        if (leftHandSecondaryButtonTemp == leftHandSecondaryButtonValue) return;
        else if (leftHandSecondaryButtonTemp != leftHandSecondaryButtonValue) leftHandSecondaryButtonValue = leftHandSecondaryButtonTemp;
        if (leftHandSecondaryButtonValue) LeftHandSecondaryButtonDown.Invoke();
        if (!leftHandSecondaryButtonValue) LeftHandSecondaryButtonUp.Invoke();
    }

    private void MenuButton()
    {
        bool menuButtonTemp;
        leftHandDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonTemp);
        if (menuButtonTemp == menuButtonValue) return;
        else if (menuButtonTemp != menuButtonValue) menuButtonValue = menuButtonTemp;
        if (menuButtonValue) MenuDown.Invoke();
        if (!menuButtonValue) MenuUp.Invoke();
    }

    private void LeftHandUserInput()
    {
        LeftHandTrigger();
        LeftHandGrip();
        LeftHandPrimaryButton();
        LeftHandSecondaryButton();
        MenuButton();

        // Primary Button Touched
        bool leftHandPrimaryButtonTouchValue;
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out leftHandPrimaryButtonTouchValue) && leftHandPrimaryButtonTouchValue) LeftHandPrimaryButtonTouched.Invoke();

        // Secondary Button Touched
        bool leftHandSecondaryButtonTouchValue;
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out leftHandSecondaryButtonTouchValue) && leftHandSecondaryButtonTouchValue) LeftHandSecondaryButtonTouched.Invoke();

        // Joysick Touched
        bool leftHandSecondaryAxisTouchValue;
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out leftHandSecondaryAxisTouchValue) && leftHandSecondaryAxisTouchValue) LeftHandAxisTouched.Invoke(); ;

        // Joystick Pressed
        bool leftHandSecondaryAxisValue;
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftHandSecondaryAxisValue) && leftHandSecondaryAxisValue) LeftHandAxisDown.Invoke();

        // Joystick Direction
        leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftHandAxisDirection);
    }

    //if Player take off the HMD, pause the game
    private void Pause()
    {
        
    }
}
