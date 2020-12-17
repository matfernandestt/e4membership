<?php

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
	//$gamercodequerycheck.close();

?>