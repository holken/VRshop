<html>
<head>
	<link rel="stylesheet" type="text/css" href="format.css">
</head>
<body>
<?php
	$errors = array (
		1 => "A store with that name already exists!",
		2 => "You have already created a shop!"
	);

	$errorID = isset($_GET['err']) ? (int)$_GET['err'] : 0;
	
	if (isset($_GET['err'])){
		
		if($_GET['err'] == 1){
			echo "A store with that name already exists!";
		} else if ($_GET['err']){
			echo "You have already created a shop!";
		}
	}

?>

<form action="createshopscript.php" method="post">
		<fieldset>
			<legend> Create shop</legend>
			<label class="field" for="shopName">Shop name:</label> <input type="text" name="shopName"><br>
			<label class="field" for="checkouturl">checkout script url:</label> <input type="text" name="checkouturl"><br>
			<input type="checkbox" name="payment[]" value="Sveaweb">sveaweb<br>
			<input type="checkbox" name="payment[]" value="Paypal">paypal<br>
			<input type="checkbox" name="payment[]" value="Creditcard">credit card<br>
			<input type="submit" value="Submit">
		</fieldset>
	</form>

</body>
</html>