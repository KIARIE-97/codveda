// See https://aka.ms/new-console-template for more information
/** 
Task 2: C# Basics & Object-
Oriented Programming (OOP)
********************************************
*/
class Person
{
    public string FullName{get; set;} = string.Empty;
    public int PhoneNumber{get;set;}
    public int Age{get; set;}

    public Person(string fullName, int phoneNumber, int age)
    {
        FullName = fullName;
        Age= age;
        PhoneNumber =phoneNumber;
    }
    public virtual void GetDetails()
    {
           Console.WriteLine($"Name: {FullName}, Age: {Age}");
    }
}
class Doctor(string fullName, int phoneNumber, int age, string specialty) : Person(fullName,phoneNumber,age)
{
    public string? Specialty { get; set; } = specialty;
    public string? Diagnosis{get; set;}

    public override void GetDetails()
    {
        Console.WriteLine($"Dr. {FullName} ({Specialty}) diagnosed: {Diagnosis}");
    }
}
class Patient(string fullName, int phoneNumber, int age, string symptoms): Person(fullName,phoneNumber,age)
{
    public string? Symptoms{ get; set;} = symptoms;
    public static void BookAppointment() {}
    public override void GetDetails()
    {
        Console.WriteLine($"my symptoms are: {Symptoms}");
    }
}

    class Program
{
        static void Main()
    {
        Doctor doc = new ("Smith", 123456789, 45, "Cardiology");
        Patient pat = new ("Alice", 987654321, 30, "Chest pain");

        doc.Diagnosis = "Hypertension";

        doc.GetDetails();
        pat.GetDetails();
    }

}


