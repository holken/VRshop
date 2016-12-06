<HTML>
<h1>Your store information</h1>
<?php

	//$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	//include_once($WebsiteRoot . '/wp-config.php');
		
	//if (is_user_logged_in()){
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
		$conn = new mysqli($servername, $username, $password, $dbName);
		
		$stmt = $conn->prepare("SELECT shopname, checkouturl FROM usershops WHERE username = ?");
		$stmt->bind_param("s", $current_user);
		$current_user = "testuser";
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($shopname, $checkouturl);
		while($stmt->fetch()){
			echo "Shop name: $shopname <br>";
			echo "checkout url: $checkouturl <br><br>";
			
			$stmt = $conn->prepare("SELECT paymentname FROM paymentoptions WHERE storename = ?");
			$stmt->bind_param("s", $shopname);
			$stmt->execute();
			$stmt->store_result();
			$stmt->bind_result($paymentname);
			echo "<h3> Payment options </h3>";
			while($stmt->fetch()){
				echo "$paymentname <br>";
			}
		}
		
		
	
		
		
		$stmt = $conn->prepare("SELECT shopname, shoptype, spawnpoint, prodid, prodname, proddesc, prodquant, prodcost, productImg FROM shopitems WHERE uploadusername = ? ORDER BY spawnpoint ASC");
		$stmt->bind_param("s", $current_user);
		//$current_user = wp_get_current_user();
		$current_user = "testuser";
		$stmt->execute();
		
		$stmt->store_result();
		$stmt->bind_result($shopname, $shoptype, $spawnpoint, $prodid, $prodname, $proddesc, $prodquant, $prodcost, $productImg);
		echo "<br>";
		echo "<h2>Your products</h2>";
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
		echo "<form action=\"deleteitem.php?\" method=\"post\"> <button name=\"product\" type=\"submit\" value=\"$spawnpoint||$shopname\">Delete item</button> </form>";
		}

$stmt->close();


?>

</HTML>