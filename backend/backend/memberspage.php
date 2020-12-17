<?php

	$mysqli = new mysqli("localhost", "id15706863_matfernandestt", "123!@#QWEasd", "id15706863_e4membership");

	$usersquery = "SELECT * FROM users";
	$userscheck = $mysqli->query($usersquery);

	while ($row = $userscheck->fetch_assoc())
	{
		echo $row["id"] . "@" . $row["gamercode"] . "@" . $row["username"] . "@" . $row["photo"] . "*";
	}
	$userscheck.close();

?>