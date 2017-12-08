using System;
using System.IO;

namespace todoFinder
{
    class Program
    {
        //  This is a little program that searches the folder looking for TODOs
        //  in comments.
        static void Main(string[] args)
        {
            //  First, we get the patch we want to search from the command-line
            //  arguments
            string path = args[0] ?? "./";

            //  Then we create a variable that we're going to use to track the
            //  number of TODOs we find.
            int todoCount = 0;

            //  We're going to capture all the files in the folder
            foreach (var file in System.IO.Directory.EnumerateFiles(path)) {

                //  We'll announce each file we're going to search
                Console.WriteLine($"Searching file: {file}");

                //  Then we'll loop over the lines
                foreach(var line in File.ReadAllLines(file)) {

                    //  If the line contains our, TODO
                    if(line.Contains("TODO:")) {
                        
                        //  Increment the counter
                        todoCount += 1;

                        //  And print it...
                        Console.WriteLine($"Found {file}: {line}");
                    }
                }
            }

            //  When we're finished, we say we're done and tell the user
            //  how many lines we found.
            Console.WriteLine($"Done - Found {todoCount}!");

            //  Now...  Let's convert this into F#!!!
        }
    }
}
