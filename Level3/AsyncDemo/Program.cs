using System.Collections.Generic;
using System.Linq;
namespace AsyncDemo;

class Program 
{
    static async Task Main(string[] args)       
     {
            Console.WriteLine("=== ASYNC PROGRAMMING & MULTITHREADING DEMO===\n");
            
            // Run all examples
            await BasicAsyncExample();
            
            await BlockingVsNonBlocking();
            
            await ParallelProcessing.RunParallelExamples();
            
            //thread pools
            ThreadPoolDemo.DemonstrateThreadPool();
            await ThreadPoolDemo.BackgroundWorkerExample();
            
            //race conditions and deadlock
            ConcurrencyProblems.DemonstrateRaceCondition();
            ConcurrencyProblems.DemonstrateDeadlock();
            
            //demo of areal world example- ecommerce.
            await RealWorldExample.ProcessOrders();
            
            Console.WriteLine("\n All demos completed! Press any key to exit...");
            Console.ReadKey();
        }
        
        // Include all the methods from above here
          // SIMPLE EXAMPLE: Making coffee while toasting bread
        static async Task BasicAsyncExample()
        {
            Console.WriteLine(" Starting breakfast preparation...");
            
            // Start both tasks, but don't wait
            var coffeeTask = MakeCoffeeAsync();
            var toastTask = ToastBreadAsync();
            
            // Now wait for both to complete
            await Task.WhenAll(coffeeTask, toastTask);
            
            Console.WriteLine(" Breakfast is ready!");
        }
        
        static async Task MakeCoffeeAsync()
        {
            Console.WriteLine("   Starting coffee machine...");
            await Task.Delay(3000); // Simulates coffee brewing (3 seconds)
            Console.WriteLine("   Coffee is ready!");
        }
        
        static async Task ToastBreadAsync()
        {
            Console.WriteLine("   Putting bread in toaster...");
            await Task.Delay(2000); // Simulates toasting (2 seconds)
            Console.WriteLine("   Toast is ready!");
        }
        
        static async Task BlockingVsNonBlocking()
    {
    Console.WriteLine("\n=== BLOCKING (BAD) ===");
    var startTime = DateTime.Now;
    
    // BLOCKING: You wait for each task to finish
    Console.WriteLine("Starting blocking operations...");
    Dowork1();  // This blocks 
    Dowork2();  // Must wait for Work1 to finish
    Dowork3();  // Must wait for Work2 to finish
    
    Console.WriteLine($"Blocking took: {(DateTime.Now - startTime).TotalSeconds} seconds");
    
    Console.WriteLine("\n=== NON-BLOCKING (GOOD) ===");
    startTime = DateTime.Now;
    
    // NON-BLOCKING: Start all at once
    Console.WriteLine("Starting non-blocking operations...");
    var task1 = DoWorkAsync(1);
    var task2 = DoWorkAsync(2);
    var task3 = DoWorkAsync(3);
    
    await Task.WhenAll(task1, task2, task3);
    
    Console.WriteLine($"Non-blocking took: {(DateTime.Now - startTime).TotalSeconds} seconds");
}

static void Dowork1() { Thread.Sleep(2000); Console.WriteLine("  Work 1 done"); }
static void Dowork2() { Thread.Sleep(2000); Console.WriteLine("  Work 2 done"); }
static void Dowork3() { Thread.Sleep(2000); Console.WriteLine("  Work 3 done"); }

static async Task DoWorkAsync(int id)
{
    await Task.Delay(2000);
    Console.WriteLine($"  Work {id} done");
}
        
