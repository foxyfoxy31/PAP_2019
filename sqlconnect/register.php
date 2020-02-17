<?php

    $con = mysqli_connect('localhost', 'root', '', 'unitydatabase');
    if (mysqli_connect_errno()) {
        echo "1: Connection failed"; //connection failed
        exit();
    }

    $playerName = $_POST["playerName"];
    $levelTime = $_POST["levelTime"];

    //add user
    $insertuserquery = "INSERT INTO leaderboard (playername, leveltime) VALUES ('" . $playerName . "' , '" . $levelTime . "');";
    mysqli_query($con, $insertuserquery) or die("2: Insert query failed");

    echo("0");
?>