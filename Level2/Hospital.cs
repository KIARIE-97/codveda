using System;

namespace Level2;

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

      //add validation
        if (age < 0) {
        throw new InvalidAgeException("Age cannot be negative");
    }
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
class Patient: Person
{
    public string Symptoms{ get; set;} 
    public static void BookAppointment() {}
    public override void GetDetails()
    {
        Console.WriteLine($"my symptoms are: {Symptoms}");
    }
    public Patient (string fullName, int phoneNumber, int age, string symptoms) : base(fullName,phoneNumber,age)
    {
        Symptoms = symptoms;

        if(string.IsNullOrEmpty(symptoms))
        {
            throw new EmptySyptomsException("please provide your symptoms for dignosis");
        }
        
    }
    
}

//a custom exception
class InvalidAgeException (string message) : Exception(message)
{
    
}
class EmptySyptomsException (string message) : Exception(message)
{
    
}

