(*** hide ***)
open System

(** 
Hello Mr. Object, do you support this interface?
================================================

Through the 90's, I was a C++ programmer who enjoyed C++ flavored Kool-Aid.  In the 
mid-90's, a coworker introduced me to this thing called 
[COM](http://en.wikipedia.org/wiki/Component_Object_Model).  COM was a standard for 
creating reusable components that just happened to be binary compatible with C++ vtables.
I was esctatic.

IUnknown - One Interface to Rule them All
-----------------------------------------

The fundamental building block in COM was the IUnknown interface.  IUnknown did two things:

1. It required objects return the interfaces it supported at run-time.
2. It provided an interface for [reference counting](http://en.wikipedia.org/wiki/Reference_counting).

If we were to translate IUnknown to C#, it might look something like this:

    [lang=csharp]
    interface IUnknown {
      //  The essence of IUnknown
      bool QueryInterface<T>(Guid interfaceId, out T returnedInterface);
      //  These two methods are used for reference counting
      ulong AddRef();
      ulong Release();
    }


Here's the basic summary of the interface:

* QueryInterface allows the person to ask the object if they support an interface (identified by 
a GUID).  If it's successful, it returns true and gives you back the pointer to the interface in 
the out parameter.  Otherwise we return false.
* AddRef was used for [reference counting](http://en.wikipedia.org/wiki/Reference_counting).  When you acquired
a reference to another object, you'd call AddRef to tell it somebody was using it.
* When a object was finished using another object it would call Release.  The idea was, if an 
object was no longer being used by anybody, it should delete itself and free its own resources.

What using an IUnknown interface looked like
---------------------------------------------

    [lang=csharp]
    IUnknown hello = CreateHelloWriter();
    IHelloWriter writer = null;
    //  Ask this object if it supports the IHelloWriter interface
    if(hello.QueryInterface(IHelloWriterGUID, out writer)) {
      //  Tell the object we are using it
      writer.AddRef();
      //  Actually use it
      writer.SayHello("World");
      //  Tell the object we are no longer using it
      writer.Release();
    } else {
      //  I don't know what to do.  Let's crash
      throw new Exception("We failed...");
    }


Can you believe I was actually excited about this standard?
-----------------------------------------------------------

Naturally, there were tools and C++ template libraries that cut down on the boilerplate
code, but its not like that didn't come with its own problems.

When .NET came around and offered up a much simpler solution with the **as** keyword:

    [lang=csharp]
    IUnknown hello = CreateHelloWriter();
  
    var writer = hello as IHelloWriter;
    if(hello != null) {
      writer.SayHello("World");
    } else {
      throw new Exception("We failed again...");
    }

The biggest difference between the COM standard and the .NET method for querying interfaces
is the .NET method is static.  Static is good.  Static is predictable.  Static tends to be 
faster because the compiler has static information it can use to make decisions.  Static 
also tends to be safer.


     

### Step 1 - Remove the reference counting methods

When we moved from the COM world to the .NET world, we dropped reference counting in 
exchange for garbage collection.  So if we were to simplify this interface, we could reduce 
it to this:

    [lang=csharp]
    interface IUnknown {
      //  The essence of IQueryable
      int QueryInterface<T>(Guid interfaceId, out T returnedInterface);
    }

### Step 2 - Translate to idiomatic F#

The first thing we need to do is get rid of the out parameter.

    type IUknown = 
      abstract member QueryInterface : Guid -> 't option



*)


type numberInfo = { x: int; dbl: int; sqr: int }
let list = [1..10]
           |> List.map(fun x -> { x = x; dbl = x; sqr = x })

(*** include-value: list ***)


(**
  That was the basic idea.
*)