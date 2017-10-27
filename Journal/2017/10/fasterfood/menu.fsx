type Table = Table of string[,]

#if HAS_FSI_ADDHTMLPRINTER
fsi.AddHtmlPrinter(fun (Table t) ->
  let body = 
    [ yield "<table>"
      for i in 0 .. t.GetLength(0)-1 do
        yield "<tr>"
        for j in 0 .. t.GetLength(1)-1 do
          yield "<td>" + t.[i,j] + "</td>" 
        yield "</tr>"
      yield "</table>" ] 
    |> String.concat ""
  seq [ "style", "<style>table { background:#f0f0f0; }</style>" ],
  body )
#endif

let table = 
  [ [ "Test"; "More"]
    [ "1234"; "5678"] ]
  |> array2D |> Table  



// The idea is simple, similar to Chipotle, customers 
// build their own meals by selecting what ingredients
// at different stations.

// Here are our steps:

let steps = [
    ("Base", ["Salad"; "Grain bowl"; "Pita"])
    ("Protein", ["Chicken";"Lamb"; "Roasted Vegetables"])
    ("Dips + Spread",  ["Hummus"; "Harissa"; "Baba Ganoush"])
    ("Toppings", ["Picked Onions"; "Olives"; "Feta"])
    ("Dressing", ["Yogurt Dill"; "Herb Tahini"; "Vinaigrette"])
]

open System

type Customer = string
type Ingredient = string
type Order = {
    Customer: Customer
    Base: Ingredient
    Protein: Ingredient
    Dips: Ingredient list
    Toppings: Ingredient list
    Dressing: Ingredient list
}

let sampleOrder = {
    Customer = "Lester"
    Base = "Grain Bowl"
    Protein = "Lamb"
    Dips = ["Harissa"]
    Toppings = ["Olives"; "Feta"]
    Dressing = ["Herb Tahini"]
}

type UserName = string
type Password = string

type UserApp = {
  PlaceOrder: Order -> OrderResult
  OrderHistory: Order list
  NotificationHistory: Notification list
  Notifications: Notification Event
}
and OrderResult = { 
  OrderNo: string
  PickupTime: DateTime
}
and Notification = { 
  Text: string
  Sent: DateTime
}

type Money = decimal
type CouponType = FreeMeal | FreeSide

type PaymentOption = 
  | Cash of Amount:Money
  | Coupon of CouponType
  | Credit of Amount:Money * CardNumber:string * Expiration:string
  | GiftCard of Amount:Money * Balance:Money * CardNumber:Guid

type Payment = Payment of PaymentOption list

let examplePayment = 
  Payment [
    Cash(20.0M)
    Coupon(FreeSide)
    Credit(10.0M, "1234-4556-8969-3455", "11/19")
    GiftCard(5.50M, 20.M, Guid.NewGuid())
  ]
  

let isValidPaymentOption(paymentType) = 
  match paymentType with
  | Cash(amount) when amount <= 0.0M -> false
  | Credit(amount, _, _) when amount <= 0.0M -> false
  | GiftCard(amount, _, _) when amount <= 0.0M -> false
  | GiftCard(amount, balance, _) when amount > balance -> false
  | _ -> true


//type Employee = { Id: int; Name: string }
type Employee = string
type PrepStation = int

type OrderEvent = 
    | OrderPlaced of Order
    | EnteredPrepStation of Employee * PrepStation
    | LeftPrepStation of Employee * PrepStation
    | InspectedBy of Employee
    | Paid of Payment
    | CustomerFeedback of Stars:int

type JournalEntry = {
    Timestamp: DateTime
    Event: OrderEvent
}

type Journal = Journal of OrderEvent list

let lestersOrder = 
  Journal [
    OrderPlaced(sampleOrder)
    EnteredPrepStation("Joe", 1)
    LeftPrepStation("Joe", 1)
    InspectedBy("Luz")
    Paid(Payment [ Cash(7.5M) ])
    CustomerFeedback(5)
  ]



  