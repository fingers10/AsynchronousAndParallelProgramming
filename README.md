TPL is used to build multi threaded apps.

If we have large set of data and each can be processed individually as sub modules then TPL is the best way to go.

We can use task from TPL to run code in different context but that doesnot effectively use all core in cpu or all available threads asynchornous programming is different from parallel programming.

Asynchronous programming will return a continuation while parallel programming computes the problem in parallel.

We can achieve the same using thread or tpl in .net. Thread class gives low level control over thread. But with more control comes the more responsibility. We need to write lot of code on our own with thread class but with tpl we have an abstraction to code easily. With Task we can leverage the framework to take care of thread management and we dont need to worry on those. Parallel (TPL) will take care of using the existing resources effectively and managing them using Invoke, For, Foreach. TPL aslo supports PLINQ.

Show the logical cores available in task manager.

Parallel will ensure that work is distributed effectively on the machine in which application runs.

By default calling these parallel methods will consume as much as computer power as possible.

Manually changing the max degree of parallelism will not make best use of resources.

Misusing parallel in asp.net can cause bad performance to all users.

Executing Parallel will again block the main thread. We now know asynchronous programming and what if we can run this in a separate context than the main context? Yes we can wrap the Parallel operation inside a Task.Run and we can await. But this will take a thread to run separately. It's a tradeoff that we need to decide based on use case. For some operations users may be fine if it takes some time. For some they might not.

When an exception is thrown, it we thrown as aggregate exception. Exception thrown in one operation will not stop another operation (i.e.) even if one parallel operation fails then other scheduled operation will still start. Exception will be thrown only after all the operations are completed.

PLINQ - when we need to work on vast huge amount of data in a efficient parallel manner, we can go with PLINQ. PLINQ is parallel implementation of LINQ. PLINQ will work on both method and query syntax. PLINQ will parallelize your LINQ to speed up the execution. This will result in faster execution. PLINQ will perform an internal analysis on query to determine if its bestsuitable for parallelization.

We can simply call .AsParallel() on IEnumerable to make it a ParallelQuery. Don't overuse AsParallel() as it adds overhead like analyzing the query. It will analyze and find if the code runs faster in sequential execution then it will not run in parallel and also if the code is unsafe to run in parallel then it will not run in parallel. We can force this with .WithParallelExecutionMode(ParallelExecutionMode.ForceParallelism). However leaving these to default will automatically give the best possible result.

Dont assume that adding AsParallel, queries will always make run faster. Performance improvement will sometimes be seen on large collections.
