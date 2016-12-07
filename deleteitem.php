<?php
	$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	include_once($WebsiteRoot . '/wp-config.php');
		
	if (is_user_logged_in()){
		echo $_POST['product'] . "<br>";
		$pos = strrpos($_POST['product'], "||");
		$pos2 = strpos($_POST['product'], "||");
		$spawnpoint = substr($_POST['product'], 0, $pos2);
		$shopName = substr($_POST['product'], $pos2+2, intval($pos)-intval($pos2)-2);
		$prodName = substr($_POST['product'],$pos+2);
		echo $spawnpoint . "<br>";
		echo $shopName . "<br>";
		echo $prodName;
		
		$servername = "localhost";
		$username = "fiskeapp";
		$password = "D&5XQJp_{!U5";
		$dbName = "fiskeapp_wrdp1";
		$conn = new mysqli($servername, $username, $password, $dbName);
		$usernameP = wp_get_current_user();
		$username = $usernameP->ID;
		//$username = "testuser";
		
		$query = "DELETE FROM shopitems WHERE shopname = ? AND spawnpoint = ? AND uploadusername = ?";
		$stmt = $conn->prepare($query);
		$stmt->bind_param("sss", $shopName, $spawnpoint, $username);
		$stmt->execute();
		header( "Location: storeoverview.php?del=$prodName");
	} else {
		echo "you have to be logged in to delete an item";
	}
?>