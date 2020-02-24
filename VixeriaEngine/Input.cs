using Microsoft.DirectX.DirectInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VixeriaEngine
{
    /// <summary>
    /// Holds all input information.
    /// </summary>
    static class Input
    {
        // dictionary of all keys that are pressed in some way
        static Dictionary<Key, KeyInfo> keys = new Dictionary<Key, KeyInfo>();
        // dictionary of all mouse buttons that are pressed in some way
        static Dictionary<int, MouseButtonInfo> mouseButtons = new Dictionary<int, MouseButtonInfo>();

        // reference to DirectX keyboard device for input detection
        static Device keyboard;
        // reference to DirectX mouse device for input detection
        static Device mouse;

        /// <summary>
        /// Initialize all input devices.
        /// </summary>
        /// <param name="_keyboard">DirectX keyboard device</param>
        /// <param name="_mouse">DirectX mouse device</param>
        public static void Initialize(Control target)
        {
            // initialize keyboard device
            keyboard = new Device(SystemGuid.Keyboard);
            keyboard.SetCooperativeLevel(target, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);
            keyboard.Acquire();

            // initialize mouse device
            mouse = new Device(SystemGuid.Mouse);
            mouse.Acquire();
        }

        /// <summary>
        /// Updates all inputs 
        /// </summary>
        public static void UpdateInputs()
        {
            UpdateKeyboardInputs();
            UpdateMouseInputs();
        }

        static void UpdateKeyboardInputs()
        {
            Key[] pressedKeys = keyboard.GetPressedKeys();
            if (pressedKeys.Length != 0)
            {
                foreach (Key k in pressedKeys)
                {
                    // is key on the list?
                    if (keys.ContainsKey(k))
                    {
                        KeyInfo keyInfo;
                        keys.TryGetValue(k, out keyInfo);

                        if (keyInfo.isDown)
                            keyInfo.isDown = false;
                        if (keyInfo.isUp)
                            keyInfo.isUp = false;

                        keyInfo.pressed = true;
                    }
                    else
                    {
                        KeyInfo keyInfo = new KeyInfo(k);
                        keys.Add(k, keyInfo);
                    }
                }
            }

            List<Key> keysToRemove = new List<Key>();
            foreach (KeyValuePair<Key, KeyInfo> keyValuePair in keys)
            {
                KeyInfo keyInfo = keyValuePair.Value;
                if (!keyInfo.pressed)
                {
                    if (keyInfo.isUp)
                    {
                        keysToRemove.Add(keyValuePair.Key);
                    }
                    else
                    {
                        if (keyInfo.isDown == true)
                            keyInfo.isDown = false;

                        keyInfo.isUp = true;
                    }
                }
                keyInfo.pressed = false;
            }

            foreach (Key k in keysToRemove)
            {
                keys.Remove(k);
            }
            keysToRemove.Clear();
        }

        static void UpdateMouseInputs()
        {
            byte[] mouseBytes = mouse.CurrentMouseState.GetMouseButtons();
            mouseBytes.ArrayToString();

            for (int i = 0; i < mouseBytes.Length; i++)
            {
                if (mouseBytes[i] == 0)
                    continue;

                if (mouseButtons.ContainsKey(i))
                {
                    MouseButtonInfo mouseButtonInfo;
                    mouseButtons.TryGetValue(i, out mouseButtonInfo);

                    if (mouseButtonInfo.isDown)
                        mouseButtonInfo.isDown = false;
                    if (mouseButtonInfo.isUp)
                        mouseButtonInfo.isUp = false;

                    mouseButtonInfo.pressed = true;
                }
                else
                {
                    MouseButtonInfo mouseButtonInfo = new MouseButtonInfo(i);
                    mouseButtons.Add(i, mouseButtonInfo);
                }
            }

            List<int> buttonsToRemove = new List<int>();
            foreach (KeyValuePair<int, MouseButtonInfo> keyValuePair in mouseButtons)
            {
                MouseButtonInfo mouseButtonInfo = keyValuePair.Value;
                if (!mouseButtonInfo.pressed)
                {
                    if (mouseButtonInfo.isUp)
                    {
                        buttonsToRemove.Add(keyValuePair.Key);
                    }
                    else
                    {
                        if (mouseButtonInfo.isDown == true)
                            mouseButtonInfo.isDown = false;

                        mouseButtonInfo.isUp = true;
                    }
                }
                mouseButtonInfo.pressed = false;
            }

            foreach (int i in buttonsToRemove)
            {
                mouseButtons.Remove(i);
            }
            buttonsToRemove.Clear();
        }

        /// <summary>
        /// Returns true on the frame the player presses the given key.
        /// </summary>
        /// <param name="key">DirectX Key</param>
        public static bool GetKeyDown(Key key)
        {
            KeyInfo keyInfo;
            keys.TryGetValue(key, out keyInfo);

            if (keyInfo != null && keyInfo.isDown)
                return true;

            return false;
        }

        /// <summary>
        /// Returns true every frame the player keeps the given key pressed.
        /// </summary>
        /// <param name="key">DirectX Key</param>
        public static bool GetKey(Key key)
        {
            KeyInfo keyInfo;
            keys.TryGetValue(key, out keyInfo);

            if (keyInfo != null)
                return true;

            return false;
        }


        /// <summary>
        /// Returns true on the frame the player releases the given key.
        /// </summary>
        /// <param name="key">DirectX Key</param>
        public static bool GetKeyUp(Key key)
        {
            KeyInfo keyInfo;
            keys.TryGetValue(key, out keyInfo);

            if (keyInfo != null && keyInfo.isUp)
                return true;

            return false;
        }

        /// <summary>
        /// Returns true on the frame the player presses the given mouse button.
        /// </summary>
        /// <param name="mouseButton">Mouse button index (0 = LMB, 1 = RMB, 2 = MMB, 3 = Mouse4, 4 = Mouse5)</param>
        public static bool GetMouseButtonDown(int mouseButton)
        {
            MouseButtonInfo mouseButtonInfo;
            mouseButtons.TryGetValue(mouseButton, out mouseButtonInfo);

            if (mouseButtonInfo != null && mouseButtonInfo.isDown)
                return true;

            return false;
        }


        /// <summary>
        /// Returns true every frame the player keeps the given mouse button pressed.
        /// </summary>
        /// <param name="mouseButton">Mouse button index (0 = LMB, 1 = RMB, 2 = MMB, 3 = Mouse4, 4 = Mouse5)</param>
        public static bool GetMouseButton(int mouseButton)
        {
            MouseButtonInfo mouseButtonInfo;
            mouseButtons.TryGetValue(mouseButton, out mouseButtonInfo);

            if (mouseButtonInfo != null)
                return true;

            return false;
        }

        /// <summary>
        /// Returns true on the frame the player releases the given mouse button.
        /// </summary>
        /// <param name="mouseButton">Mouse button index (0 = LMB, 1 = RMB, 2 = MMB, 3 = Mouse4, 4 = Mouse5)</param>
        public static bool GetMouseButtonUp(int mouseButton)
        {
            MouseButtonInfo mouseButtonInfo;
            mouseButtons.TryGetValue(mouseButton, out mouseButtonInfo);

            if (mouseButtonInfo != null && mouseButtonInfo.isUp)
                return true;

            return false;
        }

        /// <summary>
        /// Returns world coordinates of the mouse position.
        /// </summary>
        public static Vector2 mousePosition
        {
            get
            {
                Point mousePos = GameForm.instance.PointToClient(Cursor.Position);
                return new Vector2(mousePos.X, -mousePos.Y);
            }
        }
    }

    // holds all key input information
    class KeyInfo
    {
        // key is pressed this frame
        public bool pressed;
        // key is down this frame
        public bool isDown;
        // key is up this frame
        public bool isUp;
        // Key this info is related to
        public Key key;

        public KeyInfo(Key _key)
        {
            pressed = true;
            isDown = true;
            isUp = false;
            key = _key;
        }
    }

    // holds all mouse button input information
    class MouseButtonInfo
    {
        // mouse button is pressed this frame
        public bool pressed;
        // mouse button is down this frame
        public bool isDown;
        // mouse button is up this frame
        public bool isUp;
        // mouse button index this info is related to
        public int mouseButton;

        public MouseButtonInfo(int _mouseButton)
        {
            pressed = true;
            isDown = true;
            isUp = false;
            mouseButton = _mouseButton;
        }
    }
}
