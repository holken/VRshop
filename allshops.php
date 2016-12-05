<?php
	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "vrshop";
	$conn = new mysqli($servername, $username, $password, $dbName);
	$stmt = $conn->prepare("SELECT shopname FROM usershops");
	$stmt->execute();
	
	$stmt->store_result();
	$stmt->bind_result($shopname);
	
	while($stmt->fetch()){
		echo $shopname."||";
	}


?>