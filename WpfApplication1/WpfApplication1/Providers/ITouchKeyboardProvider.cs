namespace TouchKeyboardSample.Providers
{
    /// <summary>
    /// Defines a touch keyboard interface.
    /// </summary>
    public interface ITouchKeyboardProvider
    {
        /// <summary>
        /// Display the touch keyboard.
        /// </summary>
        void ShowTouchKeyboard();

        /// <summary>
        /// Hide or minimize the touch keyboard.
        /// </summary>
        void HideTouchKeyboard();
    }
}
