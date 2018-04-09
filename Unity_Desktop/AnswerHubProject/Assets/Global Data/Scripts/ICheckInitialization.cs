/// <summary>
/// An interface which has a function that should be called on initialising an object to 
/// check if there are any exceptions during the initialization procecss. More for organisation
/// than usability.
/// </summary>
/// 

namespace GlobalProject
{
    public interface ICheckInitialization
    {
        /// <summary>
        /// Checks for any problems during initialization. Any problems that occur should throw an InitializationException.
        /// This should be used in tandem with either the defined DEBUG preprocessor or with unit testing.
        /// </summary>
        void CheckInitializationForExceptions();
    }
}