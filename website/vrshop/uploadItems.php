<! DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" type="text/css" href="format.css">
</head>
<body>
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