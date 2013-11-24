lambit
======

Functional Library for C#. Bring some of that sexy stuff from Haskell, F#, Clojure and Scala into your *bosses* favorite language!

---

#Pattern Matching and Destructuring

We are able to do destructuring matches in C# by using 'match' values, which can be composed together to form a kind of ad-hoc pattern match expression.

Examples for lists:
```csharp
var myArray = new {1, 2, 3};

var matched = myArray
    .Match() // begin the match expression
    .Case((a, b) => a + b) //here we match a list with exactly 2 elements.
    //here we match using a cons pattern, in this case, a list with at 
    //least 3 elements, the rest of the list will be passed as the argument 'rest', as an IEnumerable.
    //This pattern is the one that will be hit.
    .CaseCons((a, b, c, rest) => (a * b * c) + rest.Sum())  
    .CaseElse(() => 0); //by providing an alternative, we are able to resolve our pattern match.
     
```

#Monads

I am trying to stick to monads that are useful in C# land. So far **Maybe** and **Either** are the obvious choices. LINQ'd up they can be awesome tools in C#.

N.B because of the type system in play, there is no reified notion of a Monad, or Monad instance. On the other hand, we get efficient short-circuiting **FoldM** etc.

###Maybe

```csharp
//maybe monad
var dirty = new
{
  Two = "2",
  Fourty = "40"
};

/// Result will be a Maybe<int> containing the value '42'
var fourtyTwo = from two in Parse.Int(dirty.Two)
                from fourty in Parse.Int(dirty.Fourty)
                select fourty + two;
                
```

###Either

```csharp
//either monad
var dirty = new
{
  Two = "2",
  Fourty = "I AM NOT 40"
};

/// Result will be a Either<int, string> containing the 'Does I AM NOT 40 look like 40 to you?'
var fourtyTwo = from two in Parse.Int(dirty.Two).OrValue("Uh... this ain't two")
                from fourty in Either.Maybe(dirty.Fourty, Parse.Int, (n) => string.Format("Does {0} look like 40 to you?", n))
                select fourty + two;
                
```
