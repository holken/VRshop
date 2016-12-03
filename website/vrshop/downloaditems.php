<?php
    // Send variables for the MySQL database class.
    $servername = "localhost";
	$username = "root";
	$password = "";
	$shopname = $_GET['shopname'];
	$dbName = "vrshop";
    $db = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error()); 
    mysql_select_db($dbName) or die('Could not select database');
 
    $query = "SELECT * FROM `shopitems` WHERE shopname = '$shopname'";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
        $row = mysql_fetch_array($result);
        //echo $row['shopname'] . "\t" . $row['uploadusername'] . "\t" . $row['shoptype'] . "\t" . $row['spawnpoint'] . "\t" . $row['prodid'] . "\t" . $row['prodname'] . "\t" . $row['proddesc'] . "\t" . $row['prodquant'] . "\t" . $row['prodcost'] . "\n";
		echo $row['shopname'] . "|" . $row['uploadusername'] . "|" . $row['shoptype'] . "|" . $row['spawnpoint'] . "|" . $row['prodid'] . "|" . $row['prodname'] . "|" . $row['proddesc'] . "|" . $row['prodquant'] . "|" . $row['prodcost'] . "|" . $row['uploadID'] . "|" . $row['productImg'] . "||";
	}
?>