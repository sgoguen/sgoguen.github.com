open System

type Gender = Male | Female | Other of String

type Person(name:string,DOB:DateTime) =
  let age = (DateTime.Now - DOB).Days / 365
  member this.Name = name
  member this.DOB = DOB
  member this.Age = age
  member this.DrinkingAge(country) =
    if country = "USA" then age >= 21
    elif country = "Canada" then age >= 19
    else age >= 18

let Jim = Person("Jim", DateTime(1971, 1, 1))
Jim.DrinkingAge("USA")  //  He's good
let Bobby = Person("Bobby 'Drop tables", DateTime(1993, 1, 1))
Bobby.DrinkingAge("France")
Bobby.DrinkingAge("Canada")
Bobby.DrinkingAge("USA")

