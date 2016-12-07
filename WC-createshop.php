<?php
	
	$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	include_once($WebsiteRoot . '/wp-config.php');
	
	if (is_user_logged_in()){
		$servername = "localhost";
		$username = "fiskeapp";
		$password = "D&5XQJp_{!U5";
		$dbName = "fiskeapp_wrdp1";
		//$db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
		//mysql_select_db($dbName) or die('Could not select database');
		
		$conn = new mysqli($servername, $username, $password, $dbName);
		$usernameP = wp_get_current_user();
		$current_user = $usernameP->ID;
		//$username = "testuser2";
		
		//$checkUserID = mysql_query("SELECT * from usershops WHERE username = '$username'");
		$query = "SELECT * from usershops WHERE username = '$username'";
		$result = $conn->query($query);
		if ($result->num_rows != 0) {
			echo "you already own a site";
			header( "Location: alreadycreatedshop.php" );
		} else {
			header( "Location: createshop.php" );
		}
		
	} else {
		echo "you have to login!";
	}

?>