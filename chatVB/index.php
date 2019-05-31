<?php

if(isset($_POST['user']) && isset($_POST['message']) && isset($_POST['date'])){
	$user = $_POST['user'];
	$message = $_POST['message'];
	$date = $_POST['date'];
	
	$json = file_get_contents("chat.json");
	$json_decoded = json_decode($json, true);
	array_push($json_decoded["data"], array("user" => $user, "message"=>$message, "date"=>$date));
	file_put_contents("chat.json", json_encode($json_decoded) );
	
}