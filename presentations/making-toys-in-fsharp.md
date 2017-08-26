# Making Toys in F#

* This talk is about creating toys to explore ideas and spark discussion
* Not everything is recommended
* I don't advocate all the methods or code in this talk
  * Some ideas intentionally simplified ideas
  * Some may introduce attack vectors
* I'm not an expert.
* Despite all this:  Let's talk about toys and ideas!

# Agenda

* 15 Minutes - Why Make Toys?
* 15 Minutes – Introduction to LINQPad
* 15 Minutes - Simple Printing - Dump
* 15 Minutes - Navigating Objects
* 15 Minutes - LINQPad’s Advanced Features
* 15 Minutes - The Big Picture

# Why Make Toys?

* We want to share ideas and spark discussions
* Toys can communicate ideas that are difficult to explain with words
* Toys are fun!
* Toys allow us to safely experiment
* Toys can make difficult concepts easy to understand
* People tend not to put things labeled “toys” into production
* They simulate and real / professional things
* They remind us not to be too serious!

# Let's Drop The Slides and Talk

* The point of this session is to have a discussion, not just for me to lecture.
* Please interrupt me and ask questions
* I have a planed, but loose agenda - Our conversation is more important
* I want to hear your feedback – 
  * Feel free to write comments in this Google Document while we talks
* I want to continue to have this conversation after the session.
  * Ping me:  @sgoguen
  
# LINQPad is my Favorite Toy - My Rosebud

* It’s an amazing snippet compiler and script runner with some incredible features
* Best features:
  * It facilitates a fast development cycle because snippets run instantly
  * Feedback is rich and explorable
* I use it to prototype ideas, create toys, and create scripts
* I use it to explain ideas both offline and online (@FSharpCasts)
* It makes coding less painful and fun

# I Want to Share Toys I've Created

* I’ve built some toys that let us do things similar to LINQPad
* They’re unfinished
* I want to show you how you can build them
* I want to encourage you to build toys that you can share with other people without worrying about:
  * If it’s good enough
  * Putting it into production
  * Caring what people think about it
* I want us to focus on programming and collaborating for the fun of it


------------------------------------------------------


# I Want to Show You LINQPad

* Q: Who has used LINQPad?
* 7 min - The Basics the LINQPad User Interface
 * Exploring a Table 
 * Q: How do you explore an unknown database?
 
* 5 - Watching a Process
 * 2 min - Console.WriteLine
 * Observing Processes with Observables
 * Q:  How do you monitor your process code?
 * Q:  How rich is the feedback
 
* Toy GUIs by Dumping Objects
 * A Cheap File Explorer
 * Making it More Interactive
 * Q: Who prototypes interfaces?  How would you ideally do it?
 * Prototype a Dashboard
  * Custom Printing
  
* Other Features
 * Lazy Objects
 * Async Objects
 * TPL-DataFlows
 * Panels
 * Embedding Controls
 * Q: What Custom Views Would You Like to See?
 
# How to Make Your Own Object Dumper
* Minimal Printers Help Understand Problem Space
 * Objects as Key/Value Pairs
 * Q:  Who creates these sorts of tools?
 * Q:  What would be useful for you?
* Building a Safe Foundation for HTML Printers
 * An Elm like DSL for F#
 * We're going to use this for our own object dumper
* How to Create Your Own Smarter Recursive Dump
 * Making a Printer Extensible
  * Look for an interface
  * Maybe Duck Typing
  * How about a rendering function marked with an attribute?
  * Q:  How would you make it extensible for new patterns?
 * Active Patterns on Types are Powerful
  * Let's look at some patterns
 * Making Printers Extensible with Dynamic Pattern Matching
  * Put Pattern Matchers in a List - Print the First that Says it Can
* Customizable
* Anytime You See This → Print it This Way

# Navigating Objects
* Allowing Your End Users to Navigate Your .NET Objects
 * Dynamically Mapping URLs to .NET Objects
 * Let's hook it up to a web server!!!!
 * Q:  What are the security implications?
* Let's Explore All Possible Path with an Explorer Interface
* Ways we can navigate URLS
 * An OO Scheme - Properties, Indexers, etc.
 * A More Function First Approach
  * Every URL represents an object of some type
  * Resolver:  Type → Path → Result
  * We use reflection to generate a catalog of functions at start-time
   * This is slightly safer than dynamic routing because
   * We can audit a list of possible URLs
   * We can even audit it with our Lazy Tree
 * Safety
  * We can use reflection to filter our functions we never want to be mapped
* Toy Enterprise App
 * Tiny Data Access Layer
 * Tiny Business Layer
 
# LINQPad’s Advanced Features
* Printing Observables
 * Any Enumerable can become an Observable
* Util.OnDemand
* Panels
* Monitoring TPL DataFlows - (monitoring-tpl-dataflow example)
* Embedding Controls
* Custom Printers .ToDump()
* Dump Containers - Allow you to imperatively update the UI


# The Big Picture
* Wouldn’t you like to see a world where…
 * We don’t simply write code to create systems, but to explain and reify ideas so people can play with them?
 * We can spend more time at work making toys that let us experiment with 10x ideas.
 * Work with tools that multiply a team’s diverse set of perspectives because they a kind of “type checking” for ideas?
* Wouldn’t you like to see this shitty thing changed?
 * Tests made more fun and relevant to production systems
 * Miscommunication of requirements for the team (Autogen UI with code)
 * Documentation that automatically updates because it’s interwoven in the implementation.
* Wouldn’t you like a more fun way to write tests?
* Wouldn’t you like a clearer way to debug someone else’s system?
* Wouldn’t you like a clearer way for your business analyst, project manager, or tester to understand what you do?
* Wouldn't you like a system that helps you prevent management from driving your software into the ground?
* Avoid making improvements that don't end up making much of a difference.
* “How can we sustain human understanding as complexity grows and the team changes?”


