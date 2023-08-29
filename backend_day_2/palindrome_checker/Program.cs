public class Palindrome{
    public static void Main(){
        string input;

        Console.WriteLine("Enter a string to check whether it is palindrome: ");
        input = Console.ReadLine();
        while(input.Length == 0){
            Console.WriteLine("Invalid! please enter a input");
            input = Console.ReadLine();
        }


        if (isPalindrome(input)){
            Console.WriteLine(input + " is palindrome");
        }else{
            Console.WriteLine(input + " isnot palindrome");
        }
    }

    public static bool isPalindrome(string input){
        string cleanedInput = "";

        foreach (char c in input){
            if (char.IsLetterOrDigit(c)){
                cleanedInput += char.ToLower(c);
            }
        }
        int left = 0;
        int right = cleanedInput.Length - 1;

        while(left <= right){
            if (input[left] != input[right]){
                return false;
            }

            left++;
            right--;
        }

        return true;
    }
}