using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

enum TaskCategory {
    Personal,
    Work,
    Errands
}

class TaskItem{
    public string Name {get; set;}
    public string Description {get; set;}
    public TaskCategory Category {get; set;}
    public bool IsCompleted {get; set;}
}

class TaskManager{
    private List<TaskItem> tasks = new List<TaskItem>();
    private string filePath = "tasks.csv";

    public async Task LoadTasksAsync(){
        try{
            if (File.Exists(filePath) ){
                string[] lines = await File.ReadAllLinesAsync(filePath);
                foreach (string line in lines){
                    string[] values = line.Split(',');

                    if (values.Length >= 4){
                        TaskItem task = new TaskItem{Name = values[0],Description = values[1], Category = (TaskCategory)Enum.Parse(typeof(TaskCategory),values[2]), IsCompleted = bool.Parse(values[3])};
                        tasks.Add(task);
                    }
                }
            } 

        } catch (Exception ex){
            Console.WriteLine($"Error while loading tasks: {ex.Message}");
        }
    } 

    public async Task SaveTasksAsync () {
        try {
            List<string> lines = new List<string>();
            foreach(TaskItem task in tasks){
                string line = $"{task.Name},{task.Description},{task.Category},{task.IsCompleted}";
                lines.Add(line);
            } 

            await File.WriteAllLinesAsync(filePath,lines);
        }catch (Exception ex)
        {
            Console.WriteLine($"Error while saving tasks: {ex.Message}");
        }
    
    }

    public void AddTask(TaskItem task){
        tasks.Add(task);
    }

    public void DisplayTasks()
    {
        Console.WriteLine("Task lists: ");
        foreach (TaskItem task in tasks)
        {
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine($"Completed: {task.IsCompleted}");
            Console.WriteLine();
        }
    }

    public void DisplayTasksByCategory(TaskCategory category){
        var filteredTasks = tasks.FindAll(task => task.Category == category);

        Console.WriteLine($"Filtered task of {category} category: ");

        foreach (TaskItem task in filteredTasks)
        {
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine($"Completed: {task.IsCompleted}");
            Console.WriteLine();
        }
    }
}


class Program{
    public static async Task Main(string[] args){
        TaskManager taskManager = new TaskManager();
        await taskManager.LoadTasksAsync();

        while (true){
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Display All Tasks");
            Console.WriteLine("3. Display Tasks by Category");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch(choice){
                case "1":
                    Console.Write("Enter task name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();
                    Console.WriteLine("Task Categories: 1. Personal  2. Work  3. Errands");
                    Console.Write("Select task category (1/2/3): ");
                    int categoryChoice = int.Parse(Console.ReadLine()) - 1;
                    TaskCategory category = (TaskCategory)categoryChoice;
                    TaskItem newTask = new TaskItem
                    {
                        Name = name,
                        Description = description,
                        Category = category,
                        IsCompleted = false
                    };
                    taskManager.AddTask(newTask);
                    break;
                case "2":
                    taskManager.DisplayTasks();
                    break;
                case "3":
                    Console.WriteLine("Select Category: 1. Personal  2. Work  3. Errands");
                    int filterChoice = int.Parse(Console.ReadLine()) - 1;
                    TaskCategory filterCategory = (TaskCategory)filterChoice;
                    taskManager.DisplayTasksByCategory(filterCategory);
                    break;
                case "4":
                    await taskManager.SaveTasksAsync();
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    } 
}