    class RealWorldExample
{
    public static async Task ProcessOrders()
    {
        Console.WriteLine("\n=== REAL-WORLD: E-COMMERCE ORDER PROCESSING ===");
        
        var orders = new[] { "Order1", "Order2", "Order3", "Order4", "Order5" };
        
        Console.WriteLine("Processing 5 orders simultaneously (async/parallel):");
        
        var orderTasks = orders.Select(async order =>
        {
            Console.WriteLine($"Starting {order}");
            
            // Do multiple things for each order in parallel
            var paymentTask = ProcessPaymentAsync(order);
            var inventoryTask = CheckInventoryAsync(order);
            var shippingTask = CalculateShippingAsync(order);
            
            await Task.WhenAll(paymentTask, inventoryTask, shippingTask);
            
            Console.WriteLine($"✅ {order} completed!");
            return $"{order} processed";
        });
        
        var results = await Task.WhenAll(orderTasks);
        Console.WriteLine($"\nAll orders processed: {results.Length} completed");
    }
    
    static async Task ProcessPaymentAsync(string orderId)
    {
        await Task.Delay(1000);
        Console.WriteLine($"  💳 Payment processed for {orderId}");
    }
    
    static async Task CheckInventoryAsync(string orderId)
    {
        await Task.Delay(800);
        Console.WriteLine($"  📦 Inventory checked for {orderId}");
    }
    
    static async Task CalculateShippingAsync(string orderId)
    {
        await Task.Delay(600);
        Console.WriteLine($"  🚚 Shipping calculated for {orderId}");
    }
}
   
   class ConcurrencyProblems
{
    // RACE CONDITION: Two threads fighting over bank account
    private static int _bankBalance = 1000;
    private static readonly object _lockObject = new object(); // The solution!
    
    public static void DemonstrateRaceCondition()
    {
        Console.WriteLine("\n=== RACE CONDITION DEMO ===");
        Console.WriteLine($"Starting balance: ${_bankBalance}");
        
        // PROBLEM: Without locking, threads can interfere
        Console.WriteLine("\n WITHOUT LOCK (Race condition may occur):");
        var tasks = new List<Task>();
        
        // Create 10 threads all trying to withdraw money
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => WithdrawUnsafe(100)));
        }
        
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Final balance (unsafe): ${_bankBalance}");
        
        // Reset for safe example
        _bankBalance = 1000;
        Console.WriteLine($"\n WITH LOCK (Thread-safe):");
        tasks.Clear();
        
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => WithdrawSafe(100)));
        }
        
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Final balance (safe): ${_bankBalance}");
    }
    
    static void WithdrawUnsafe(int amount)
    {
        if (_bankBalance >= amount)
        {
            // This delay makes race condition more likely
            Thread.Sleep(10);
            _bankBalance -= amount;
            Console.WriteLine($"  Withdrew ${amount}, remaining: ${_bankBalance}");
        }
        else
        {
            Console.WriteLine($"  Failed to withdraw ${amount}, insufficient funds");
        }
    }
    
    static void WithdrawSafe(int amount)
    {
        lock (_lockObject) // Only one thread can enter here at a time
        {
            if (_bankBalance >= amount)
            {
                Thread.Sleep(10);
                _bankBalance -= amount;
                Console.WriteLine($"  Withdrew ${amount}, remaining: ${_bankBalance}");
            }
            else
            {
                Console.WriteLine($"  Failed to withdraw ${amount}, insufficient funds");
            }
        }
    }
    
    // DEADLOCK: Two threads waiting for each other
    private static object _resourceA = new object();
    private static object _resourceB = new object();
    
    public static void DemonstrateDeadlock()
    {
        Console.WriteLine("\n=== DEADLOCK DEMO ===");
        Console.WriteLine("⚠️ Starting deadlock demonstration...");
        
        var task1 = Task.Run(() => Task1());
        var task2 = Task.Run(() => Task2());
        
        // Wait with timeout to avoid hanging
        Task.WaitAny(new[] { task1, task2 }, 2000);
        Console.WriteLine("Tasks deadlocked! They're both waiting for each other.");
        
        Console.WriteLine("\n🔧 HOW TO FIX DEADLOCKS:");
        Console.WriteLine("1. Always lock resources in the same order");
        Console.WriteLine("2. Use timeout when waiting for locks");
        Console.WriteLine("3. Avoid nested locks when possible");
    }
    
    static void Task1()
    {
        lock (_resourceA)
        {
            Console.WriteLine("Task 1 locked A, waiting for B...");
            Thread.Sleep(100);
            lock (_resourceB) // This will never get B (Task2 has it)
            {
                Console.WriteLine("Task 1 got both locks");
            }
        }
    }
    
    static void Task2()
    {
        lock (_resourceB)
        {
            Console.WriteLine("Task 2 locked B, waiting for A...");
            Thread.Sleep(100);
            lock (_resourceA) // This will never get A (Task1 has it)
            {
                Console.WriteLine("Task 2 got both locks");
            }
        }
    }
}
   
    class ThreadPoolDemo
{
    public static void DemonstrateThreadPool()
    {
        Console.WriteLine("\n=== THREAD POOL DEMO ===");
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");
        
        // ThreadPool 
        for (int i = 1; i <= 5; i++)
        {
            int taskNumber = i; // Capture variable
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine($"Task {taskNumber} running on thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000); // Simulate work
                Console.WriteLine($"Task {taskNumber} completed");
            });
        }
        
