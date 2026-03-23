
using Serilog;
using Task3;

/**
Task 3: Exception Handling & Debugging
**/
class Program
{
    static void Main ()
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute)
        .CreateLogger();
        try
        {
        Doctor doc = new ("Smith", 123456789, 45, "Cardiology");
        Patient pat = new ("Alice", 987654321, 3, "Chest pain");

        doc.Diagnosis = "Hypertension";

        pat.GetDetails();
        doc.GetDetails();
            
        }
        catch (InvalidAgeException exce)
        {
            // Console.WriteLine("custom error:" + exce.Message );
            //logging
            Log.Error(exce, "invalid age entered");
        } catch(Exception exce)
        {
            Console.WriteLine("general erroe:" + exce.Message);
        }
        finally
        {
            Console.WriteLine("program executed successfully");
        }
    }
}