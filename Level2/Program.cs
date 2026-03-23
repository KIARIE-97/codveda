using Level2;

class Program
{
    static void Main()
    { 
       try
       {
         Console.WriteLine("1. Register Patient \n 2. Add Doctor \n 3. Get your diagnosis \n Choose an option:");
        string? input = Console.ReadLine();
        if(input == "1")
        {
            Console.WriteLine("enter your fullname");
            string? name = Console.ReadLine();
            
             Console.WriteLine("enter your age");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your phoneNumber");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());

             Console.WriteLine("what are your symptoms");
            string? symptoms = Console.ReadLine();

            Patient patient = new(name, phoneNumber, age, symptoms);
            patient.GetDetails();

        }else if(input == "2")
        {
             Console.WriteLine("enter your fullname");
            string? name = Console.ReadLine();
            
             Console.WriteLine("enter your age");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your phoneNumber");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());

             Console.WriteLine("what are your symptoms");
            string? specialty = Console.ReadLine();

            Doctor doctor = new(name, phoneNumber, age, specialty);
            doctor.GetDetails();
        }else
            {
                
            }

       }
       catch (InvalidAgeException exce)
       {
        
        Console.WriteLine("custom error:" + exce.Message );
       }
        finally
        {
            Console.WriteLine("program executed successfully");
        }
    }
}