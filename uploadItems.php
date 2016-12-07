<! DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" type="text/css" href="format.css">
</head>
<body>
<?php
$errors = array (
	1 => "File uploaded is not an image!",
	2 => "File with the name already exists",
	3 => "The file size is too large",
	4 => "Sorry! We only have support for PNG, JPEG, JPG and GIF",
	5 => "There was an error uploading your file, try again!",
	6 => "You have to be logged in to upload items!"
);

$errorID = isset($_GET['err']) ? (int)$_GET['err'] : 0;
if (isset($_GET['err'])){
	if($_GET['err'] == 1){
		echo "File uploaded is not an image!";
	} else if ($_GET['err'] == 2){
		echo "File with the name already exists";
	} else if ($_GET['err'] == 3){
		echo "The file size is too large";
	} else if ($_GET['err'] == 4){
		echo "Sorry! We only have support for PNG, JPEG, JPG and GIF";
	} else if ($_GET['err'] == 5){
		echo "There was an error uploading your file, try again!";
	} else if ($_GET['err'] == 5){
		echo "You have to be logged in to upload items!";
	}
}


/*
if ($errorID != 0 && in_array($errorID, $errors)){
	echo $errors[$errorID];
}*/
?>
	<form action="uploadScript.php" method="post" enctype="multipart/form-data">
		<fieldset>
			<legend> Item upload</legend>
			<label class="field" for="shopName">Shop name:</label> <input type="text" name="shopName"><br>
			<label class="field" for="spawnPoint">Spawn point:</label> <input type="text" name="spawnPoint"><br>
			<label class="field" for="productId">Product id:</label> <input type="text" name="productId"><br>
			<label class="field" for="productName">Product name:</label> <input type="text" name="productName"><br>
			<label class="field" for="productDesc">Product description:</label> <input type="text" name="productDesc"><br>
			<label class="field" for="productQuantity">Product quantity:</label>  <input type="text" name="productQuantity"><br>
			<label class="field" for="productCost">Product cost</label>  <input type="text" name="productCost"><br><br>
			<label class="field" for="submit">Image of product</label><br>
			<input type="file" name="fileToUpload" id="fileToUpload">
			<input type="submit" value="Upload Image" name="submit">
		</fieldset>
	</form>
</body>