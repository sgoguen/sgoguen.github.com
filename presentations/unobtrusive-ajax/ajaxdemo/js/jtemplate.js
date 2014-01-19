/* prevent execution of jQuery if included more then once */
if(typeof window.jtemplate == "undefined") {

/*
  bindMany - Binds multiple event handlers to multiple selectors.
             Useful when defining components.  
             
  // Example
  $(document).bindMany({
  
    //  By default, we binds the onclick event.
    ".hidable": function() {
      $(this).hide();
    },
    
    //  Handle the click, dblclick, and blur events for the "multiple_event" class
    ".multiple_event": {
      click: function() {
        alert("Click");
      },
      dblclick: function() {
        alert("Double Click");
      },
      blur: function() {
        alert("Leaving Focus");
      }            
    }
  }, "click");  //  Set our default_event here
*/
$.fn.bindMany = function(event_handlers, default_event) {
  default_event = default_event || "click";

  for(selector in event_handlers) {
    events = event_handlers[selector];
    if(typeof events == 'function') {          
      $(this).find(selector).bind(default_event, events);
    } else if(typeof events == 'object') {
      for(event_name in events) {
        event_handler = events[event_name];
        $(this).find(selector).bind(event_name, event_handler);
      }
    }
  }  
}

$.fn.defineTemplate = function(template, event_handlers, default_event) {
  //  Create a templates object where we store all our templates...
  if(!$.templates) $.templates = {};
  //  Clone our currently selected node, adding the event handlers in the "bindings" object
  var node = $(this).clone();
  $(this).bindMany(event_handlers, default_event);
  $.templates[template] = {
    name: template, 
    node: node, 
    event_handlers: event_handlers, 
    default_event: default_event
  };
}

$.fn.applyTemplate = function(template) {
  if(typeof template == 'string') template = $.templates[template];
  $(this).bindMany(template.event_handlers, template.default_event);
}

$.fn.applyValues = function(parameters) {
  var key;
  for(key in parameters) {
    var val = parameters[key];
    var p = $(this).children("#" + key);
    debugger
    p.html(val).val(val);
  }
}

$.fn.appendTemplate = function(template, parameters) {
  if(typeof template == 'string') template = $.templates[template];
  var node = $(template.node).clone();
  $(this).append(node);
  $(node).bindMany(template.event_handlers, template.default_event);
  if(parameters) {
    $(node).applyValues(parameters);
  }
  return node;
}

$.fn.afterTemplate = function(template) {
  if(typeof template == 'string') template = $.templates[template];
  var node = $(template.node).clone();
  $(this).after(node);
  $(node).bindMany(template.event_handlers, template.default_event);  
  return node;
}

//  TODO: Rename cloneWithBindings
$.fn.cloneWithBindings = function(event_handlers, default_event) {
  var result = $(this).clone();
  result.bindMany(event_handlers, default_event);
  return result;
}

$.fn.appendIfNotFound = function(f, element) {
  if($(this).find(f).length == 0)
    $(this).append(element);
  var element = $(this).find(f);
  return element;
}

$.fn.styleString = function() {
  var node = $(this).get();
  debugger
}

function debug(category, message) {
  var table = $("#debug_console").appendIfNotFound("#debug_table", "<table id='debug_table' border='1'></table>");
  var row = table.append("<tr><td>" + category + "</td><td>" + message + "</td></tr>");
  row.text(message);
}
  
}

