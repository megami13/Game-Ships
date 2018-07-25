<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbName = "Ships";

$user_username = $_POST["usernamePost"];
$user_password = $_POST["passwordPost"];

//Make Connection
$conn = new mysqli($servername, $username, $password, $dbName);
//Check Connection
if(!$conn) {
	die("Connection Failed. ".mysqli_connect_error());
}

$sql = "SELECT password FROM users WHERE username = '".$user_username."' ";
$result = mysqli_query($conn, $sql);

//Get the result and login
if(mysqli_num_rows($result) > 0) {
	//show data for each row
	While($row = mysqli_fetch_assoc($result)) {
		if($row['password'] == $user_password) {
			echo "login success";
		} else {
			echo "password incorrect";
		}
	}
} else {
	echo "user not found";
}

?>