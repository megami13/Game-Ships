<?php
$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbName = "ships";

//Variable from the user
$username = $_POST["usernamePost"];//"Lucas Test AC";
$password = $_POST["passwordPost"]; //"123456";

//Make Connection
$conn = new mysqli($servername, $server_username, $server_password, $dbName);
//Check Connection
if(!$conn) {
	die("Connection Failed. ".mysqli_connect_error());
}

$sql = "SELECT username FROM users WHERE username = '".$username."' ";
$result = mysqli_query($conn, $sql);

if(mysqli_num_rows($result) > 0) {
	echo "Account already exists";
	
} else {
	$sql = "INSERT INTO users (username, password) VALUES ('".$username."','".$password."')";
	$result = mysqli_query($conn, $sql);
	echo "Account created";
}

//$sql = "INSERT INTO users (username, password) VALUES ('".$username."','".$password."')";
//$result = mysqli_query($conn, $sql);

//if(!result) echo "there was en error";
//else echo "Everything ok";

?>