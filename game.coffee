## Our game

game = 
  drawMap: ->
    map = $("#map").text().toString()
    lines = for line in map.split('\n') when line.length > 0
               cols = for c in line
                        cssClass = game.mapChar(c)
                        "  <td class=\"col #{cssClass}\"></td>"
               "<tr>#{cols.join(' ')}</tr>"
    rows = lines.join(" ")
    table = "<table class='game-table' border='1'>#{rows}</table>"
    $("#output").html(table)
  mapChar: (c) ->
    switch c
      when "X" then "barrier"
      when "." then "dot"
      when "*" then "super-dot"
      when "R" then "robot"

$ -> game.drawMap()