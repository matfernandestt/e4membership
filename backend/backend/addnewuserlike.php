<?php

	$con = mysqli_connect('localhost', 'id15706863_matfernandestt', '123!@#QWEasd', 'id15706863_e4membership');
	
	$gamercode = $_POST["gamercode"];
	$gamename = $_POST["gamename"];

	$insertgame = "INSERT INTO userlikes (gamercode, gamename) VALUES ('" . $gamercode . "', '" . $gamename . "');";
	mysqli_query($con, $insertgame) or die("addnewuserlike.php - insert new user like failed");

	echo "0";

?>