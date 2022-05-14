using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETS
{
    // Assume the following two job classes are implemented and work
    public class JobInput
    {     
       public int data { get; set; }
    } 

    public class JobThread 
    { 
        // Adds a job that will call action 'complete' on a background 
        // thread within a thread pool.
        
        public static void AddJob(JobInput input, System.Action<JobInput> complete) 
        { 
            // Don't implement this, just assume it works
        } 
        public static void WaitAll() 
        { 
            // Don't implement this, just assume it works
        } 
    } 
    /*
    // Find the issues in the following code:
    public class MyClass 
    { 
        public void SomeJobs() 
        { 
            var errors = new List<string>(); 

            System.Threading.Mutex mutex = new System.Threading.Mutex(); 

            var input = new JobInput(); 

            string resultStr = ""; 

            for (int i = 0; i < 10; i++) 
            {
                input.data = i; 
                
                JobThread.AddJob(input, d => { 
                    string errorStr;
                    // The following call only accesses the input data passed to it..
                    // Not sure if you want me to implement this or not?
                    var result = SomethingInteresting(d, out errorStr);

                    if (result == null)
                    {
                        errors.Add(errorStr);
                    }

                    mutex.WaitOne(); 
                    resultStr += result.ToString(); 
                }); 

                mutex.ReleaseMutex(); 
                JobThread.WaitAll(); 
            } 
            Debug.Log(resultStr); 
        } 
    } // class MyClass 
    */
}

