<?php

  require "shared.php";
 
  class TaskController extends Controller {
    var $tasks;  //  OUR MODEL
    
    function TaskController() {
      //  CREATE THE MODEL...
      $this->tasks = new TaskModel;
    }
    
    function index() {
      //  RETRIEVE ALL TASKS
      $tasks = $this->tasks->retrieve_all();
      //  ASSIGN IT FOR OUTPUT
      $this->assign("tasks", $tasks);
      //  DISPLAY TEMPLATE
      $this->display("index.phtml");
    }
       
    function add_task() {
      //  GET THE TASK FROM THE SUBMITTED FORM
      $task = array(
        'description' => $_REQUEST['description'],
        'assigned_to' => $_REQUEST['assigned_to'],
        'priority' => $_REQUEST['priority']
      );
      
      //  TELL THE MODEL TO UPDATE THE TASK
      $this->tasks->add_task($task);
      
      if($_REQUEST['f'] == 'snippet') {
        $this->assign("task", $task);
        $this->display("task-item.phtml");
      } else {
        $this->index();
      }
    }
    
    function remove_task() {
      $task = array(
        'task_id' => $_REQUEST['task_id']
      );
      $this->tasks->delete_task($task);
      if($_REQUEST['f'] == 'json') {
        echo "{ removed: true, task_id: {$task['task_id']} }";
      } else {
        $this->index();
      }
    }
    
  }
  
  class TaskModel extends MySQLModel {
    function TaskModel() {
      $this->connect("localhost", "root", "");
      $this->select_database("task", true);
      $this->create_table();
    }
    
    function retrieve_all() {
      $tasks = $this->query_to_array("SELECT * FROM task ORDER BY task_id");
      return $tasks;
    }
    
    function add_task(&$task) {
      $sql = <<<EOT
        INSERT INTO task (description, priority, assigned_to)
        VALUES ('{$task['description']}', '{$task['priority']}', '{$task['assigned_to']}');
EOT;
      //check($sql);
      $this->query($sql);
      $task['task_id'] = mysql_insert_id();
    }
    
    function delete_task($task) {
      $sql = "DELETE FROM task WHERE task_id = '{$task['task_id']}'";
      $this->query($sql);
    }
        
    function create_table() {
      $sql = <<<EOT
        CREATE TABLE IF NOT EXISTS task (
          task_id int auto_increment,
          description varchar(500),
          priority varchar(50),
          assigned_to varchar(50),
          PRIMARY KEY ( task_id)
        );
EOT;
      $this->query($sql);
    }
    
  }
  
  
  auto_dispatch("TaskController");

?>