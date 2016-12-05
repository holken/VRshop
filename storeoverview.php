<HTML>
<h1>Your products</h1>
<?php

	//$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	//include_once($WebsiteRoot . '/wp-config.php');
		
	//if (is_user_logged_in()){
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
		$conn = new mysqli($servername, $username, $password, $dbName);
		
		$stmt = $conn->prepare("SELECT shopname, shoptype, spawnpoint, prodid, prodname, proddesc, prodquant, prodcost, productImg FROM shopitems WHERE uploadusername = ? ORDER BY spawnpoint ASC");
		$stmt->bind_param("s", $current_user);
		//$current_user = wp_get_current_user();
		$current_user = "testuser";
		$stmt->execute();
		
		$stmt->store_result();
		$stmt->bind_result($shopname, $shoptype, $spawnpoint, $prodid, $prodname, $proddesc, $prodquant, $prodcost, $productImg);
		
		while($stmt->fetch()){
			echo "<h3>$prodname</h3>";
			echo "<img src=\"$productImg\" style =\"width:300px;hieght:300px;\"> <br>";
			echo "Id: $prodid <br>";
			echo "Description: $proddesc <br>";
			echo "Quantity: $prodquant <br>";
			echo "Cost: $prodcost <br>";
			echo "Shop name: $shopname <br>";
			echo "Shop type: $shoptype <br>";
			echo "Spawnpoint: $spawnpoint <br><br>";
		}

$stmt->close();


?>

</HTML>