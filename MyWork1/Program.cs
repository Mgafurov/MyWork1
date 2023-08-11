using System;
using System.Collections.Generic;
using System.IO;

class Student
{
    public string Name { get; set; }
    public List<int> Grades { get; set; }

    public double CalculateAverageGrade()
    {
        if (Grades.Count == 0)
            return 0;

        int sum = 0;
        foreach (int grade in Grades)
        {
            sum += grade;
        }
        return (double)sum / Grades.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string journalFileName = "journal.txt";
        List<Student> students = new List<Student>();

        if (!File.Exists(journalFileName))
        {
            CreateJournalFile(journalFileName);
        }
        else
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать новый журнал");
            Console.WriteLine("2. Открыть существующий журнал");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 2)
            {
                students = LoadJournalFromFile(journalFileName);
            }
            else
            {
                CreateJournalFile(journalFileName);
            }
        }

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотреть средние баллы студентов");
            Console.WriteLine("2. Добавить студента и его оценки");
            Console.WriteLine("3. Сохранить и выйти");
            int actionChoice = int.Parse(Console.ReadLine());

            switch (actionChoice)
            {
                case 1:
                    DisplayStudentAverages(students);
                    break;
                case 2:
                    AddStudentAndGrades(students);
                    break;
                case 3:
                    SaveJournalToFile(journalFileName, students);
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }

    static void CreateJournalFile(string fileName)
    {
        using (StreamWriter writer = File.CreateText(fileName))
        {
            // Создание пустого файла
        }
        Console.WriteLine("Файл журнала создан.");
    }

    static List<Student> LoadJournalFromFile(string fileName)
    {
        List<Student> students = new List<Student>();
        string[] lines = File.ReadAllLines(fileName);

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            string name = parts[0];

            string[] gradeStrings = parts[1].Split(',');
            List<int> grades = new List<int>();
            foreach (string gradeString in gradeStrings)
            {
                int grade = int.Parse(gradeString);
                grades.Add(grade);
            }

            Student student = new Student { Name = name, Grades = grades };
            students.Add(student);
        }

        return students;
    }

    static void DisplayStudentAverages(List<Student> students)
    {
        foreach (Student student in students)
        {
            double averageGrade = student.CalculateAverageGrade();
            Console.WriteLine($"{student.Name}: Средний балл - {averageGrade:F2}");
        }
    }

    static void AddStudentAndGrades(List<Student> students)
    {
        Console.Write("Введите имя студента: ");
        string name = Console.ReadLine();

        Console.Write("Введите оценки студента через запятую: ");
        string[] gradeStrings = Console.ReadLine().Split(',');
        List<int> grades = new List<int>();
        foreach (string gradeString in gradeStrings)
        {
            int grade = int.Parse(gradeString);
            grades.Add(grade);
        }

        Student newStudent = new Student { Name = name, Grades = grades };
        students.Add(newStudent);

        Console.WriteLine("Студент и его оценки добавлены.");
    }

    static void SaveJournalToFile(string fileName, List<Student> students)
    {
        using (StreamWriter writer = new StreamWriter(fileName, append: false))
        {
            foreach (Student student in students)
            {
                string gradeString = string.Join(",", student.Grades);
                writer.WriteLine($"{student.Name};{gradeString}");
            }
        }
    }
}
