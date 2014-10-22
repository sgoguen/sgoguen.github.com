open System

let MinMax(min,max) value =
    if value > max then max
    elif value < min then min
    else value

type TV() =
  let mutable currentChannel = 3
  let mutable volume = 10
  member this.Channel = currentChannel
  member this.Volume = volume
  member this.ChangeChannel(newChannel) =
    currentChannel <- newChannel |> MinMax(2, 99)
    printfn "Channel: %i" currentChannel
  member this.ChangeVolume(newVolume) =
    volume <- newVolume |> MinMax(1,11)
    printfn "Volume: %i" volume

type Command = unit -> unit
type UndoableCommand = unit -> Command


type IRemote =
  abstract member ChannelUp: Command
  abstract member ChannelDown: Command
  abstract member VolumeUp: Command
  abstract member VolumeDown: Command
  abstract member Undo: Command

type TVRemote(tv:TV) =
  let mutable commandLog = List<Command>.Empty
  let ExecuteUndoable (cmd:UndoableCommand) =
    fun () -> 
      let undo = cmd()
      commandLog <- undo :: commandLog
  let channelUp = fun () ->
                    let lastChannel = tv.Channel
                    tv.ChangeChannel(tv.Channel + 1)
                    fun () -> tv.ChangeChannel(lastChannel)
  let channelDown = fun () ->
                      let lastChannel = tv.Channel
                      tv.ChangeChannel(tv.Channel - 1)
                      fun () -> tv.ChangeChannel(lastChannel)
  let volumeUp = fun () ->
                    let lastVolume = tv.Volume
                    tv.ChangeVolume(tv.Volume + 1)
                    fun () -> tv.ChangeVolume(lastVolume)
  let volumeDown = fun () ->
                    let lastVolume = tv.Volume
                    tv.ChangeVolume(tv.Volume - 1)
                    fun () -> tv.ChangeVolume(lastVolume)
  interface IRemote with
    member this.ChannelUp = channelUp |> ExecuteUndoable
    member this.ChannelDown = channelDown |> ExecuteUndoable
    member this.VolumeUp = volumeUp |> ExecuteUndoable
    member this.VolumeDown = volumeDown |> ExecuteUndoable
    member this.Undo =
      fun () ->
        if commandLog.IsEmpty = false then
          let cmd = commandLog.Head
          commandLog <- commandLog.Tail
          cmd()

let myTV = TV()
let myRemote = TVRemote(myTV) :> IRemote

myRemote.ChannelUp()
myRemote.ChannelUp()
myRemote.ChannelUp()
myRemote.ChannelDown()
myRemote.ChannelDown()
myRemote.ChannelUp()
myRemote.ChannelUp()
myRemote.VolumeDown()
myRemote.VolumeDown()
myRemote.VolumeDown()

//  Let's undo it all
myRemote.Undo()