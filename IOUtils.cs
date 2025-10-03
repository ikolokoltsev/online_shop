namespace IOUtilsApp;

public static class IOUtils
{
    /// <summary>
    /// This method is a modified variant of the <see cref="Console.ReadLine()"/>.
    /// This implementation will also process exceptions 
    /// </summary>
    /// <returns>NotNullable string</returns>
    /// <remarks>Note that an empty string is an error case</remarks>
    public static string StringUserInput()
    {
        string? input = Console.ReadLine();
        if (input != null && input.GetType() == typeof(string))
        {
            return input;
        }
        else if (input != null)
        {
            Type type = input.GetType();
            Console.WriteLine($"Unexpected type {type} for text input");
            return "";
        }
        else
        {
            Console.WriteLine($"There is a null in text input");
            return "";
        }
    }

    /// <summary>
    /// Under the hood use <see cref="StringUserInput()"/> and <seealso cref="Console.ReadLine()"/>
    /// To be aware of the issues, we are parsing a string through the try-catch.
    /// In this method we are processing <see cref="FormatException"/>,  <see cref="OverflowException"/>, <see cref="ArgumentNullException"/>.
    /// </summary>
    /// <returns>Returns int</returns>
    /// <remarks>-1 is an error case</remarks>
    public static int IntUserInput()
    {
        string input = StringUserInput();
        try
        {
            int parsed_input = int.Parse(input);
            return parsed_input;
        }
        catch (FormatException error)
        {
            Console.WriteLine($"Ohhhh... Wrong format {error.Message}");
        }
        catch (OverflowException error)
        {
            Console.WriteLine($"Are you kidding me? {error.Message}");
        }
        catch (ArgumentNullException error)
        {
            Console.WriteLine($"That's not even funny! {error.Message}");
        }

        return -1;
    }

    /// <summary>
    /// Simple wrapper on top of the <see cref="StringUserInput()"/>
    /// that cleans the console right after submitting the input
    /// </summary>
    /// <returns>NotNullable string</returns>
    /// <remarks>Empty string "" is an error case</remarks>
    public static string StringUserInputWithCleanUp()
    {
        string input = StringUserInput();
        Console.Clear();
        return input;
    }

    /// <summary>
    /// Simple wrapper on top of the <see cref="IntUserInput()"/>
    /// that cleans the console right after submitting the input
    /// </summary>
    /// <returns>NotNullable int</returns>
    /// <remarks>-1 is an error case</remarks>
    public static int IntUserInputWithCleanUp()
    {
        int input = IntUserInput();
        Console.Clear();
        return input;
    }

    /// <summary>
    /// This method allows us to control the <see cref="WriteLine()"/> colors 
    /// </summary>
    /// <param name="print_message">string</param>
    /// <param name="foreground_color">ConsoleColor?</param>
    /// <param name="background_color">ConsoleColor?</param>
    public static void ColorizedPrint(string print_message, ConsoleColor foreground_color = ConsoleColor.White,
        object background_color = null)
    {
        if (background_color is ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        Console.ForegroundColor = foreground_color;
        Console.WriteLine(print_message);
        Console.ResetColor();
    }

    public static string PasswordInput()
    {
        string password_acc = "";
        ConsoleKeyInfo key_pressed;

        while (true)
        {
            key_pressed = Console.ReadKey(true);
            if (key_pressed.Equals(ConsoleKey.Enter))
            {
                break;
            }
            else if (key_pressed.Equals(ConsoleKey.Backspace))
            {
                if (password_acc.Length > 0)
                {
                    password_acc = password_acc.Substring(0, password_acc.Length - 1);
                }
            }
            else
            {
                password_acc += key_pressed.KeyChar;
            }
        }

        return password_acc;
    }
}