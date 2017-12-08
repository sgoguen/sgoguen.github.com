open System.Collections.Concurrent
open System

type WorkQueue<'a>(upperBound:int) = 
    let collection = new BlockingCollection<'a>(upperBound)
    let rec dequeue() = 
        async {
            let mutable value = Unchecked.defaultof<'a>
            let found = collection.TryTake(&value, 100)
            if found then return value
            else return! dequeue()
        }
    member this.Enqueue item = collection.Add(item)
    member this.Dequeue() = dequeue()

let workQueue = WorkQueue<string * TimeSpan>(10)

let rec doWork(workerName: string) = 
    async {
        let! (jobName, timeSpan) = workQueue.Dequeue()
        if jobName <> "quit" then
            printfn "%s - Starting Job - %s" workerName jobName
            do! Async.Sleep(int(timeSpan.TotalMilliseconds))
            printfn "%s - Finished Job - %s" workerName jobName
            do! doWork(workerName)
        else
            printfn "All done!"
    }

doWork("Bub") |> Async.Start

let addWork(maxJobs) = 
    for x in 1..maxJobs do
        let time = 1.0 + (float(x % 3) * 0.3)
        workQueue.Enqueue("Work hard " + string(x), TimeSpan.FromSeconds(time))
    //workQueue.Enqueue("quit", TimeSpan.FromSeconds(0.0))

addWork(50)
doWork("Mary") |> Async.Start
doWork("Joe") |> Async.Start
doWork("Frank") |> Async.Start