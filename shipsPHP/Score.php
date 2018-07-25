<?php
$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbName = "ships";

//Variable from the user
$username = $_POST["usernamePost"];
$score = $_POST["scorePost"];

//Make Connection
$conn = new mysqli($servername, $server_username, $server_password, $dbName);
//Check Connection
if(!$conn) {
	die("Connection Failed. ".mysqli_connect_error());
}

$sql = "UPDATE scoretable SET score = '".$score."' WHERE username = '".$username."' ";
$result = mysqli_query($conn, $sql);

if(!result) echo "there was en error";
else echo "Everything ok";

?>