# F# for C# Developers Tweetcast!

This screencast series is inspired from Jorge Fioranelli's full-day 
workshop.  If you have the chance to experience the workshop, it's a 
great opportunity to play with the F# language in a hands-on way.

Like the workshop, these screencasts are best experienced hands-on.
That means these exercises are meant to be followed.  The idea is
your fingers will remember what your mind forgets.

So let's get cracking!

# Outline

* Bindings | Functions | Tuples | Records 
* Higher ordered functions | Pipelining | Partial Application | Composition
* Options | Pattern Matching | Discriminated Unions | Units of Measure
* Functional Lists | Object-oriented Programming | Type Providers

# The Basics

In this tutorial, we're going to teach you F# by starting with C# examples, translating those examples, and then showing you how you can refactor your code to a more idiomatic F# and functional C# style.

## Variables and Bindings

In C#, we typically start with declaring variables, giving them values.

```csharp
//  We can declare a variable and set it's value in one line.
string myName = "Steve";

//  We might change our variable's value at some point later in the function.
myName = "Stefcho";

//  We can type inferencing by using the var keyword so we don't have to
//  provide the variables type.  The compiler figures it out for us.
var x = 5;
```

There are a few differences with F#.  

```fsharp
//  If we're ever going to reassign a
//  variable's value, we need to mark it as
//  ***mutable***
let mutable myName : string = "Steve"

//  To reassign it, we need to use the <- operator
myName <- "Stefcho"

//  F# also does type inferencing, only better.
let x = 5
```