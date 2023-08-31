using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int RollNumber { get; }
    public string Grade { get; set; }

    private static int rollNumberCounter = 1;

    public Student(string name, int age, string grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
        RollNumber = rollNumberCounter++;
    }
}

class StudentList<T> where T : Student
{
    public List<T> Students { get; set; }

    public StudentList()
    {
        Students = new List<T>();
    }

    public void AddStudent(T student)
    {
        Students.Add(student);
    }

    public T SearchStudentByName(string name)
    {
        return Students.FirstOrDefault(student => student.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public T SearchStudentById(int rollNumber)
    {
        return Students.FirstOrDefault(student => student.RollNumber == rollNumber);
    }

    public void DisplayAllStudents()
    {
        if (Students.Count == 0){
            Console.WriteLine("No students to display.");
        } else{

            foreach (var student in Students)
            {
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Roll Number: {student.RollNumber}, Grade: {student.Grade}");
            }
        }
    }

    public async Task SerializeToJsonAsync(string filePath)
    {
        string jsonData = JsonConvert.SerializeObject(this);
        await File.WriteAllTextAsync(filePath, jsonData);
    }

    public static async Task<StudentList<T>> DeserializeFromJsonAsync(string filePath)
    {
        string jsonData = await File.ReadAllTextAsync(filePath);
        return JsonConvert.DeserializeObject<StudentList<T>>(jsonData);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        StudentList<Student> studentList;
        if (!File.Exists("student_data.json")){
            studentList = new StudentList<Student>();
        }else{
            studentList = await StudentList<Student>.DeserializeFromJsonAsync("student_data.json");
        }


        bool keepRunning = true;
        while (keepRunning)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Search by Name");
            Console.WriteLine("3. Search by Roll Number");
            Console.WriteLine("4. Display All Students");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Enter Grade: ");
                    string grade = Console.ReadLine();
                    studentList.AddStudent(new Student(name, age, grade));
                    Console.WriteLine("Student added successfully!");
                    break;

                case "2":
                    Console.Write("Enter Name to Search: ");
                    string searchName = Console.ReadLine();
                    Student searchedStudent = studentList.SearchStudentByName(searchName);
                    if (searchedStudent != null)
                    {
                        Console.WriteLine($"Name: {searchedStudent.Name}, Age: {searchedStudent.Age}, Roll Number: {searchedStudent.RollNumber}, Grade: {searchedStudent.Grade}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                    break;

                case "3":
                    Console.Write("Enter Roll Number to Search: ");
                    int searchRollNumber = int.Parse(Console.ReadLine());
                    searchedStudent = studentList.SearchStudentById(searchRollNumber);
                    if (searchedStudent != null)
                    {
                        Console.WriteLine($"Name: {searchedStudent.Name}, Age: {searchedStudent.Age}, Roll Number: {searchedStudent.RollNumber}, Grade: {searchedStudent.Grade}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                    break;

                case "4":
                    Console.WriteLine("All Students:");
                    studentList.DisplayAllStudents();
                    break;

                case "5":
                    keepRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }

        // Serialize and save student data to a JSON file
        await studentList.SerializeToJsonAsync("student_data.json");
    }
}
