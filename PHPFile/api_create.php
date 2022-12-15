<?php

header("Content-Type: application/json");
header("Acess-Control-Allow-Origin: *");
header("Acess-Control-Allow-Methods: POST");
// header("Acess-Control-Allow-Headers: Acess-Control-Allow-Headers,Content-Type, 
// Acess-Control-Allow-Methods, Authorization");


$data = json_decode(file_get_contents("php://input"), true);

$pname = $data["username"];
$pprice = $data["email"];

include('servercon.php');
$query = "INSERT INTO users(username, email) 
                       VALUES ('".$pname."', '".$pprice."')";

if(mysqli_query($dbconnect, $query) or die("Insert Query Failed"))
{
	echo json_encode(array("message" => "Product Inserted Successfully", "status" => true));	
}
else
{
	echo json_encode(array("message" => "Failed Product Not Inserted ", "status" => false));	
}

?>