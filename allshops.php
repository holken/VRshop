<?php
	$servername = "localhost";
	$username = "fiskeapp";
	$password = "D&5XQJp_{!U5";
	$dbName = "fiskeapp_wrdp1";
	$conn = new mysqli($servername, $username, $password, $dbName);
	$stmt = $conn->prepare("SELECT shopname FROM usershops");
	$stmt->execute();
	
	$stmt->store_result();
	$stmt->bind_result($shopname);
	
	while($stmt->fetch()){
		echo $shopname."||";
	}


?>