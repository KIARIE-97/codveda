using Level2;

class Program
{
    static void Main()
    { 
    
       try
       {
        while(true){
         Patient? patient = null;
        Doctor? doctor = null;

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

            patient = new(name!, phoneNumber, age, symptoms!);
            patient.GetDetails();

        }else if(input == "2")
        {
             Console.WriteLine("enter your fullname");
            string? name = Console.ReadLine();
            
             Console.WriteLine("enter your age");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your phoneNumber");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());

             Console.WriteLine("enter your specialty");
            string? specialty = Console.ReadLine();

            doctor = new(name!, phoneNumber, age, specialty!);
            
        }else if(input == "3")
        {
                if( patient == null || doctor == null)
                {
                    Console.WriteLine("please register doctor and patient first");
                }
                Console.WriteLine("Enter diagnosis");
                string? diagnosis = Console.ReadLine();
                doctor!.Diagnosis = diagnosis;
                doctor.GetDetails();
        }
     if(input == "4") break;
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