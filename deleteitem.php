<?php
	//$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	//include_once($WebsiteRoot . '/wp-config.php');
		
	//if (is_user_logged_in()){
		echo $_POST['product'];
		$pos = strrpos($_POST['product'], "||");
		$spawnpoint = substr($_POST['product'], 0, $pos);
		$shopName = substr($_POST['product'],$pos+2);
	
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
		$conn = new mysqli($servername, $username, $password, $dbName);
		$query = "SELECT FROM"
		
		$query = "DELETE FROM shopitems WHERE shopname = '$shopName' AND spawnpoint = '$spawnpoint';";
	//}
?>