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
		
		$checkUserID = mysql_query("SELECT * from usershops WHERE username = '$username'");
		if (mysql_num_rows($checkUserId) > 0) {
			echo "you already own a site";
			header( "Location: alreadycreatedshop.php" );
		} else {
			header( "Location: createshop.php" );
		}
		
	//} 

?>