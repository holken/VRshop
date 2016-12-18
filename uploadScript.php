<?php

$target_dir = "Images/";
$target_file = $target_dir . basename($_FILES["fileToUpload"]["name"]);
$uploadOk = 1;
$imageFileType = pathinfo($target_file,PATHINFO_EXTENSION);

$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

include_once($WebsiteRoot . '/wp-config.php');

if (is_user_logged_in()){

	// Check if image file is a actual image or fake image
	if(isset($_POST["submit"])) {
		$check = getimagesize($_FILES["fileToUpload"]["tmp_name"]);
		if($check !== false) {
			echo "File is an image - " . $check["mime"] . ".";
			$uploadOk = 1;
		} else {
			echo "File is not an image.";
			header ("Location: uploadItems.php?err=1");
			$uploadOk = 0;
		}
	}
	// Check if file already exists
	if (file_exists($target_file)) {
		echo "Sorry, file already exists.";
		header ("Location: uploadItems.php?err=2");
		$uploadOk = 0;
	}
	// Check file size
	if ($_FILES["fileToUpload"]["size"] > 5000000) {
		echo "Sorry, your file is too large.";
		header ("Location: uploadItems.php?err=3");
		$uploadOk = 0;
	}
	// Allow certain file formats
	if($imageFileType != "jpg" && $imageFileType != "png" && $imageFileType != "jpeg"
	&& $imageFileType != "gif"  && $imageFileType != "PNG" && $imageFileType != "JPG") {
		echo "Sorry, only JPG, JPEG, PNG & GIF files are allowed.";
		header ("Location: uploadItems.php?err=4");
		$uploadOk = 0;
	}
	// Check if $uploadOk is set to 0 by an error
	if ($uploadOk == 0) {
		echo "Sorry, your file was not uploaded.";
	// if everything is ok, try to upload file
	} else {
		$RandomAccountNumber = uniqid();
		if (move_uploaded_file($_FILES["fileToUpload"]["tmp_name"], $target_file)) {
			echo "The file ". basename( $_FILES["fileToUpload"]["name"]). " has been uploaded.";
		} else {
			echo "Sorry, there was an error uploading your file.";
			header ("Location: uploadItems.php?err=5");
		}
	}
	if ($uploadOk != 0){
	//database upload
	$servername = "localhost";
	$username = "fiskeapp";
	$password = "D&5XQJp_{!U5";
	$dbName = "fiskeapp_wrdp1";
	$usernameP = wp_get_current_user();
	$current_user= $usernameP->ID;
	$db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
	mysql_select_db($dbName) or die('Could not select database');

	$shopName = mysql_real_escape_string($_POST['shopName'], $db); 
	$spawnpoint = mysql_real_escape_string($_POST['spawnPoint'], $db); 
	$prodid = mysql_real_escape_string($_POST['productId'], $db); 
	$prodname = mysql_real_escape_string($_POST['productName'], $db); 
	$proddesc = mysql_real_escape_string($_POST['productDesc'], $db); 
	$prodquant = mysql_real_escape_string($_POST['productQuantity'], $db);
	$prodcost = mysql_real_escape_string($_POST['productCost'], $db);

	$page_file_temp = $_SERVER["PHP_SELF"];
	$page_directory = dirname($page_file_temp);

	$imglink = "http://$_SERVER[HTTP_HOST]$page_directory/".$target_file;
	$query = "DELETE FROM shopitems WHERE shopname = '$shopName' AND spawnpoint = '$spawnpoint' AND uploadusername = '$current_user';";
	$result = mysql_query($query) or die('Query failed: ' . mysql_error()); 

	$query = "insert into shopitems(shopname, uploadusername, shoptype, spawnpoint, prodid, prodname, proddesc, prodquant, prodcost, productImg) values ('$shopName', '$current_user', 'testshop1', '$spawnpoint', '$prodid', '$prodname', '$proddesc', '$prodquant', '$prodcost', '$imglink');"; 
	$result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
	}

} else {
	header( "Location: uploadItems.php?err=6");
}
?>