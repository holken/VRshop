<?php

	//$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	//include_once($WebsiteRoot . '/wp-config.php');
	
	//if (is_user_logged_in()){
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
		$db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
		mysql_select_db($dbName) or die('Could not select database');
	
		$current_user = wp_get_current_user();
		$username = $current_user->user_login;
		
		if(isset($_POST['shopName']){
			
			$stmt = db->prepare("INSERT INTO usershops (username, shopname, checkouturl) VALUES (?, ?, ?)");
			$stmt->bind_param("sss", $Uusername, $Ushopname, $Ucheckouturl);
			$Uusername = $username;
			$Ushopname = $_post['shopName'];
			$Ucheckouturl = $_post['checkouturl'];
			$stmt->execute();
			
			echo "New shop created successfully";
			$stmt->close();
		}
	//} 

?>