        // Give threads time to complete
        Thread.Sleep(3000);
        Console.WriteLine("All thread pool tasks queued and processed");
    }
    
    public static async Task BackgroundWorkerExample()
    {
        Console.WriteLine("\n=== BACKGROUND WORKER SIMULATION ===");
        
        // Background worker does heavy work
        Console.WriteLine("Starting heavy calculation in background...");
        
        var heavyWork = Task.Run(() =>
        {
            int result = 0;
            for (int i = 1; i <= 100; i++)
            {
                result += i;
                Thread.Sleep(10); // Simulate work
                if (i % 20 == 0)
                    Console.WriteLine($"  Progress: {i}%");
            }
            return result;
        });
        
        // Main thread continues working
        Console.WriteLine("Main thread is free to do other things...");
        for (int i = 1; i <= 3; i++)
        {
            await Task.Delay(500);
            Console.WriteLine($"  Main thread doing other work... {i}");
        }
        
        int finalResult = await heavyWork;
        Console.WriteLine($"Background calculation result: {finalResult}");
    }
}

    class ParallelProcessing
{
    public static async Task RunParallelExamples()
    {
        Console.WriteLine("\n=== TASK PARALLEL LIBRARY (TPL) ===");
        
        // Example 1: Process a list of items in parallel
        var items = new List<string> { "Apple", "Banana", "Cherry", "Date", "Elderberry" };
        
        Console.WriteLine("Processing fruits (one by one - SLOW):");
        foreach (var item in items)
        {
            await ProcessItemAsync(item); // Slow: waits for each
        }
        
        Console.WriteLine("\nProcessing fruits (all at once - FAST):");
        var tasks = items.Select(item => ProcessItemAsync(item));
        await Task.WhenAll(tasks); 
        
        // Example 2: Parallel.ForEach 
        Console.WriteLine("\nUsing Parallel.ForEach:");
        Parallel.ForEach(items, item =>
        {
            Console.WriteLine($"Processing {item} on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000); // Simulate work
        });
        
        // Example 3: Running multiple different tasks
        Console.WriteLine("\nRunning different tasks simultaneously:");
        var downloadTask = DownloadFileAsync();
        var processTask = ProcessDataAsync();
        var sendEmailTask = SendEmailAsync();
        
        await Task.WhenAll(downloadTask, processTask, sendEmailTask);
        Console.WriteLine("All different tasks completed!");
    }
    
    static async Task ProcessItemAsync(string item)
    {
        await Task.Delay(500);
        Console.WriteLine($"  Processed: {item}");
    }
    
    static async Task DownloadFileAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("   File downloaded");
    }
    
    static async Task ProcessDataAsync()
    {
        await Task.Delay(1500);
        Console.WriteLine("   Data processed");
    }
    
    static async Task SendEmailAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("  Email sent");
    }
}


   }


   

