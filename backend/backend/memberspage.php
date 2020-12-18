<?php

	header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Methods: GET, POST');
	header("Access-Control-Allow-Headers: X-Requested-With");

	$mysqli = new mysqli("localhost", "id15706863_matfernandestt", "123!@#QWEasd", "id15706863_e4membership");

	$usersquery = "SELECT * FROM users";
	$userscheck = $mysqli->query($usersquery);

	while ($row = $userscheck->fetch_assoc())
	{
		echo $row["id"] . "@" . $row["gamercode"] . "@" . $row["username"] . "@" . $row["photo"] . "*";
	}

?>