<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);
	//$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	//include_once($WebsiteRoot . '/wp-config.php');
	
	//if (is_user_logged_in()){
		$servername = "localhost";
		$username = "root";
		$password = "";
		$dbName = "vrshop";
		/*$db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
		mysql_select_db($dbName) or die('Could not select database');*/
		
		$conn = new mysqli($servername, $username, $password, $dbName);
		
		//$current_user = wp_get_current_user();
		$username = "holky";
		$paymentOptions = $_POST['payment'];
		if(isset($_POST['shopName']) && !empty($paymentOptions)){
			
			//$query = "insert into usershops (username, shopname, checkouturl) values ('$username', '$_POST[shopName]', '$_POST[checkouturl]');";
			$query = "insert into usershops (username, shopname, checkouturl) values (?, ?, ?)";
            $stmt = $conn->prepare($query);
			$shopname = $_POST['shopName'];
			$stmt->bind_param("sss", $username, $shopname, $_POST['checkouturl']);
			$stmt->execute();
			
			$N = count($paymentOptions);
 
			for($i=0; $i < $N; $i++)
			{
				$query = "insert into paymentoptions(storename, paymentname) values (?, ?)";
				$stmt = $conn->prepare($query);
				$payment = $paymentOptions[$i];
				echo $payment;
				$stmt->bind_param("ss", $shopname, $payment);
				$stmt->execute();
			}
			
			//$result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
			//$Uusername = $username;
			//$Ushopname = $_post['shopName'];
			//$Ucheckouturl = $_post['checkouturl'];
			//$stmt->execute();
			
			echo "New shop created successfully";
			//$stmt->close();
		}
	//} 

?>