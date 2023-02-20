// Adding a new method to String class

// Define a custom namespace
// Add using this namespace in the class where you want to add the method
namespace ExtensionMethods;

// Declare the class as public and static
public static class MyExtensions
{
    // declare the method as public and static
    public static int WordCount(this string str)
    {
        return str.Split(new char[] { ' ', '.', '?' },
            StringSplitOptions.RemoveEmptyEntries).Length;
    }
}