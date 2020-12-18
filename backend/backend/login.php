<?php

	header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Methods: GET, POST');
	header("Access-Control-Allow-Headers: X-Requested-With");

	$mysqli = new mysqli("localhost", "id15706863_matfernandestt", "123!@#QWEasd", "id15706863_e4membership");

	$gamercode = $_POST["gamercode"];

	$gamercodequery = "SELECT * FROM users WHERE gamercode = '". $gamercode ."'";
	$gamercodequerycheck = $mysqli->query($gamercodequery);

	if(mysqli_num_rows($gamercodequerycheck) == 0)
	{
		echo "1";
		exit();
	}

	while ($row = $gamercodequerycheck->fetch_assoc())
	{
		echo $row["id"] . "@" . $row["gamercode"] . "@" . $row["username"] . "@" . $row["photo"];
	}

?>