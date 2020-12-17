<?php

	$con = mysqli_connect('localhost', 'id15706863_matfernandestt', '123!@#QWEasd', 'id15706863_e4membership');

	if(mysqli_connect_errno())
	{
		echo "failed";
		exit();
	}

	$gamercode = $_POST["gamercode"];
	$username = $_POST["username"];
	$photo = $_POST["photo"];

	$gamercodecheckquery = "SELECT gamercode FROM users WHERE gamercode='" . $gamercode . "';";

	$gamercodecheck = mysqli_query($con, $gamercodecheckquery) or die("register.php - gamercode check query failed");

	if(mysqli_num_rows($gamercodecheck) > 0)
	{
		echo "register.php - gamercode already exists";
		exit();
	}

	$insertuser = "INSERT INTO users (gamercode, username, photo) VALUES ('" . $gamercode . "', '" . $username . "', '" . $photo . "');";
	mysqli_query($con, $insertuser) or die("register.php - insert player failed");

	echo "0";

?>