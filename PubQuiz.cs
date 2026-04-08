public class QuizQuestion
{
    public string Text { get; set; }
    public string Answer { get; set; }
    public string Category { get; set; } 

    public QuizQuestion(string text, string answer, string category)
    {
        Text = text;
        Answer = answer;
        Category = category;
    }
}

class Program
{
    static void Main()
    {
        bool isValid = false;
        int index = 0;
        List<QuizQuestion> masterQuiz = new List<QuizQuestion>
        {
            new QuizQuestion("What is the capital of France?", "paris", "Geography"),
            new QuizQuestion("What is the longest river in the world?", "nile", "Geography"),
            new QuizQuestion("Where is Big Ben located?", "london", "Geography"),
            new QuizQuestion("Who comes up with the theory c² = a² + b² in a right triangle", "pythagoras", "Math"),
            new QuizQuestion("What is 3 * 9?", "27", "Math"),
            new QuizQuestion("What is the square root of 16?", "4", "Math"),
            new QuizQuestion("In which year did the WW1 begin" , "1914", "History"),
            new QuizQuestion("Who drew the Mona Lisa", "da vinci", "History"),
            new QuizQuestion("Who was the 'Sun King' ", "Louis XIV", "History"),
            new QuizQuestion("What is the final boss in Minecraft ", "end dragon", "Games"),
            new QuizQuestion("What is the popular ARPG series from Bethesda", "the elder scrolls", "Games"),
            new QuizQuestion("Which company created the game 'Super Mario'", "nintendo", "Games"),

        };

        Console.WriteLine("--- Welcome to the Pub Quiz! ---");
        var categories = masterQuiz.Select(q => q.Category).Distinct().ToList();

        Console.WriteLine("Choose a category:");
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        while (!isValid)
        {
            Console.Write("\nEnter choice (1 - 4): ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out index) && index >= 1 && index <= categories.Count)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Please choose a number from 1 to 4.");
            }
        }
        string selectedCategory = categories[index - 1];
        var filteredQuiz = masterQuiz.Where(q => q.Category == selectedCategory).ToList();

        Console.WriteLine($"--- Starting {selectedCategory} Round ---");

        Random rng = new Random();
        var randomizedQuiz = filteredQuiz.OrderBy(q => rng.Next()).ToList();

        int score = 0;

        foreach (var question in randomizedQuiz)
        {

            {
                Console.WriteLine($"[{question.Category}] {question.Text}");
                Console.Write("Your Answer: ");
                string input = Console.ReadLine()?.ToLower().Trim();
                if (input == question.Answer)
                {
                    Console.WriteLine("Correct! ");
                    score++;
                }
                else
                {
                    Console.WriteLine("Wrong! ");
                }
            }
        }

        Console.WriteLine("-------------------------------");
        Console.WriteLine($"Quiz Completed! Final Score: {score}/{randomizedQuiz.Count}");
        Console.WriteLine("-------------------------------");
    }
}
