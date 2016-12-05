<?php
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
        $db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
        mysql_select_db($dbName) or die('Could not select database');
 
        // Strings must be escaped to prevent SQL injection attack. 
        $shopName = mysql_real_escape_string($_GET['shopname'], $db); 
        $uploadusername = mysql_real_escape_string($_GET['uploadusername'], $db); 
		$shoptype = mysql_real_escape_string($_GET['shoptype'], $db); 
		$spawnpoint = mysql_real_escape_string($_GET['spawnpoint'], $db); 
		$prodid = mysql_real_escape_string($_GET['prodid'], $db); 
		$prodname = mysql_real_escape_string($_GET['prodname'], $db); 
		$proddesc = mysql_real_escape_string($_GET['proddesc'], $db); 
		$prodquant = mysql_real_escape_string($_GET['prodquant'], $db);
		$prodcost = mysql_real_escape_string($_GET['prodcost'], $db);
        $hash = $_GET['hash']; 
 
        $secretKey="mySecretKey"; # Change this value to match the value stored in the client javascript below 

        $real_hash = md5($shopName . $uploadusername . $shoptype . $spawnpoint . $prodid . $prodname . $proddesc . $prodquant . $prodcost . $secretKey); 
        if($real_hash == $hash) { 
            // Send variables for the MySQL database class. 
            $query = "insert into shopitems values (NULL, '$shopName', '$uploadusername', '$shoptype', '$spawnpoint', '$prodid', '$prodname', '$proddesc', '$prodquant', '$prodcost', '$secretKey');"; 
            $result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
        } 

/*
	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "vrshop";
	
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if (!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	
	$sql = "SElECT * FROM shopitems";
	$result = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($result) > 0){
		while($row = mysqli_fetch_assoc($result)){
		echo "shopname:".$row['shopname']."|uploadusername:".$row['uploadusername']."|shoptype:".$row['shoptype']."|spawnpoint:".$row['spawnpoint']."|prodid:".$row['prodid']."|prodname:".$row['prodname']."|proddesc:".$row['proddesc']."|prodquant:".$row['prodquant']."|prodcost:".$row['prodcost']."|productpic:".$row['productpic'].";";
		}
	}
	*/
?>