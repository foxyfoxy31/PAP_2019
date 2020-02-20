<?php

    $con = mysqli_connect('localhost', 'root', '', 'unitydatabase');
    if (mysqli_connect_errno()) {
        echo "1: Connection failed"; //connection failed
        exit();
    }

    $playerName = $_POST["playerName"];
    $levelTime = $_POST["levelTime"];

    $floatLevelTime = floatval($levelTime);

    //add user
    $insertuserquery = "INSERT INTO leaderboard (playername, Leveltime) VALUES ('" . $playerName . "' , '" . $floatLevelTime . "');";
    mysqli_query($con, $insertuserquery) or die("2: Insert query failed");

    echo("0");
?>