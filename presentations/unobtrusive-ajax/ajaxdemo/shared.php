<?php

  /* TEMPLATE FUNCTION */
  function php_template($file, $values) {
  	extract($values);
  	require($file); 
  }
  
  
  /* DEBUG FUNCTIONS */
  function debug($var) {
     echo "<pre>";
     print_r($var);
     echo "</pre>";
  }
  
  function check($var) {
    debug($var); die();
  }
  
  function default_val(&$variable, $value) {
    if(!isset($variable)) {
      $variable = $value;
    }
    return $variable;
  }

  /* CONTROLLER CLASS */
  class Controller {
    var $parameters = array();
    
    function assign($key, $value = null) {
       $this->parameters[$key] = $value;
    }
    
    function display($page) {
      php_template("./views/$page", $this->parameters);
    }  
  }
  
  class MySQLModel {
    var $link;

    function connect($host, $user, $password) {
      if(!$this->link = mysql_pconnect($host, $user, $password)) {
        die('Could not connect to mysql');
      }        
    }

    function query($sql) {
      $result = mysql_query($sql, $this->link);
      if (!$result) {
         echo "DB Error, could not query the database\n";
         echo 'MySQL Error: ' . mysql_error();
         exit;
      }
      return $result;    
    }
    
    function query_to_array($sql) {
      $result = $this->query($sql);
      $array = array();
      while($row = mysql_fetch_array($result))
        $array[] = $row;
      return $array;
    }
    
    function select_database($database, $autocreate = false) {
      if($autocreate)
        $this->query("CREATE DATABASE IF NOT EXISTS {$database};");
      mysql_select_db($database, $this->link);    
    }    
  }
  
  function auto_dispatch($controller) {
    $m = new $controller;
    $method = default_val($_REQUEST["m"], "index");
    $m->$method();  
  }

?>