<?php
	//http://localhost/vrshop/allpaymentoptions.php?storeName=Dayscene
	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "vrshop";
	$conn = new mysqli($servername, $username, $password, $dbName);
	
	function parseQueryString(){
		$urlExt = $_SERVER['QUERY_STRING']; 
				
		parse_str($urlExt, $get_array);
				
		return $get_array;
	}
			
	$parameters = parseQueryString();
	$storeName = $parameters['storeName'];
	
	$stmt = $conn->prepare("SELECT paymentname FROM paymentoptions WHERE storename = ?");
	$stmt->bind_param("s", $storeName);
	$stmt->execute();
	$stmt->store_result();
	$stmt->bind_result($paymentName);
	
	while($stmt->fetch()){
		echo $paymentName."||";
	}
	
	/*$stmt = $conn->prepare("SELECT shopname FROM usershops");
	$stmt->execute();
	
	$stmt->store_result();
	$stmt->bind_result($shopname);
	
	while($stmt->fetch()){
		echo $shopname."||";
	}
*/

?>